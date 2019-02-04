using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : Collectible
{
    private DistanceJoint2D hinge;
    private Rigidbody2D player;

    private void Start()
    {
        hinge = gameObject.GetComponent<DistanceJoint2D>();
        player = PlayerObject.playerObject.gameObject.transform.Find("RightArm").GetComponent<Rigidbody2D>();
    }

    protected override void Collect()
    {
        hinge.enabled = true;
        hinge.connectedBody = player;
        gameObject.GetComponent<Rigidbody2D>().mass = 10;
    }
}
