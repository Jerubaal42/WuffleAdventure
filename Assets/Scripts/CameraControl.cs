using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour, IUpdate
{
    public static CameraControl camControl;
    public static Camera mainCam;
    public bool followPlayer = false;
    public Vector3 destination;
    private Vector3 currentVelocity;
    public float moveTime = 0;
    [HideInInspector]
    public Transform player;
    public float camPos = -10;

    void Awake()
    {
        camControl = this;
        mainCam = GetComponent<Camera>();
    }

    void Start()
    {
        GameController.gameController.updateList.Add(this);
    }

    public void UpdateFunction()
    {
        if (followPlayer)
        {
            destination = player.position;
        }
        destination = new Vector3(destination.x, destination.y, camPos);
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref currentVelocity, moveTime);
    }
}
