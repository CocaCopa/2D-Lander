using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    GameObject playerObject;
    PlayerMovement playerMovement;
    PlayerInput playerInput;
    PlayerFuel playerFuel;
    LandingTrigger landingTrigger;

    bool throttleInput = false;
    bool controlsEnabled = true;

    private void Awake() {

        instance = this;
        Initialze();
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

    private void Initialze() {

        playerObject = PlayerManager.instance.GetPlayerTransform.gameObject;
        landingTrigger  = FindObjectOfType<LandingTrigger>();
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