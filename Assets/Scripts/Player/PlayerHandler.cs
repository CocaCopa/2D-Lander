using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    float horizontalRotation;

    public Quaternion HandleRotation(bool invertAxis, float rotationSpeed) {

        float invert = invertAxis ? 1 : -1;
        float horizontalInput = Input.GetAxisRaw("Horizontal") * invert;
        horizontalRotation += horizontalInput * rotationSpeed * Time.deltaTime;
        Vector3 rotattion = Vector3.zero;
        rotattion.z = horizontalRotation;
        return Quaternion.Euler(rotattion);
    }

    public bool GetMoveInput() {

        if (Input.GetButton("Jump")) {

            return true;
        }
        return false;
    }
}
