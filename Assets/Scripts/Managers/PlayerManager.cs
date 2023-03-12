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
    [SerializeField] SpaceshipData defaultData;
    public SpaceshipData GetShipData { get { return m_data; } }
    SpaceshipData m_data;

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
        InitializeShipData();
        InitializeVariables();
        PlayerState.SetCurrentState(PlayerState.Player_State.CinematicEntrance);
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

    private void InitializeVariables() {
        
        playerRB            = playerObject.GetComponent<Rigidbody2D>();
        landingTrigger      = FindObjectOfType<LandingTrigger>();
        playerCollision     = playerObject.GetComponent<PlayerCollisionCheck>();
        playerFuel          = playerObject.GetComponent<PlayerFuel>();
        playerAutoPilot     = playerObject.GetComponent<PlayerAutoPilot>();
        input               = FindObjectOfType<PlayerInput>();
        playerController    = FindObjectOfType<PlayerController>();
    }

    private void InitializeShipData() {

        m_data = SpaceshipData.m_data;
        if (m_data == null) {
            m_data = defaultData;
        }
        playerObject.GetComponentInChildren<SpriteRenderer>().sprite = m_data.shipSprite;
    }
}
