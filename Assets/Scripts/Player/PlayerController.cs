using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private GameObject playerObject;
    // TODO: Once more spaceships will be added, a "List<>()" containing all of the different ship stats will be added as well.
    // "Playermanager" should then check which spaceship the player chose, in order to initialize "m_data" with the correct value.
    // That said, all of the scripts reading the "m_data" variable, will get the correct information.
    public SpaceshipData m_data;

    PlayerMovement playerMovement;
    PlayerInput playerInput;
    PlayerFuel playerFuel;
    LandingTrigger landingTrigger;

    bool throttleInput = false;
    bool controlsEnabled = true;

    private void Awake() {

        instance = this;

        InitialzeComponentVariables();
        playerFuel.InitializeVariables();
        playerMovement.InitializeVariables();
    }

    private void FixedUpdate() {

        if (controlsEnabled && throttleInput) {

            playerMovement.ThrottleMovement();
        }
    }

    private void Update() {

        bool enableAutoPilot = landingTrigger.LandingAccepted;
        controlsEnabled = enableAutoPilot == false && playerFuel.GetRemainingFuel > 0;

        if (controlsEnabled) {

            this.ManageMovement();
        }

        if (enableAutoPilot) {

            playerMovement.HandleAutoLanding();
        }
    }

    private void InitialzeComponentVariables() {

        GameObject landArea = GameObject.FindGameObjectWithTag("LandingTrigger");

        landingTrigger  = landArea.GetComponent<LandingTrigger>();
        playerMovement  = playerObject.GetComponent<PlayerMovement>();
        playerInput     = playerObject.GetComponent<PlayerInput>();
        playerFuel      = playerObject.GetComponent<PlayerFuel>();
    }

    private void ManageMovement() {

        playerMovement.HandleRotation(playerInput.GetHorizontalInput());
        throttleInput = playerInput.GetThrottleInput();

        if (throttleInput) {

            playerFuel.ConsumeFuel();
        }
    }
}