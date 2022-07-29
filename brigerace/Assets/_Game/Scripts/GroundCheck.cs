using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public float distanceToCheck =0.001f;
    public bool isGrounded;
    public RaycastHit hit;
    public LayerMask mask;
    public bool canCheck = false;

    void Update()
    {
        if (canCheck)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            isGrounded = Physics.Raycast(ray, out hit,distanceToCheck, mask);
           // Debug.Log(hit.transform.name + "- isGR:" + isGrounded);
            if(isGrounded)
            {
                canCheck = false;
            }
        }
    }
}
