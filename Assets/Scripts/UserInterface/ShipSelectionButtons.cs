using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSelectionButtons : MonoBehaviour
{
    public void NextSpaceship() {

        SpaceshipSelection spaceshipSelection = FindObjectOfType<SpaceshipSelection>();
        spaceshipSelection.SwipeLeft(true);
        spaceshipSelection.ToggleStatsUI(true);
    }

    public void PreviousSpaceship() {

        SpaceshipSelection spaceshipSelection = FindObjectOfType<SpaceshipSelection>();
        spaceshipSelection.SwipeLeft(false);
        spaceshipSelection.ToggleStatsUI(false);
    }

    public void SelectSpaceship() {

        Collider2D ship = Physics2D.OverlapCircle(Vector2.zero, 0.1f);
        SpaceshipData.m_data = ship.GetComponent<MyData>().GetSpaceshipData;
        ManageScenes.instance.StartGame();
    }
}
