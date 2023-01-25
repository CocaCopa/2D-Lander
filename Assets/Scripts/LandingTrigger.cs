using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingTrigger : MonoBehaviour
{
    [SerializeField] float acceptableAngle;
    [SerializeField] float acceptableSpeed;

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.transform.CompareTag("Player")) {

            GameObject player = other.gameObject;

            float playerAngle = Vector3.Angle(player.transform.up, Vector2.up);
            
            if (playerAngle > acceptableAngle) {

                GameManager.instance.PlayerDied = true;
                Debug.Log("Player Angle: " + playerAngle + " --- " + "Acceptable Angle: " + acceptableAngle);
                return;
            }

            Rigidbody2D playerRb = player.GetComponentInParent<Rigidbody2D>();
            float playerSpeed = playerRb.velocity.magnitude;

            if (playerSpeed > acceptableSpeed) {

                GameManager.instance.PlayerDied = true;
                Debug.Log("Player Speed: " + playerSpeed + " --- " + "Acceptable Speed: " + acceptableSpeed);
                return;
            }

            player.GetComponent<PlayerManager>().EnableAutoPilot = true;
            GameManager.instance.PlayerLanded = true;
        }
    }
}
