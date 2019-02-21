﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour, IUpdate
{
    private float time = 0;
    public Image loadImage;
    public float fadeTime = 3;
    private Color loadColour = new Color(0, 0, 0, 0);
    public string scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        GameController.gameController.updateList.Add(this);
        GameController.gameController.allowMovement = false;
        loadImage.enabled = true;
    }

    public void UpdateFunction()
    {
        time += Time.deltaTime;
        if (time < 3)
        {
            loadColour.a = time / fadeTime;
            loadImage.color = loadColour;
        }
        else
        {
            GameController.gameController.EmptyUpdate();
            GameController.gameController.allowMovement = true;
            CameraControl.camControl.moveTime = 0;
            SceneManager.LoadScene(scene);
        }
    }
}