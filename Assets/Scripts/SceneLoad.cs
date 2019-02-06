using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour, IUpdate
{
    private float time = 0;
    public float scrollTime = 1;
    public float waitTime = 1;
    private bool onLoad = true;
    private bool toGoal = true;
    public Image loadImage;
    public float fadeTime = 3;
    private Color loadColour = new Color(0,0,0,1);

    void Start()
    {
        GameController.gameController.updateList.Add(this);
        GameController.gameController.allowMovement = false;
        CameraControl.camControl.followPlayer = true;
        time = fadeTime;
    }

    public void UpdateFunction()
    {
        if (onLoad)
        {
            time -= Time.deltaTime;
            if (time > 0)
            {
                loadColour.a = time / fadeTime;
                loadImage.color = loadColour;
            }
            else
            {
                loadImage.enabled = false;
                onLoad = false;
                CameraControl.camControl.moveTime = scrollTime;
                CameraControl.camControl.destination = transform.position;
                CameraControl.camControl.followPlayer = false;
            }
        }
        else
        {
            time += Time.deltaTime;
            if (time > (scrollTime + waitTime) * 2 && toGoal)
            {
                toGoal = false;
                CameraControl.camControl.followPlayer = true;
                time = 0;
            }
            if (time > (scrollTime + waitTime) * 2 && !toGoal)
            {
                GameController.gameController.removeUpdateList.Add(this);
                GameController.gameController.allowMovement = true;
                CameraControl.camControl.moveTime = 0;
            }
        }
    }
}
