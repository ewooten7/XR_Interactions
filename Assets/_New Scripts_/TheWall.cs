using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TheWall : MonoBehaviour
{
    [SerializeField] XRSocketInteractor wallSocket;
    [SerializeField] GameObject[] wallCubes;
    void Start()
    {
        if(wallSocket != null)
        {
            wallSocket.selectEntered.AddListener(OnSocketEnter);
            wallSocket.selectExited.AddListener(OnSocketExited);
        }
    }

    private void OnSocketExited(SelectExitEventArgs arg0)
    {
        
    }

    private void OnSocketEnter(SelectEnterEventArgs arg0)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
