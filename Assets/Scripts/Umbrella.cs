using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : Collectible, IUpdate
{
    private DistanceJoint2D hinge;
    private Rigidbody2D player;
    private bool isActive = false;
    private int charType;
    private Collider2D childCollide;
    private float time = 0;
    public float timeoutTime = 30;
    private string arm;

    private void Start()
    {
        hinge = gameObject.GetComponent<DistanceJoint2D>();
        charType = GameController.gameController.charType;
        switch (charType)
        {
            case 0:
                arm = "RightLimb";
                break;
            case 1:
                arm = "Joint1";
                break;
            case 2:
                arm = "RightArm/RightArm1/RightArm2/RightArm3/RightHand";
                break;
        }
        player = PlayerObject.playerObject.gameObject.transform.Find(arm).GetComponent<Rigidbody2D>();
        childCollide = transform.Find("Handle").GetComponent<Collider2D>();
    }

    protected override void Collect()
    {
        if (!isActive)
        {
            isActive = true;
            hinge.enabled = true;
            hinge.connectedBody = player;
            childCollide.enabled = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = -4;
            gameObject.GetComponent<FlingableObject>().enabled = true;
            GameController.gameController.updateList.Add(this);
        }
    }

    public void UpdateFunction()
    {
        time += Time.deltaTime;
        if(time > timeoutTime)
        {
            GameController.gameController.removeUpdateList.Add(this);
            GameController.gameController.removeUpdateList.Add(gameObject.GetComponent<FlingableObject>());
            Destroy(gameObject);
        }
    }
}
