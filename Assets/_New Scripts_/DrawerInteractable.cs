using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class DrawerInteractable : XRGrabInteractable
{
    [SerializeField] XRSocketInteractor keySocket;
    [SerializeField] bool isLocked;
    private Transform parentTransform;
    private const string defaultLayer = "Default";
    private const string grabLayer = "Grab";
    void Start()
    {   
        if(keySocket != null)
        {
            keySocket.selectEntered.AddListener(OnDrawerUnlocked);
            keySocket.selectExited.AddListener(OnDrawerLocked);
        }
        parentTransform = transform.parent.transform;
    }
    private void OnDrawerLocked(SelectExitEventArgs arg0)
    {
        isLocked = true;
        Debug.Log("****DRAWER LOCKED");
    }

    private void OnDrawerUnlocked(SelectEnterEventArgs arg0)
    {
        isLocked = false;
        Debug.Log("****DRAWER UNLOCKED");
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        if(!isLocked)
        {
           transform.SetParent(parentTransform);                     
        }
        else
        {
            interactionLayers = InteractionLayerMask.GetMask(defaultLayer);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        interactionLayers = InteractionLayerMask.GetMask(grabLayer);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

