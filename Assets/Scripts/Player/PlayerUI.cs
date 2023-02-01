using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Text speedText;
    [SerializeField] Text angleText;
    [SerializeField] LandingTrigger landingTrigger;
    PlayerManager playerManager;

    private void Awake() {
        
        playerManager = GetComponent<PlayerManager>();
    }

    private void Update() {

        float acceptableAngle = landingTrigger.GetAcceptableAngle();
        float acceptableSpeed = landingTrigger.GetAcceptableSpeed();
        float playerAngle = playerManager.GetPlayerAngle();
        float playerSpeed = playerManager.GetPlayerSpeed();

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
