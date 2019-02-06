using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public static PlayerObject playerObject;
    public int charType = 0;
    public Transform rainbow;

    private void Awake()
    {
        playerObject = this;
        rainbow = transform.Find("Rainbow1");
    }

    void Start()
    {
        CameraControl.camControl.player = transform;
    }
}
