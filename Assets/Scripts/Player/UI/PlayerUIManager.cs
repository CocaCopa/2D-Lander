using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    GameObject playerObject;

    private void Start() {

        playerObject = PlayerManager.instance.GetPlayerTransform.gameObject;
    }

}
