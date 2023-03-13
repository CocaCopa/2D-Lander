using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Player Data")]
public class SpaceshipData : ScriptableObject
{
    public static SpaceshipData m_data;

    #region Constants:
    public const float maxHealthPoints          = 100;
    public const float maxSteerAngle            = 180;
    public const float maxFuelCapacity          = 150;
    public const float maxFuelConsumptionRate   = 150;
    public const float maxShipMass              = 100;
    public const float maxForce                 = 50;
    public const float maxVeclocity             = 70;
    public const float maxRotationSpeed         = 300;
    #endregion

                                            public Sprite shipSprite;
    [Range(0.01f, maxHealthPoints)]         public float healthPoints;
    [Range(0.01f, maxFuelCapacity)]         public float fuelCapacity;
    [Range(0.01f, maxFuelConsumptionRate)]  public float fuelConsumptionRate;
    [Range(0.01f, maxRotationSpeed)]        public float rotationSpeed;
    [Range(0.01f, maxSteerAngle)]           public float steerAngle;
    [Range(0.01f, maxVeclocity)]            public float maximumVeclocity;
    [Range(0.01f, maxShipMass)]             public float shipMass;
    [Range(0.01f, maxForce)]                public float force;

    #region Spaceship Selection UI:
    public float MaxHealthPoints() {
        return maxHealthPoints;
    }
    public float MaxSteerAngle() {
        return maxSteerAngle;
    }
    public float MaxFuelCapacity() {
        return maxFuelCapacity;
    }
    public float MaxFuelConsumptionRate() {
        return maxFuelConsumptionRate;
    }
    public float MaxShipMass() {
        return maxShipMass;
    }
    public float MaxForce() {
        return maxForce;
    }
    public float MaxVeclocity() {
        return maxVeclocity;
    }
    public float MaxRotationSpeed() {
        return maxRotationSpeed;
    }
    #endregion
}
