using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InstallEventReceiver();
    }

    public void InstallEventReceiver()
    {
        EventManager.applicationEventDispatcher.AddListener<EventTypeA>(ReceiveEventTypeAMethod);
        Debug.Log("BClass InstallEventReceiver");
    }

    void ReceiveEventTypeAMethod(EventTypeA @event)
    {
        if(@event.evenType == EventManager.TEST_A_DISPATCHER)
        {
            Debug.Log("B Class _ Receive A EVENT");
        }
        else if(@event.evenType == EventManager.TEST_B_DISPATCHER)
        {
            Debug.Log("B Class _ Receive B EVENT");
        }
        else if(@event.evenType == EventManager.TEST_C_DISPATCHER)
        {
            Debug.Log("B Class _ Receive C EVENT");
        }
    }
}
