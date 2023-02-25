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

    [SerializeField] private GameObject playerObject;
    [SerializeField] private Transform playerSpawnTransform;
    // TODO: Once more spaceships will be added, a "List<>()" containing all of the different ship stats will be added as well.
    // "Playermanager" should then check which spaceship the player chose, in order to initialize "m_data" with the correct value.
    // That said, all of the scripts reading the "m_data" variable, will get the correct information.
    public SpaceshipData m_data;

    private PlayerCollisionCheck playerCollision;
    private PlayerController playerController;
    private PlayerFuel playerFuel;
    private PlayerMovement playerMovement;

    private void Awake() {

        instance = this;
        Current_State = PlayerState.Player_State.Gameplay;

        Initialize();
    }

    private void Update() {

        CheckIfPlayerIsAlive();

        switch (Current_State) {

            case PlayerState.Player_State.CinematicEntrance:
            playerController.enabled = false;
            break;

            case PlayerState.Player_State.Gameplay:
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

    private void Initialize() {

        playerCollision     = playerObject.GetComponent<PlayerCollisionCheck>();
        playerFuel          = playerObject.GetComponent<PlayerFuel>();
        playerMovement      = playerObject.GetComponent<PlayerMovement>();
        playerController    = FindObjectOfType<PlayerController>();
    }
}
