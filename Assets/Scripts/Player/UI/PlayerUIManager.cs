using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    GameObject playerObject;

    private void Start() {

        playerObject = PlayerManager.instance.GetPlayerTransform.gameObject;
    }

    private void Update() {

        bool playerLanded = GameManager.instance.PlayerLanded;
        
        // Hide fuel images if player landed or died.
    }
}
