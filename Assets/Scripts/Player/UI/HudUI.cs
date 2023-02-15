using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    [SerializeField] Text speedText;
    [SerializeField] Text angleText;
    [SerializeField] LandingTrigger landingTrigger;
    PlayerMovement playerMovement;

    private void Start() {

        GameObject playerObject = GameManager.instance.GetPlayerTransform.gameObject;
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

        string asd = "Speed: " + speedText;
        this.speedText.text = asd;
        this.angleText.text = "Angle: " + angleText;
    }
}
