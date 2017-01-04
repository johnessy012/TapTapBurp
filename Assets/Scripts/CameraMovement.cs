using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Transform target;
    private Vector3 currentRotation;
    private float distance;
    private float currentHeight;

    public enum CameraSettings
    {
        Slow,
        Normal, 
        Fast, 
        SlowInverted, 
        NormalInverted, 
        FastInverted,
        SlowWithTween, 
        NormalWithTween, 
        FastWithTween, 
        TopDown, 
        BehindPlayer, 
        InfrontOfPlayer
    };

    public CameraSettings CamSettings;

    private void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        transform.position = target.position;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x ,currentHeight , transform.position.z);

        // Always look at the target
        transform.LookAt(target);        
    }

    public void SwitchCamSettings(CameraSettings setting)
    {
        // Set the new camera settings;
        CamSettings = setting;
    }
}
