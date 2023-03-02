using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceSpaceship : MonoBehaviour
{
    [SerializeField] float forceAmount;
    [SerializeField] float fuelConsumePercentage;

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.CompareTag("Player")) {
            
            GameObject playerObject = collision.gameObject;
            PlayerFuel playerFuel = playerObject.GetComponent<PlayerFuel>();

            if (playerFuel.GetRemainingFuel > 0) {
                PushPlayerAway(playerObject, collision);
                ConsumeFuelFromPlayer(playerFuel);
            }
        }
    }

    private void PushPlayerAway(GameObject playerObject, Collision2D collision) {

        Rigidbody2D playerRb = playerObject.GetComponent<Rigidbody2D>();
        Vector3 resetVelocity = Vector3.zero;

        foreach (var contact in collision.contacts) {

            playerRb.velocity = resetVelocity;
            playerRb.AddForce(contact.normal * -forceAmount, ForceMode2D.Impulse);
        }
    }

    private void ConsumeFuelFromPlayer(PlayerFuel playerFuel) {

        float maxFuel = playerFuel.AddFuel;
        float consumeAmount = -fuelConsumePercentage / 100 * maxFuel;
        playerFuel.AddFuel = consumeAmount;
    }
}