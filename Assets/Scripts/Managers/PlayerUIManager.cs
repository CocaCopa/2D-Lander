using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] Canvas fuelUI;
    GameObject playerObject;

    private void Start() {

        playerObject = PlayerManager.instance.GetPlayerTransform.gameObject;
    }

    private void Update() {

        DisableFuelUI();
    }

    private void DisableFuelUI() {

        if (fuelUI.isActiveAndEnabled == false) {
            return;
        }

        PlayerState.Player_State currentState = PlayerState.GetCurrentState();
        PlayerState.Player_State autoPilot = PlayerState.Player_State.AutoPilot;

        if (currentState == autoPilot) {

            fuelUI.gameObject.SetActive(false);
        }
    }
}
