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
        horizontalRotation = Mathf.Clamp(horizontalRotation, -89, 89);
        Vector3 rotation = Vector3.zero;
        rotation.z = horizontalRotation;
        return Quaternion.Euler(rotation);
    }

    public bool GetMoveInput() {

        if (Input.GetButton("Jump")) {

            return true;
        }
        return false;
    }
}
