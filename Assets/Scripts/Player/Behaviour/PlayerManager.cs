using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Transform GetPlayerTransform {
        get {
            return playerObject.transform; 
        }
    }

    PlayerState.Player_State Current_State;

    [SerializeField] GameObject playerObject;
    [SerializeField] Transform playerSpawnTransform;
    // TODO: Once more spaceships will be added, a "List<>()" containing all of the different ship stats will be added as well.
    // "Playermanager" should then check which spaceship the player chose, in order to initialize "m_data" with the correct value.
    // That said, all of the scripts reading the "m_data" variable, will get the correct information.
    public SpaceshipData m_data;

    Rigidbody2D playerRB;
    PlayerCollisionCheck playerCollision;
    PlayerController playerController;
    PlayerFuel playerFuel;
    PlayerMovement playerMovement;
    PlayerInput input;

    private void Awake() {

        instance = this;
        Current_State = PlayerState.Player_State.CinematicEntrance;
        Initialize();
    }

    private void Update() {

        CheckIfPlayerIsAlive();

        switch (Current_State) {

            case PlayerState.Player_State.CinematicEntrance:
            playerRB.simulated = false;
            playerController.enabled = false;
            GameplayStateTransition();
            break;

            case PlayerState.Player_State.Gameplay:
            playerRB.simulated = true;
            playerController.enabled = true;
            break;
            
            case PlayerState.Player_State.Dead:
            playerObject.SetActive(false);
            break;
        }
    }

    private void CheckIfPlayerIsAlive() {

        if (playerCollision.PlayerDied) {

            Current_State = PlayerState.Player_State.Dead;
        }
    }

    private bool CinematicEntranceCompleted() {

        if (playerMovement.CinematicEntrance()) {
            return true;
        }
        return false;
    }

    private void GameplayStateTransition() {

        if (CinematicEntranceCompleted()) {

            if (input.GetThrottleInput()) {

                Current_State = PlayerState.Player_State.Gameplay;
            }
        }
    }

    private void Initialize() {

        playerRB            = playerObject.GetComponent<Rigidbody2D>();
        playerCollision     = playerObject.GetComponent<PlayerCollisionCheck>();
        playerFuel          = playerObject.GetComponent<PlayerFuel>();
        playerMovement      = playerObject.GetComponent<PlayerMovement>();
        input               = playerObject.GetComponent<PlayerInput>();
        playerController    = FindObjectOfType<PlayerController>();
    }
}
