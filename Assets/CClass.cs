using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CClass : MonoBehaviour
{
    void Start()
    {
        InstallEventReceiver();
    }

    public void InstallEventReceiver()
    {
        EventManager.applicationEventDispatcher.AddListener<TestEvent>(ReceiveEventMethod);
        Debug.Log("C Class InstallEventReceiver");
    }

    void ReceiveEventMethod(TestEvent @event)
    {
        Debug.Log(@event.evenType);
        if (@event.evenType == EventManager.TEST_A_DISPATCHER)
        {
            Debug.Log("C Class _ Receive A EVENT");
           
        }
        else if (@event.evenType == EventManager.TEST_B_DISPATCHER)
        {
            Debug.Log("C Class _ Receive B EVENT");
        }
        else if (@event.evenType == EventManager.TEST_C_DISPATCHER)
        {
            Debug.Log("C Class _ Receive C EVENT");
        }
    }
}
