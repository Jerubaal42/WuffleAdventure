using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour, IUpdate
{
    public Vector2 endLocation;
    public float waitTime = 3;
    private float time;
    private float moveTime = 2;
    private bool waitStart = true;
    private bool waitEnd = false;
    public Sprite mouseUp;
    public Sprite mouseDown;
    private SpriteRenderer render;
    public bool active;
    public static Tutorial tutorial;
    public GameObject cursor;
    public Vector2 startPos;

    private void Awake()
    {
        render = cursor.GetComponent<SpriteRenderer>();
        tutorial = this;
    }

    private void Start()
    {
        GameController.gameController.updateList.Add(this);
    }

    public void UpdateFunction()
    {
        if (active)
        {
            time += Time.deltaTime;
            cursor.transform.rotation = Quaternion.Euler(Vector3.zero);
            if (waitStart)
            {
                if ((time > waitTime / 2) && render.sprite != mouseDown) { render.sprite = mouseDown; }
                if (time > waitTime) { time = 0; waitStart = false; }
            }
            else if (waitEnd)
            {
                if ((time > waitTime / 2) && render.sprite != mouseUp) { render.sprite = mouseUp; }
                if (time > waitTime) { cursor.transform.localPosition = startPos; time = 0; waitStart = true; waitEnd = false; }
            }
            else
            {
                cursor.transform.localPosition = Vector2.Lerp(startPos, endLocation, time / moveTime);
                if (time / moveTime >= 1) { waitEnd = true; time = 0; }
            }
        }
    }

    public void Deactivate() { active = false; cursor.SetActive(false); }
    public void Activate() { active = true; cursor.SetActive(true); }
}
