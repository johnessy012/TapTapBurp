using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float Speed;

    private Vector3 _startingPosition;
    private LevelCreator _levelCreator;

    private void Start()
    {
        _startingPosition = transform.position;
        _levelCreator = FindObjectOfType<LevelCreator>();
    }

    private void Update()
    {
        transform.position += Vector3.forward * Speed * Time.deltaTime;
    }
}
