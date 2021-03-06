
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCam : SingletonMonoBehaviour<FollowPlayerCam>
{
    Vector3 cameraOffset;
    public Transform player;
    private float followSpeed = 3f;

    public Vector3 mainCameraRoom;
    private Vector3 mainRotation;

    public Vector3 currentCameraRoom;
    Transform trans;
    Vector3 newPos;
    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - player.transform.position;
        trans = transform;
    }

    // Update is called once per frame


    private void LateUpdate()
    {

        newPos = player.transform.position + cameraOffset;
        trans.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * followSpeed);
    }
}
