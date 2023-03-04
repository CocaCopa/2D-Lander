using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] bool invertRotationAxis = false;

    public float GetHorizontalInput() {

        float invert = invertRotationAxis ? 1.0f : -1.0f;
        return Input.GetAxisRaw("Horizontal") * invert;
    }

    public bool GetThrottleInput() {

        if (Input.GetButton("Jump")) {

            return true;
        }
        return false;
    }
}
