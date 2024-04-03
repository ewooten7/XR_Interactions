using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRPlayerControl : MonoBehaviour
{
    [SerializeField] GrabMoveProvider[] grabMovers;
    [SerializeField] Collider[] grabColliders;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < grabColliders.Length; i++)
        {
            if (other == grabColliders[i])
            {
                SetGrabMovers(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < grabColliders.Length; i++)
        {
            if (other == grabColliders[i])
            {
                SetGrabMovers(false);
            }
        }
    }
    private void SetGrabMovers(bool isActive)
    {
        for (int i = 0; i < grabMovers.Length; i++)
        {
            grabMovers[i].enabled = isActive;
        }
    }
}
