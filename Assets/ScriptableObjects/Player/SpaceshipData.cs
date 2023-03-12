using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Player Data")]
public class SpaceshipData : ScriptableObject
{
    public static SpaceshipData m_data;

    public Sprite shipSprite;
    public float healthPoints;
    public float maxSteerAngle;
    public float fuelCapacity;
    public float fuelConsumptionRate;
    public float shipMass;
    public float force;
    public float maxVeclocity;
    public float rotationSpeed;
}
