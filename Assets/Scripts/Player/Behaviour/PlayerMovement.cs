using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    #region Spaceship Stats
    float forceAmount;
    float maxSteerAngle;
    float maxVelocity;
    float rotationSpeed;
    #endregion

    Rigidbody2D playerRb;
    float horizontalRotation;

    public void InitializeVariables() {
        
        playerRb = GetComponent<Rigidbody2D>();

        PlayerManager manager = PlayerManager.instance;
        forceAmount     = manager.GetShipData.force;
        maxSteerAngle   = manager.GetShipData.maxSteerAngle;
        maxVelocity     = manager.GetShipData.maxVeclocity;
        playerRb.mass   = manager.GetShipData.shipMass;
        rotationSpeed   = manager.GetShipData.rotationSpeed;
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

    /// <summary>
    /// Moves the spaceship by force
    /// </summary>
    public void ThrottleMovement() {

        playerRb.AddForce(transform.up * forceAmount, ForceMode2D.Impulse);
        playerRb.velocity = Vector2.ClampMagnitude(playerRb.velocity, maxVelocity);
    }

    public float GetCurrentSpeed() {

        return playerRb.velocity.magnitude;
    }

    public float GetCurrentAngle() {

        return Vector2.Angle(transform.up, Vector2.up);
    }
}
