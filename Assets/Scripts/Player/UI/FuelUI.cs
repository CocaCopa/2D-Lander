using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    [SerializeField] Image fuelImageLeft;
    [SerializeField] Image fuelImageRight;
    [SerializeField] Gradient fuelBarColor;

    PlayerFuel playerFuel;

    private void Awake() {
        
        playerFuel = GetComponent<PlayerFuel>();
    }

    private void Update() {

        BarFillAmount();
        BarGradientColor();
    }

    private void BarFillAmount() {

        fuelImageLeft.fillAmount = playerFuel.GetRemainingFuel / playerFuel.GetMaxFuel;
        fuelImageRight.fillAmount = playerFuel.GetRemainingFuel / playerFuel.GetMaxFuel;
    }

    private void BarGradientColor() {

        fuelImageLeft.color = fuelBarColor.Evaluate(fuelImageLeft.fillAmount);
        fuelImageRight.color = fuelBarColor.Evaluate(fuelImageRight.fillAmount);
    }
}
