using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlingableObject : MonoBehaviour, IUpdate, IFixedUpdate
{
    private bool flingActive = false;
    private Vector2 direction;
    private Vector2 startPos;
    private Rigidbody2D rb;
    private Vector2 zero;
    private Camera mainCam;
    private Vector2[] speedMeasure = new Vector2[10];
    private int mouseAge = 0;
#if UNITY_STANDALONE
    private bool aiming = false;
#endif
#if UNITY_IOS || UNITY_ANDROID
    private Touch touch;
#endif

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        zero = Vector2.zero;
    }

    private void Start()
    {
        GameController.gameController.updateList.Add(this);
        GameController.gameController.fixedUpdateList.Add(this);
        mainCam = CameraControl.mainCam;
    }

    public void UpdateFunction()
    {
        if (GameController.gameController.allowMovement)
        {
#if UNITY_STANDALONE
            if (Input.GetMouseButtonDown(0))
            {
                if (((Vector2)transform.position - (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition)).sqrMagnitude < 1)
                {
                    mouseAge = 0;
                    startPos = speedMeasure[9];
                    Debug.Log("Start: " + startPos);
                    aiming = true;
                }
            }
            if (Input.GetMouseButtonUp(0) && aiming == true)
            {
                direction = speedMeasure[9] - (mouseAge > 10 ? speedMeasure[0] : startPos);
                Debug.Log("End: " + direction);
                flingActive = true;
                aiming = false;
            }
#endif
#if UNITY_IOS || UNITY_ANDROID
            if(Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (((Vector2)transform.position - (Vector2)mainCam.ScreenToWorldPoint(touch.position)).sqrMagnitude < 9)
                        {
                            mouseAge = 0;
                            startPos = speedMeasure[9];
                            Debug.Log("Start: " + startPos);
                        }
                        break;
                    case TouchPhase.Ended:
                        direction = speedMeasure[9] - (mouseAge > 10 ? speedMeasure[0] : startPos);
                        Debug.Log("End: " + direction);
                        flingActive = true;
                        break;
                }
            }
#endif
        }
    }

    public void FixedUpdateFunction()
    {
        mouseAge++;
        for(int i = 0; i < 10; i++)
        {
            if (i == 9)
            {
                speedMeasure[i] = Input.mousePosition;
            }
            else
            {
                speedMeasure[i] = speedMeasure[i + 1];
            }
        }
        if (flingActive)
        {
            rb.AddForce(direction);
            flingActive = false;
            direction = zero;
        }
    }
}
