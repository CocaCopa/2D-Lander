using UnityEngine;

public class LandingTrigger : MonoBehaviour
{
    [SerializeField] float acceptableAngle;
    [SerializeField] float acceptableSpeed;

    public bool LandingAccepted { get; private set; }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.transform.CompareTag("Player")) {

            GameObject player = other.gameObject;
            Rigidbody2D playerRb = player.GetComponentInParent<Rigidbody2D>();

            float playerSpeed = playerRb.velocity.magnitude;
            float playerAngle = Vector3.Angle(player.transform.up, Vector2.up);
            bool playerCanLand = playerAngle < acceptableAngle && playerSpeed < acceptableSpeed;

            if (playerCanLand) {

                LandingAccepted = true;
            }
        }
    }

    public float GetAcceptableAngle() {

        return acceptableAngle;
    }

    public float GetAcceptableSpeed() {

        return acceptableSpeed;
    }
}
