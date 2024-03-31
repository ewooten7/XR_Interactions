using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TheWall : MonoBehaviour
{
    [SerializeField] int columns;
    [SerializeField] int rows;
    [SerializeField] GameObject wallCubePrefab;
    [SerializeField] GameObject socketWallPrefab;
    [SerializeField] int socketPosition = 1;
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
        for (int i = 0; i < columns; i++)
        {
            GenerateColumn(rows, true);
            spawnPosition.x += cubeSize.x + cubeSpacing;
        }
    }
    private void GenerateColumn(int height, bool socketed)
    { 
        spawnPosition.y = transform.position.y;
        wallCubes = new GameObject[height];
        for (int i = 0; i < wallCubes.Length; i++)
        {
            if (wallCubePrefab != null)
            {
                wallCubes[i] = Instantiate(wallCubePrefab, spawnPosition, transform.rotation);
                if(i == 0)
                {
                    wallCubes[i].name = "column";
                    wallCubes[i].transform.SetParent(transform);
                }
                else
                {
                    wallCubes[i].transform.SetParent(wallCubes[0].transform);
                }
            }

            spawnPosition.y += cubeSize.y + cubeSpacing;
        }

        if (socketed && socketWallPrefab != null)
        {
            if (socketPosition < 0 || socketPosition >= height)
            {
                socketPosition = 0;
            }
            if (wallCubes[socketPosition] != null)
            {
                Vector3 position = wallCubes[socketPosition].transform.position;
                DestroyImmediate(wallCubes[socketPosition]);
                wallCubes[socketPosition] = Instantiate(socketWallPrefab, position, transform.rotation);
                if(socketPosition == 0)
                {
                    wallCubes[socketPosition].transform.SetParent(transform);
                }
                else
                {
                    wallCubes[socketPosition].transform.SetParent(wallCubes[0].transform);
                }
                wallSocket = wallCubes[socketPosition].GetComponentInChildren<XRSocketInteractor>();
                if (wallSocket != null)
                {
                    wallSocket.selectEntered.AddListener(OnSocketEnter);
                    wallSocket.selectExited.AddListener(OnSocketExited);
                }
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

