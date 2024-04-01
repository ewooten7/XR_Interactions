using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WandControl : XRGrabInteractable
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnPoint;
    private bool isFiring;

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);
        if(projectilePrefab != null)
        {
            Instantiate(projectilePrefab,
                projectileSpawnPoint.position,
                projectileSpawnPoint.rotation);
        }
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);
    }
}
