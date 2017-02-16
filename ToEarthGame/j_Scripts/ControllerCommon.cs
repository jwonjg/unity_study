using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerCommon : MonoBehaviour
{

    public bool isMobile;
    public float pressParamSpeed;

    protected float pressParam;
    protected Animator animator;

    private bool isDetect;

    private static readonly int CONTROLLER_NORMAL = 0;
    private static readonly int CONTROLLER_PRESS = 1;
    private static readonly int CONTROLLER_PRESSING = 2;
    private static readonly int CONTROLLER_RELEASE = 3;

    // TODO: 스크립트가 각 GameObject 마다 별개로 복사되는지 확인 필요
    private int controllerState = CONTROLLER_NORMAL;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMobile)
        {
            detectTouch();
        }
        else
        {
            detectMouse();
        }
    }

    void FixedUpdate()
    {
        if (isDetect)
        {
            press();
        }
        else
        {
            release();
        }
    }

    private void detectMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D coll = Physics2D.OverlapPoint(tapPoint);
            if (coll && coll.gameObject == gameObject)
            {
                isDetect = true;
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                isDetect = false;
            }
        }
    }

    private void detectTouch()
    {
        bool isAnyTouch = false;
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase != TouchPhase.Ended && isTouchGameObject(touch))
            {
                isAnyTouch = true;
                break;
            }
        }
        if (isAnyTouch && (controllerState == CONTROLLER_NORMAL || controllerState == CONTROLLER_RELEASE))
        {
            press();
        }
        else if (controllerState == CONTROLLER_PRESS || controllerState == CONTROLLER_PRESSING)
        {
            release();
        }
    }

    bool isTouchGameObject(Touch touch)
    {
        Vector2 touchDeltaPosition = touch.deltaPosition;
        Collider2D coll = Physics2D.OverlapPoint(touchDeltaPosition);
        if (coll)
        {
            return (coll.gameObject == gameObject);
        }
        else
        {
            return false;
        }
    }

    protected abstract void press();
    protected abstract void release();
}
