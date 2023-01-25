using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("--- End of Level Behaviour ---")]
    [SerializeField] Transform landingTransform;
    [SerializeField] Vector3 landOffset = new Vector3(0,1);
    [SerializeField] float landSpeed = 0;

    float horizontalRotation;

    /// <summary>
    /// Handles the rotation of the ship based on user input
    /// </summary>
    /// <param name="invertAxis">Should the input be inverted?</param>
    /// <param name="rotationSpeed">Speed of the rotation</param>
    /// <param name="angle">Maximum angle the ship is allowed to turn</param>
    /// <returns>The ship's new rotation</returns>
    public Quaternion HandleRotation(bool invertAxis, float rotationSpeed, float angle) {

        float invert = invertAxis ? 1 : -1;
        float horizontalInput = Input.GetAxisRaw("Horizontal") * invert;
        horizontalRotation += horizontalInput * rotationSpeed * Time.deltaTime;
        if (horizontalRotation > 360)
            horizontalRotation = 0;
        horizontalRotation = Mathf.Clamp(horizontalRotation, -angle, angle);
        Vector3 rotation = Vector3.zero;
        rotation.z = horizontalRotation;
        return Quaternion.Euler(rotation);
    }

    /// <summary>
    /// Lands the spaceship automatically
    /// </summary>
    public void HandleAutoLanding() {

        Rigidbody2D playerRb = GetComponent<Rigidbody2D>();

        Vector2 resetVelocity = Vector2.zero;
        playerRb.velocity = resetVelocity;
        playerRb.isKinematic = true;

        transform.position = Vector2.Lerp(transform.position, landingTransform.position + landOffset, landSpeed * Time.deltaTime);
        transform.up = Vector2.Lerp(transform.up, Vector2.up, landSpeed * 2.5f * Time.deltaTime);
    }

    public bool GetMoveInput() {

        if (Input.GetButton("Jump")) {

            return true;
        }
        return false;
    }
}
