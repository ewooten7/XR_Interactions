using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TheWall : MonoBehaviour
{
    [SerializeField] GameObject wallCubePrefab;
    [SerializeField] GameObject socketWallPrefab;
    [SerializeField] XRSocketInteractor wallSocket;
    [SerializeField] GameObject[] wallCubes;
    [SerializeField] float cubeSpacing = 0.005f;
    private Vector3 cubeSize;
    private Vector3 spawnPosition;
    void Start()
    {
        if (wallCubePrefab != null)
        {
            cubeSize = wallCubePrefab.GetComponent<Renderer>().bounds.size;
        }

        spawnPosition = transform.position;
        BuildWall();
    }
    private void BuildWall()
    {
        wallCubes = new GameObject[2];
        if (wallCubePrefab != null)
        {
            wallCubes[0] = Instantiate(wallCubePrefab, spawnPosition, transform.rotation);
        }
        spawnPosition.y += cubeSize.y + cubeSpacing;
        
        if (socketWallPrefab != null)
        {
            wallCubes[1] = Instantiate(socketWallPrefab, spawnPosition, transform.rotation);
            wallSocket = wallCubes[1].GetComponentInChildren<XRSocketInteractor>();

            if (wallSocket != null)
            {
                wallSocket.selectEntered.AddListener(OnSocketEnter);
                wallSocket.selectExited.AddListener(OnSocketExited);
            }
        }
        for (int i = 0; i < wallCubes.Length; i++)
        {
            if (wallCubes[i] != null)
            {
                wallCubes[i].transform.SetParent(transform);
            }
        }
    }
    private void OnSocketExited(SelectExitEventArgs arg0)
    {
        for (int i = 0; i < wallCubes.Length; i++)
        {
            if (wallCubes[i] != null)
            {
                Rigidbody rb = wallCubes[i].GetComponent<Rigidbody>();
                rb.isKinematic = true;
            }
        }
    }
    private void OnSocketEnter(SelectEnterEventArgs arg0)
    {
        for (int i = 0; i < wallCubes.Length; i++)
        {
            if (wallCubes[i] != null)
            {
                Rigidbody rb = wallCubes[i].GetComponent<Rigidbody>();
                rb.isKinematic = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

