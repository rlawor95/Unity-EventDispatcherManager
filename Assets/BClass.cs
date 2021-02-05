using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstallEventReceiver()
    {
        EventManager.applicationEventDispatcher.AddListener<TestEvent>(ReceiveEventMethod);
    }

    void ReceiveEventMethod(TestEvent @event)
    {
        
    }
}
