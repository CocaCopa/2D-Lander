using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] SpaceshipData m_data;

    PlayerMovement handler;
    PlayerInput playerInput;
    Rigidbody2D rigidBody;
    LandingTrigger landingTrigger;

    public float SpaceshipFuel {
        get {
            return fuelMaxCapacity; 
        }
        set {
            remainingFuel += value;
            remainingFuel = Mathf.Clamp(remainingFuel, 0, fuelMaxCapacity);
        }
    }

    #region Spaceship Stats variables
    float forceAmount;
    float maxSteerAngle;
    float fuelMaxCapacity;
    float fuelConsumptionRate;
    float maxVelocity;
    float rotationSpeed;
    bool throttleInput = false;
    #endregion

    float remainingFuel;
    bool controlsEnabled = true;

    private void Awake() {

        InitialzeComponentVariables();
        InitializeStatsVariables();

        remainingFuel = fuelMaxCapacity;
    }

    private void FixedUpdate() {

        if (controlsEnabled && throttleInput) {

            // TODO:
            // Move the following code to PlayerMovement. Doesn't make sense to be implemented here.
            // How to:
            // Idea 1 -> Create a "PlayerVariables" script so that "PlayerMovement" will be able to see "forceAmount" and "maxVelocity"
            // Idea 2 -> ???
            rigidBody.AddForce(transform.up * forceAmount, ForceMode2D.Impulse);
            rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxVelocity);
        }
    }

    private void Update() {

        bool enableAutoPilot = landingTrigger.LandingAccepted;
        controlsEnabled = enableAutoPilot == false && remainingFuel > 0;

        if (controlsEnabled) {

            ManageMovement();
        }        

        if (enableAutoPilot) {

            handler.HandleAutoLanding();
        }

        Debug.Log(remainingFuel);
    }

    private void InitializeStatsVariables() {

        forceAmount         = m_data.force;
        maxSteerAngle       = m_data.maxSteerAngle;
        fuelMaxCapacity     = m_data.fuelCapacity;
        fuelConsumptionRate = m_data.fuelConsumptionRate;
        maxVelocity         = m_data.maxVeclocity;
        rigidBody.mass      = m_data.shipMass;
        rotationSpeed       = m_data.rotationSpeed;
    }

    private void InitialzeComponentVariables() {

        GameObject landArea = GameObject.FindGameObjectWithTag("LandingTrigger");
        landingTrigger = landArea.GetComponent<LandingTrigger>();
        rigidBody = GetComponent<Rigidbody2D>();
        handler = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void ManageMovement() {

        transform.rotation = handler.HandleRotation(rotationSpeed, maxSteerAngle);
        throttleInput = playerInput.GetThrottleInput();

        if (throttleInput) {

            remainingFuel -= fuelConsumptionRate * Time.deltaTime;
        }
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
