﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;

public static class EventManager
{
    /// <summary>
    public const int TEST_A_DISPATCHER = 0x00000001;
    public const int TEST_B_DISPATCHER = 0x0000000f;
    public const int TEST_C_DISPATCHER = 0x00000003;

    public static ApplicationEventDispatcher applicationEventDispatcher = null;
    public static void Init()
    {
        applicationEventDispatcher = new ApplicationEventDispatcher();
    }
}


class EventTypeA : IApplicationEvent
{
    private readonly int _eventType;

    public EventTypeA(int _eventType)
    {
        this._eventType = _eventType;
    }

    public int evenType
    {
        get { return _eventType; }
    }
}

class EventTypeB : IApplicationEvent
{
    private readonly int _eventType;

    public EventTypeB(int _eventType)
    {
        this._eventType = _eventType;
    }

    public int evenType
    {
        get { return _eventType; }
    }
}


public interface IApplicationEvent { }

public delegate void ApplicationEventHandlerDelegate<in TEvent>(TEvent @event) where TEvent : IApplicationEvent;

public interface IApplicationEventDispatcher : IDisposable
{
    void AddListener<TEvent>(ApplicationEventHandlerDelegate<TEvent> handler) where TEvent : IApplicationEvent;
    void RemoveListener<TEvent>(ApplicationEventHandlerDelegate<TEvent> handler) where TEvent : IApplicationEvent;
    void Dispatch<TEvent>(TEvent @event) where TEvent : IApplicationEvent;
}

public class ApplicationEventDispatcher : IApplicationEventDispatcher
{
    private bool _disposed;
    private Dictionary<Type, Delegate> _applicationEventHandlers;

    public ApplicationEventDispatcher()
    {
        _applicationEventHandlers = new Dictionary<Type, Delegate>();
    }

    ~ApplicationEventDispatcher()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            // free other managed objects that implement IDisposable only
            RemoveAllListeners();
        }

        // release any unmanaged objects
        // set the object references to null
        _applicationEventHandlers = null;

        _disposed = true;
    }

    public void AddListener<TEvent>(ApplicationEventHandlerDelegate<TEvent> handler) where TEvent : IApplicationEvent
    {
        Delegate @delegate;
        if (_applicationEventHandlers.TryGetValue(typeof(TEvent), out @delegate))
        {
            _applicationEventHandlers[typeof(TEvent)] = Delegate.Combine(@delegate, handler);
        }
        else
        {
            _applicationEventHandlers[typeof(TEvent)] = handler;
        }
    }

    public void RemoveListener<TEvent>(ApplicationEventHandlerDelegate<TEvent> handler) where TEvent : IApplicationEvent
    {
        Delegate @delegate;
        if (_applicationEventHandlers.TryGetValue(typeof(TEvent), out @delegate))
        {
            Delegate currentDel = Delegate.Remove(@delegate, handler);

            if (currentDel == null)
            {
                _applicationEventHandlers.Remove(typeof(TEvent));
            }
            else
            {
                _applicationEventHandlers[typeof(TEvent)] = currentDel;
            }
        }
    }

    public void Dispatch<TEvent>(TEvent @event) where TEvent : IApplicationEvent
    {
        if (@event == null)
            throw new ArgumentNullException("event");

        if (_disposed)
            throw new ObjectDisposedException("Cannot dispatch and event when disposed! ");

        Delegate @delegate;
        if (_applicationEventHandlers.TryGetValue(typeof(TEvent), out @delegate))
        {
            ApplicationEventHandlerDelegate<TEvent> callback = @delegate as ApplicationEventHandlerDelegate<TEvent>;
            if (callback != null)
            {
                callback(@event);
            }
        }
    }

    private void RemoveAllListeners()
    {
        var handlerTypes = new Type[_applicationEventHandlers.Keys.Count];
        _applicationEventHandlers.Keys.CopyTo(handlerTypes, 0);

        foreach (Type handlerType in handlerTypes)
        {
            Delegate[] delegates = _applicationEventHandlers[handlerType].GetInvocationList();
            foreach (Delegate @delegate1 in delegates)
            {
                var handlerToRemove = Delegate.Remove(_applicationEventHandlers[handlerType], @delegate1);
                if (handlerToRemove == null)
                {
                    _applicationEventHandlers.Remove(handlerType);
                }
                else
                {
                    _applicationEventHandlers[handlerType] = handlerToRemove;
                }
            }
        }
    }
}