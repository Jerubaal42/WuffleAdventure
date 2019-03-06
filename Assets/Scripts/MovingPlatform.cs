using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IUpdate, IFixedUpdate
{
    public bool smoothDampMove = false;
    public bool uniquePauses = false;
    public bool uniqueTravelTime = false;
    public bool backtrack = false;
    private int backtrackDir = 1;
    public Vector2[] positions;
    public float[] waitTimes;
    public float[] moveTimes;
    public float waitTime = 0;
    public float moveTime = 1;
    private int positionQuantity;
    private int location = 0;

    void Awake()
    {
        positionQuantity = positions.Length;
    }

    void Start()
    {
        GameController.gameController.updateList.Add(this);
        GameController.gameController.fixedUpdateList.Add(this);
    }

    public void UpdateFunction()
    {
        if (smoothDampMove)
        {

        }
        else
        {
        }
        if((positions[location] - (Vector2)transform.position).sqrMagnitude < 0.1f)
        {
            if(location == positionQuantity - 1)
            {
                if (backtrack) { backtrackDir = -1; }
                else { location = 0; }
            }
            else if(backtrack && location == 0 && backtrackDir == -1)
            {

            }
            else
            {

            }
        }
    }

    public void FixedUpdateFunction()
    {
    }
}
