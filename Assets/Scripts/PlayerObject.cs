﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public static PlayerObject playerObject;
    public Transform rainbow;
    private int charType;

    private void Awake()
    {
        playerObject = this;
        rainbow = transform.Find("Rainbow1");
    }

    void Start()
    {
        CameraControl.camControl.player = transform;
        charType = GameController.gameController.charType;
        switch (charType)
        {
            case 0:
                transform.Find("RightLimb").gameObject.SetActive(true);
                transform.Find("LeftLimb").gameObject.SetActive(true);
                transform.Find("RightFoot").gameObject.SetActive(true);
                transform.Find("LeftFoot").gameObject.SetActive(true);
                break;
            case 1:
                transform.Find("Head_S").gameObject.SetActive(true);
                break;
            case 2:
                transform.Find("Torso_A").gameObject.SetActive(true);
                break;
        }
    }

    public void changeChar(int charType)
    {
        switch (charType)
        {
            case 0:
                transform.Find("RightLimb").gameObject.SetActive(true);
                transform.Find("LeftLimb").gameObject.SetActive(true);
                transform.Find("RightFoot").gameObject.SetActive(true);
                transform.Find("LeftFoot").gameObject.SetActive(true);
                transform.Find("Head_S").gameObject.SetActive(false);
                transform.Find("Torso_A").gameObject.SetActive(false);
                break;
            case 1:
                transform.Find("Head_S").gameObject.SetActive(true);
                transform.Find("Torso_A").gameObject.SetActive(false);
                transform.Find("RightLimb").gameObject.SetActive(false);
                transform.Find("LeftLimb").gameObject.SetActive(false);
                transform.Find("RightFoot").gameObject.SetActive(false);
                transform.Find("LeftFoot").gameObject.SetActive(false);
                break;
            case 2:
                transform.Find("Torso_A").gameObject.SetActive(true);
                transform.Find("Head_S").gameObject.SetActive(false);
                transform.Find("RightLimb").gameObject.SetActive(false);
                transform.Find("LeftLimb").gameObject.SetActive(false);
                transform.Find("RightFoot").gameObject.SetActive(false);
                transform.Find("LeftFoot").gameObject.SetActive(false);
                break;
        }
    }
}
