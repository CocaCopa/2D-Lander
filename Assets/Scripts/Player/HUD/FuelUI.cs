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

        fuelImageLeft.fillAmount = 
        fuelImageRight.fillAmount = playerFuel.GetRemainingFuel / playerFuel.GetMaxFuel;
    }

    private void BarGradientColor() {

        fuelImageLeft.color =
        fuelImageRight.color = fuelBarColor.Evaluate(fuelImageLeft.fillAmount);
    }
}
