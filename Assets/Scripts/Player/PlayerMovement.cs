using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("--- End of Level Behaviour ---")]
    [SerializeField] Transform landingTransform;
    [SerializeField] Vector3 landOffset = new Vector3(0,1);
    [SerializeField] float landSpeed = 0;
    [SerializeField] float rotationSpeedMultiplier = 2.5f;

    float forceAmount;
    float maxSteerAngle;
    float maxVelocity;
    float rotationSpeed;

    Rigidbody2D playerRb;
    float horizontalRotation;

    public void InitializeVariables() {
        
        playerRb = GetComponent<Rigidbody2D>();

        PlayerController manager = PlayerController.instance;
        forceAmount     = manager.m_data.force;
        maxSteerAngle   = manager.m_data.maxSteerAngle;
        maxVelocity     = manager.m_data.maxVeclocity;
        playerRb.mass   = manager.m_data.shipMass;
        rotationSpeed   = manager.m_data.rotationSpeed;
    }

    /// <summary>
    /// Handles the rotation of the ship based on user input
    /// </summary>
    /// <param name="invertAxis">Should the input be inverted?</param>
    /// <param name="rotationSpeed">Speed of the rotation</param>
    /// <param name="angle">Maximum angle the ship is allowed to turn</param>
    /// <returns>The ship's new rotation</returns>
    public Quaternion HandleRotation(float horizontalInput) {

        horizontalRotation += horizontalInput * rotationSpeed * Time.deltaTime;
        horizontalRotation = Mathf.Clamp(horizontalRotation, -maxSteerAngle, maxSteerAngle);
        Vector3 rotation = Vector3.zero;
        rotation.z = horizontalRotation;
        transform.rotation = Quaternion.Euler(rotation);
        return Quaternion.Euler(rotation);
    }

    public void ThrottleMovement() {

        playerRb.AddForce(transform.up * forceAmount, ForceMode2D.Impulse);
        playerRb.velocity = Vector2.ClampMagnitude(playerRb.velocity, maxVelocity);
    }

    /// <summary>
    /// Lands the spaceship automatically
    /// </summary>
    public void HandleAutoLanding() {

        DisableRigidbody();

        Vector3 current = transform.position;
        Vector3 target = landingTransform.position + landOffset;
        float lerpTime = landSpeed * Time.deltaTime;
        // Set Position
        transform.position = Vector2.Lerp(current, target, lerpTime);

        current = transform.up;
        target = Vector3.up;
        lerpTime = landSpeed * rotationSpeedMultiplier * Time.deltaTime;
        // Set Angle
        transform.up = Vector2.Lerp(current, target, lerpTime);
    }

    public float GetCurrentSpeed() {

        return playerRb.velocity.magnitude;
    }

    public float GetCurrentAngle() {

        return Vector2.Angle(transform.up, Vector2.up);
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
