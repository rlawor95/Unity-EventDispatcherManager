using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Init();

    }


    public void AClassEventMethod()
    {
        var @event = new TestEvent(EventManager.TEST_DISPATCHER);
        EventManager.applicationEventDispatcher.Dispatch(@event);
    }
}
