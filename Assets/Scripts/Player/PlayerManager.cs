using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] SpaceshipData m_data;
    [SerializeField] bool invertAxis = false;

    PlayerMovement handler;
    Rigidbody2D rigidBody;

    public bool EnableAutoPilot { get; set; }

    #region Spaceship Stats variables
    float forceAmount;
    float maxSteerAngle;
    float fuelCapacity;
    float fuelConsumptionRate;
    float maxVelocity;
    float rotationSpeed;
    bool b_move = false;
    #endregion

    private void Awake() {

        rigidBody = GetComponent<Rigidbody2D>();
        handler = GetComponent<PlayerMovement>();

        InitializeStats();
    }

    private void FixedUpdate() {

        if (b_move) {

            rigidBody.AddForce(transform.up * forceAmount, ForceMode2D.Impulse);
            rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxVelocity);
        }
    }

    private void Update() {

        bool controlsEnabled = EnableAutoPilot == false && fuelCapacity > 0;

        if (controlsEnabled) {

            b_move = handler.GetMoveInput();
            transform.rotation = handler.HandleRotation(invertAxis, rotationSpeed, maxSteerAngle);
        }
        else {
            b_move = false;
        }

        if (b_move) {
            
            fuelCapacity -= fuelConsumptionRate * Time.deltaTime;
        }

        if (EnableAutoPilot) {

            handler.HandleAutoLanding();
        }
    }

    private void InitializeStats() {

        forceAmount         = m_data.force;
        maxSteerAngle       = m_data.maxSteerAngle;
        fuelCapacity        = m_data.fuelCapacity;
        fuelConsumptionRate = m_data.fuelConsumptionRate;
        maxVelocity         = m_data.maxVeclocity;
        rigidBody.mass      = m_data.shipMass;
        rotationSpeed       = m_data.rotationSpeed;
    }

    public float GetPlayerSpeed() {

        return rigidBody.velocity.magnitude;
    }

    public float GetPlayerAngle() {

        return Vector2.Angle(transform.up, Vector2.up);
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        GameManager.instance.PlayerDied = true;
    }
}
