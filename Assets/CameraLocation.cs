using UnityEngine;
using System.Collections;

// This script will only be assigned to the camera in the 'Close-Up' view.

public class CameraLocation : MonoBehaviour {
    public GameObject earth;
    public string upbutton;
    private float angle = 45.0f;
    private Vector3 offset;

    // The start method intialises the script by setting the variable offset equal to the distance between the gameobject (MainCamera) and the Earth.

    void Start ()
    {
            offset = transform.position - earth.transform.position;
    }

    // The update method is called every frame.
    // If the up button is pressed then the camera position changes by an angle relative to offset.
    void Update()
    {

        if (Input.GetButton(upbutton))
        {
            offset = Quaternion.AngleAxis (angle, Vector3.left) * offset;
            transform.LookAt(earth.transform);
        }

        transform.position = earth.transform.position + offset;
    }
}
