using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float Speed;

    [SerializeField]
    private Vector3 turnDirection;

    public void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.eulerAngles = turnDirection;
        }
    }

    public void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Dead")
        {
            // DEAD
        }
        else if (hit.tag == "Right")
        {
            turnDirection += new Vector3(0, 90, 0);
        }
        else if (hit.tag == "Left")
        {
            turnDirection += new Vector3(0, -90, 0);
        }
        else if (hit.tag == "Forward")
        {
            turnDirection = new Vector3(0, 0, 0);
        }
    }
}
