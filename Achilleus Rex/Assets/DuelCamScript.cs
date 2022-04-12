using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelCamScript : MonoBehaviour
{
    public Transform cam;
    public Transform self;
    public Transform other;
    public Vector3 midpoint;
    
    void Update()
    {
        //Calculate Midpoint between self and other
        midpoint = ((other.position - self.position) / 2) + self.position;
        cam.LookAt(midpoint);
    }
}
