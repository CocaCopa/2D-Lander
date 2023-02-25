using UnityEngine;

public class FuelBonus : MonoBehaviour
{
    [SerializeField] float refillAmountPercentage;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Player")) {

            PlayerFuel playerFuel = collision.GetComponent<PlayerFuel>();
            float maxFuel = playerFuel.AddBonusFuel;
            float refillAmount = refillAmountPercentage / 100 * maxFuel;
            playerFuel.AddBonusFuel = refillAmount;

            gameObject.SetActive(false);
        }
    }
}
