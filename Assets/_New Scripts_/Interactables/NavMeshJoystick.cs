using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshJoystick : SimpleHingeInteractable
{
    [SerializeField] Transform trackedObject;
    [SerializeField] Transform trackingObject;
    protected override void ResetHinge()
    {
        
    }
    protected override void Update()
    {
        base.Update();
        trackingObject.position = new Vector3(trackedObject.position.x,
        trackingObject.position.y, trackedObject.position.z);
    }
}
