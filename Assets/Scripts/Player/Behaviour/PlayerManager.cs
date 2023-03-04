using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Transform GetPlayerTransform {
        get {
            return playerObject.transform; 
        }
    }

    [SerializeField] GameObject playerObject;
    // TODO: Once more spaceships will be added, a "List<>()" containing all of the different ship stats will be added as well.
    // "Playermanager" should then check which spaceship the player chose, in order to initialize "m_data" with the correct value.
    // That said, all of the scripts reading the "m_data" variable, will get the correct information.
    public SpaceshipData m_data;

    Rigidbody2D playerRB;
    LandingTrigger landingTrigger;
    PlayerCollisionCheck playerCollision;
    PlayerController playerController;
    PlayerFuel playerFuel;
    PlayerAutoPilot playerAutoPilot;
    PlayerInput input;

    bool playerOutOfFuel = false;

    private void Awake() {

        instance = this;
        PlayerState.SetCurrentState(PlayerState.Player_State.CinematicEntrance);
        Initialize();
    }

    private void Update() {

        CheckIfPlayerIsAlive();
        AutoPilotEnabled();
        ManagePlayerStates();
    }

    private void CheckIfPlayerIsAlive() {

        playerOutOfFuel = playerFuel.GetRemainingFuel <= 0;

        if (playerCollision.PlayerDied || playerOutOfFuel) {

            PlayerState.SetCurrentState(PlayerState.Player_State.Dead);
        }
    }

    private void ManagePlayerStates() {

        switch (PlayerState.GetCurrentState()) {

            case PlayerState.Player_State.CinematicEntrance:
            CinematicState();
            break;

            case PlayerState.Player_State.Gameplay:
            GameplayState();
            break;

            case PlayerState.Player_State.AutoPilot:
            AutoPilotState();
            break;

            case PlayerState.Player_State.Dead:
            DeadState();
            break;
        }
    }

    private bool CinematicEntranceCompleted() {

        if (playerAutoPilot.CinematicEntrance()) {
            return true;
        }
        return false;
    }

    private void GameplayStateTransition() {

        if (CinematicEntranceCompleted()) {

            if (input.GetThrottleInput()) {

                PlayerState.SetCurrentState(PlayerState.Player_State.Gameplay);
            }
        }
    }

    private void CinematicState() {

        playerRB.simulated = false;
        playerController.enabled = false;
        GameplayStateTransition();
    }

    private void GameplayState() {

        playerRB.simulated = true;
        playerController.enabled = true;
    }

    private void AutoPilotState() {

        playerRB.simulated = false;
        playerController.enabled = false;
        playerAutoPilot.HandleAutoLanding();
    }

    private void DeadState() {

        if (playerOutOfFuel) {

            playerRB.simulated = true;
            playerController.enabled = false;
        }
        else {

            playerObject.SetActive(false);
        }
    }

    private void AutoPilotEnabled() {

        bool enableAutoPilot = landingTrigger.LandingAccepted;

        if (enableAutoPilot) {

            PlayerState.SetCurrentState(PlayerState.Player_State.AutoPilot);
        }
    }

    private void Initialize() {

        playerRB            = playerObject.GetComponent<Rigidbody2D>();
        landingTrigger      = FindObjectOfType<LandingTrigger>();
        playerCollision     = playerObject.GetComponent<PlayerCollisionCheck>();
        playerFuel          = playerObject.GetComponent<PlayerFuel>();
        playerAutoPilot  = playerObject.GetComponent<PlayerAutoPilot>();
        input               = playerObject.GetComponent<PlayerInput>();
        playerController    = FindObjectOfType<PlayerController>();
    }
}
