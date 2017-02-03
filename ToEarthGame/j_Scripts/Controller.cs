using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public bool isMobile;

    private static readonly int CONTROLLER_NORMAL = 0;
    private static readonly int CONTROLLER_PRESS = 1;
    private static readonly int CONTROLLER_PRESSING = 2;
    private static readonly int CONTROLLER_RELEASE = 3;

    private int controllerState = CONTROLLER_NORMAL;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isMobile)
        {
            detectTouch();
        } else
        {
            detectMouse();
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
                Debug.Log("CONTROLLER_PRESS");
                controllerState = CONTROLLER_PRESS;
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0) && (controllerState == CONTROLLER_PRESS || controllerState == CONTROLLER_PRESSING))
            {
                Debug.Log("CONTROLLER_RELEASE");
                controllerState = CONTROLLER_RELEASE;
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
        if(isAnyTouch && (controllerState == CONTROLLER_NORMAL || controllerState == CONTROLLER_RELEASE))
        {
            Debug.Log("CONTROLLER_PRESS");
            controllerState = CONTROLLER_PRESS;
        } else if(controllerState == CONTROLLER_PRESS || controllerState == CONTROLLER_PRESSING)
        {
            Debug.Log("CONTROLLER_RELEASE");
            controllerState = CONTROLLER_RELEASE;
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
    
    public bool isPress()
    {
        return controllerState == CONTROLLER_PRESSING;
    }
}
