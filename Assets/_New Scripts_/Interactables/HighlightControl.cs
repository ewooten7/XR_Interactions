using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HighlightControl : MonoBehaviour
{
    [SerializeField] XRBaseInteractable interactableObject;
    [SerializeField] Material startMaterial;
    [SerializeField] Material emmisionMaterial;
    [SerializeField] Renderer highlightableObject;

    private void OnEnable()
    {
        if (interactableObject != null)
        {
            interactableObject.selectEntered.AddListener(HighlightObject);
            interactableObject.selectExited.AddListener(ResetObject);
        }
    }
    private void OnDisable()
    {
        if (interactableObject != null)
        {
            interactableObject.selectEntered.RemoveListener(HighlightObject);
            interactableObject.selectExited.RemoveListener(ResetObject);
        }
    }

    private void ResetObject(SelectExitEventArgs arg0)
    {
        if (highlightableObject != null && startMaterial != null)
        {
            highlightableObject.material = startMaterial;
        }
    }

    private void HighlightObject(SelectEnterEventArgs arg0)
    {
        if (highlightableObject != null && emmisionMaterial != null)
        {
            highlightableObject.material = emmisionMaterial;
        }
    }
}
