using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XrAudioManager : MonoBehaviour
{
    [SerializeField] TheWall wall;
    [SerializeField] AudioSource wallSource;
    [SerializeField] AudioClip destroyWallClip;
    [SerializeField] private AudioClip fallBackClip;
    private const string FallBackClip_Name = "fallBackClip";
 
    private void OnEnable()
    {
        if(fallBackClip == null)
        {
            fallBackClip = AudioClip.Create(FallBackClip_Name, 1, 1, 1000, true);
        }
        if (wall != null)
        {
            destroyWallClip = wall.GetDestroyClip;
            if(destroyWallClip == null)
            {
                destroyWallClip = fallBackClip;
            }
            wall.OnDestroy.AddListener(OnDestroyWall);
        }
    }

    private void OnDestroyWall()
    {
        if(wallSource != null)
        {
            wallSource.Play();
        }
    }

    private void OnDisable()
    {
        if (wall != null)
        {
            wall.OnDestroy.RemoveListener(OnDestroyWall);
        }
    }
}
