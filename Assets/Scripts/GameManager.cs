using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject player;
    [SerializeField] Transform landingTransform;
    [SerializeField] Vector3 landOffset = new Vector3(0,1);
    [SerializeField] float landSpeed = 0;

    [HideInInspector] public bool playerLanded = false;
    [HideInInspector] public bool playerDied = false;

    LandingTrigger landingTrigger;

    private void Awake() {

        instance = this;

        landingTrigger = GameObject.Find("LandingTrigger").GetComponent<LandingTrigger>();
    }

    private void Update() {
        
        if (playerLanded) {

            HandlePlayerLanding();
        }
    }
    
    private void HandlePlayerLanding() {

        player.GetComponent<PlayerManager>().enabled = false;
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();

        Vector2 resetVelocity = Vector2.zero;
        playerRb.velocity = resetVelocity;
        playerRb.isKinematic = true;

        player.transform.position = Vector2.Lerp(player.transform.position, landingTransform.position + landOffset, landSpeed * Time.deltaTime);

        float angle = Vector3.SignedAngle(player.transform.up, Vector3.up, Vector3.up);
        Vector3 target = Quaternion.AngleAxis(-angle, player.transform.up) * Vector2.up;
        float rotationSpeed = angle;
        rotationSpeed = Mathf.Clamp(rotationSpeed, landSpeed * 2, angle / 10);
        player.transform.up = Vector2.Lerp(player.transform.up, target, rotationSpeed * Time.deltaTime);
    }
}
