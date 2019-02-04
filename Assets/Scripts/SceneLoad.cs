using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoad : MonoBehaviour, IUpdate
{
    private float time = 0;
    public float scrollTime = 1;
    public float waitTime = 1;
    private bool toGoal = true;

    void Start()
    {
        GameController.gameController.updateList.Add(this);
        GameController.gameController.allowMovement = false;
        CameraControl.camControl.followPlayer = false;
        CameraControl.camControl.moveTime = scrollTime;
        CameraControl.camControl.destination = transform.position;
    }

    public void UpdateFunction()
    {
        time += Time.deltaTime;
        if(time > (scrollTime + waitTime) * 2 && toGoal)
        {
            toGoal = false;
            CameraControl.camControl.followPlayer = true;
            time = 0;
        }
        if(time > (scrollTime + waitTime) * 2 && !toGoal)
        {
            GameController.gameController.removeUpdateList.Add(this);
            GameController.gameController.allowMovement = true;
            CameraControl.camControl.moveTime = 0;
        }
    }
}
