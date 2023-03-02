using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    GameObject playerObject;
    PlayerMovement playerMovement;
    PlayerInput playerInput;
    PlayerFuel playerFuel;

    bool throttleInput = false;

    private void Awake() {

        instance = this;
        Initialize();
        playerFuel.InitializeVariables();
        playerMovement.InitializeVariables();
    }

    private void FixedUpdate() {

        if (throttleInput) {

            playerMovement.ThrottleMovement();
        }
    }

    private void Update() {

        ManageMovement();
    }

    private void Initialize() {

        playerObject = PlayerManager.instance.GetPlayerTransform.gameObject;
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