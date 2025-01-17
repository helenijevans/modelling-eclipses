using UnityEngine;
using System.Collections;

public class Light : MonoBehaviour
        
{
    // This holds the position of an out-of-script defined game object called EARTH.
    public Transform EARTH;

    protected void Update()
    {
        // Rotates the transform so the forward vector points towards EARTH's current position.
        transform.LookAt(EARTH);
}
}
