using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] bool invertAxis = false;

    public float GetHorizontalInput() {

        float invert = invertAxis ? 1.0f : -1.0f;
        return Input.GetAxisRaw("Horizontal") * invert;
    }

    public bool GetThrottleInput() {

        if (Input.GetButton("Jump")) {

            return true;
        }
        return false;
    }
}
