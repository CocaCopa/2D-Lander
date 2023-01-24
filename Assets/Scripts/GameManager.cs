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

    private void Awake() {

        instance = this;
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
        //player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(Vector3.zero), landSpeed * Time.deltaTime);
        Vector3 directionToFace = Vector3.forward;
        Quaternion targetRotation = Quaternion.LookRotation(directionToFace);
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, landSpeed * Time.deltaTime);
        player.transform.up = Vector3.RotateTowards(player.transform.up, Vector2.up, landSpeed * Time.deltaTime, 1);
        //playerRb.rotation = Mathf.Lerp(playerRb.rotation, 0, landSpeed * Time.deltaTime);
    }
}
