using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorInteractable : SimpleHingeInteractable
{
    [SerializeField] CombinationLock comboLock;
    [SerializeField] Transform doorObject;
    [SerializeField] Vector3 rotationLimits;
    [SerializeField] Collider closedCollider;
    private bool isClosed;
    private Vector3 startRotation;
    [SerializeField] Collider openCollider;
    private bool isOpen;
    [SerializeField] private Vector3 endRotation;
    private float startAngleX;
    protected override void Start()
    {
        base.Start();
        startRotation = transform.localEulerAngles;
        startAngleX = startRotation.x;
        if (startAngleX >= 180)
        {
            startAngleX -= 360;
        }
        if (comboLock != null)
        {
            comboLock.UnlockAction += OnUnlocked;
            comboLock.LockAction += OnLocked;
        }
    }
    private void OnLocked()
    {
        LockHinge();
    }

    private void OnUnlocked()
    {
        UnlockHinge();
    }

    // Update is called once per frame 
    protected override void Update()
    {
        base.Update();
        if (doorObject != null)
        {
            doorObject.localEulerAngles = new Vector3(
                doorObject.localEulerAngles.x,
                transform.localEulerAngles.y,
                doorObject.localEulerAngles.z
            );
        }

        if (isSelected)
        {
            CheckLimits();
        }
    }
    private void CheckLimits()
    {
        isClosed = false;
        isOpen = false;
        float localAngleX = transform.localEulerAngles.x;

        if (localAngleX >= 180)
        {
            localAngleX -= 360;
        }
        if (localAngleX >= startAngleX + rotationLimits.x ||
            localAngleX <= startAngleX - rotationLimits.x)
        {
            ReleaseHinge();

        }
    }
    protected override void ResetHinge()
    {
        if (isClosed)
        {
            transform.localEulerAngles = startRotation;
        }
        else if(isOpen)
        {
            transform.localEulerAngles = endRotation;
        }
        else
        {
            transform.localEulerAngles = new Vector3(
                startAngleX,
                transform.localEulerAngles.y,
                transform.localEulerAngles.z);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == closedCollider)
        {
            isClosed = true;
            ReleaseHinge();
        }
        else if(other == openCollider)
        {
            isOpen = true;
            ReleaseHinge();
        }
    }
}

