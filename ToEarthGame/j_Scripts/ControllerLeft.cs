using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ControllerLeft : ControllerCommon
{

    private bool isPressed;

    public bool Pressed()
    {
        return isPressed = true;
    }

    public bool Released()
    {
        return isPressed = false;
    }

    public bool IsPressed()
    {
        return isPressed;
    }

    protected override void press()
    {
        if (pressParam > -0.8f)
        {
            pressParam -= pressParamSpeed;
        }
        else
        {
            pressParam = -0.8f;
        }
        animator.SetFloat("Pressed", pressParam);
    }

    protected override void release()
    {
        if (pressParam < 0.0f)
        {
            pressParam += pressParamSpeed;
        }
        else
        {
            pressParam = 0.0f;
        }
        animator.SetFloat("Pressed", pressParam);
    }
}
