using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpike : MonoBehaviour, IUpdate, IFixedUpdate
{

    private int layermask;
    private bool active = false;
    private Collider2D[] playerCheck = new Collider2D[1];
    public float radius = 5;
    public float spinSpeed;
    private Animator animator;
    private Rigidbody2D rb;

    void Awake()
    {
        layermask = 1 << 8;
        animator = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        GameController.gameController.updateList.Add(this);
        GameController.gameController.fixedUpdateList.Add(this);
    }

    public void UpdateFunction()
    {
        Physics2D.OverlapCircleNonAlloc(transform.position, radius, playerCheck, layermask);
        if (playerCheck[0] != null) { active = true; animator.SetBool("Active", true); }
        else { active = false; animator.SetBool("Active", false); }
        Array.Clear(playerCheck, 0, 1);
    }

    public void FixedUpdateFunction()
    {
        if (active) { rb.angularVelocity = spinSpeed; }
        else { rb.angularVelocity = 0; }
    }
}
