using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    CinemachineBrain camBrain;

    private void Awake() {

        camBrain = Camera.main.GetComponent<CinemachineBrain>();
    }

    private void Update() {

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
        }
    }

    private void CinematicState() {
        camBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.LateUpdate;
    }

    private void GameplayState() {
        camBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
    }

    private void AutoPilotState() {
        camBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.LateUpdate;
    }
}
