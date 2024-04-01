using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class ExplosiveDevice : XRGrabInteractable
{
    public UnityEvent OnDetonated;
    private bool isActivated;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        if(args.interactorObject.transform
        .GetComponent<XRSocketInteractor>() != null)
        {
            isActivated = true;
        }
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(isActivated &&
            other.gameObject.GetComponent<WandProjectile>() != null)
        {
            OnDetonated?.Invoke();
        }
    }
}
