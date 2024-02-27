using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class DrawerInteractable : MonoBehaviour
{
    [SerializeField] XRSocketInteractor keySocket;
    [SerializeField] bool isLocked;
    void Start()
    {
        if(keySocket != null)
        {
            keySocket.selectEntered.AddListener(OnDrawerUnlocked);
            keySocket.selectExited.AddListener(OnDrawerLocked);
        }
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
