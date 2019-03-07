using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IUpdate
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
    private bool wait = false;
    private int positionQuantity;
    private int location = 0;
    private Vector3 currentVelocity;
    private Vector2 prevPosition;
    private float time = 0f;

    void Awake()
    {
        positionQuantity = positions.Length;
        prevPosition = positions[positionQuantity - 1];
    }

    void Start()
    {
        GameController.gameController.updateList.Add(this);
    }

    public void UpdateFunction()
    {
        if (wait)
        {
            time += Time.deltaTime;
            if (time > waitTime) { wait = false; time = 0; }
        }
        else
        {
            if (smoothDampMove)
            {
                transform.position = Vector3.SmoothDamp(transform.position, positions[location], ref currentVelocity, moveTime);
            }
            else
            {
                time += Time.deltaTime;
                transform.position = Vector3.Lerp(prevPosition, positions[location], time / moveTime);
            }
            if (smoothDampMove ? ((positions[location] - (Vector2)transform.position).sqrMagnitude < 0.1f) : (time / moveTime >= 1))
            {
                wait = true;
                prevPosition = positions[location];
                if (backtrack && location == 0 && backtrackDir == -1)
                {
                    backtrackDir = 1;
                }
                if (location == positionQuantity - 1)
                {
                    if (backtrack && backtrackDir == 1) { backtrackDir = -1; location += backtrackDir; }
                    else { location = 0; }
                }
                else { location += backtrackDir; }
                if (uniquePauses) { waitTime = waitTimes[location]; }
                if (uniqueTravelTime) { moveTime = moveTimes[location]; }
                time = 0;
            }
        }
    }
}
