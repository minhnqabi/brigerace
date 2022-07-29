using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GroundCheckWithSnapping : MonoBehaviour
{
    public float distanceToCheck = 2f;
    public bool isGrounded;
    public RaycastHit hit;
    public LayerMask mask;
    public bool canFall = false;
    public float velocity = -10;
  
}