using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyData : MonoBehaviour
{
    public SpaceshipData SpaceshipData { get { return myData; } }
    [SerializeField] SpaceshipData myData;
}
