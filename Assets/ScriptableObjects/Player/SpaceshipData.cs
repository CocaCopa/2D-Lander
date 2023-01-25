using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")]
public class SpaceshipData : ScriptableObject
{
    public float healthPoints;
    public float maxSteerAngle;
    public float fuelCapacity;
    public float fuelConsumptionRate;
    public float shipMass;
    public float force;
    public float maxVeclocity;
    public float rotationSpeed;
}
