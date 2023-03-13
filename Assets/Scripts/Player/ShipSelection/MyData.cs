using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyData : MonoBehaviour
{
    public SpaceshipData GetSpaceshipData { get { return myData; } }
    [SerializeField] SpaceshipData myData;
}
