using UnityEngine;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    [SerializeField] Text speedText;
    [SerializeField] Text angleText;
    LandingTrigger landingTrigger;
    PlayerMovement playerMovement;

    private void Awake() {
        
        landingTrigger = FindObjectOfType<LandingTrigger>();
    }

    private void Start() {

        GameObject playerObject = PlayerManager.instance.GetPlayerTransform.gameObject;
        playerMovement = playerObject.GetComponent<PlayerMovement>();
    }

    private void Update() {

        float acceptableAngle = landingTrigger.GetAcceptableAngle();
        float acceptableSpeed = landingTrigger.GetAcceptableSpeed();

        float playerAngle = playerMovement.GetCurrentAngle();
        float playerSpeed = playerMovement.GetCurrentSpeed();

        string speedText = "OK";
        string angleText = "OK";

        if (playerAngle > acceptableAngle) {

            angleText = "WARNING";
        }
        if (playerSpeed > acceptableSpeed) {

            speedText = "WARNING";
        }

        string speed = "Speed: " + speedText;
        string angle = "Angle: " + angleText;

        this.speedText.text = speed;
        this.angleText.text = angle;
    }
}
