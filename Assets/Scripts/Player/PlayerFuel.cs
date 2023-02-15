using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFuel : MonoBehaviour
{
    float fuelMaxCapacity;
    float fuelConsumptionRate;
    float remainingFuel;

    public float AddBonusFuel {
        get {
            return fuelMaxCapacity;
        }
        set {
            remainingFuel += value;
            remainingFuel = Mathf.Clamp(remainingFuel, 0, fuelMaxCapacity);
        }
    }

    public float GetRemainingFuel {
        get {
            return remainingFuel;
        }
    }

    public void InitializeVariables() {

        SpaceshipData m_data = PlayerController.instance.m_data;
        fuelMaxCapacity = m_data.fuelCapacity;
        fuelConsumptionRate = m_data.fuelConsumptionRate;
        remainingFuel = fuelMaxCapacity;
    }

    public void ConsumeFuel() {

        remainingFuel -= fuelConsumptionRate * Time.deltaTime;
    }
}
