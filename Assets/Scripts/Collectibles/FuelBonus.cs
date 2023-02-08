using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBonus : MonoBehaviour
{
    [SerializeField] float refillAmountPercentage;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Player")) {

            PlayerManager playerManager = collision.GetComponent<PlayerManager>();
            float maxFuel = playerManager.SpaceshipFuel;
            float refillAmount = refillAmountPercentage / 100 * maxFuel;
            playerManager.SpaceshipFuel = refillAmount;

            gameObject.SetActive(false);
        }
    }
}
