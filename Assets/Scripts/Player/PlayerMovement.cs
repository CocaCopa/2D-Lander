using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("--- End of Level Behaviour ---")]
    [SerializeField] Transform landingTransform;
    [SerializeField] Vector3 landOffset = new Vector3(0,1);
    [SerializeField] float landSpeed = 0;
    [SerializeField] float rotationSpeedMultiplier = 2.5f;

    PlayerInput playerInput;
    Rigidbody2D playerRb;
    float horizontalRotation;

    private void Awake() {
        
        playerInput = GetComponent<PlayerInput>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Handles the rotation of the ship based on user input
    /// </summary>
    /// <param name="invertAxis">Should the input be inverted?</param>
    /// <param name="rotationSpeed">Speed of the rotation</param>
    /// <param name="angle">Maximum angle the ship is allowed to turn</param>
    /// <returns>The ship's new rotation</returns>
    public Quaternion HandleRotation(float rotationSpeed, float angle) {

        float horizontalInput = playerInput.GetHorizontalInput();
        horizontalRotation += horizontalInput * rotationSpeed * Time.deltaTime;
        horizontalRotation = Mathf.Clamp(horizontalRotation, -angle, angle);
        Vector3 rotation = Vector3.zero;
        rotation.z = horizontalRotation;
        return Quaternion.Euler(rotation);
    }

    /// <summary>
    /// Lands the spaceship automatically
    /// </summary>
    public void HandleAutoLanding() {

        DisableRigidbody();

        transform.position = Vector2.Lerp(transform.position, landingTransform.position + landOffset, landSpeed * Time.deltaTime);
        transform.up = Vector2.Lerp(transform.up, Vector2.up, landSpeed * rotationSpeedMultiplier * Time.deltaTime);
    }

    private void DisableRigidbody() {

        if (playerRb.simulated == true) {

            Vector2 resetVelocity = Vector2.zero;
            playerRb.velocity = resetVelocity;
            playerRb.simulated = false;
            playerRb.isKinematic = true;
        }
    }
}
