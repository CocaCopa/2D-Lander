using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipSelection : MonoBehaviour
{
    [SerializeField] GameObject shipHolder;
    [SerializeField] float shipSpacing = 15;
    [SerializeField] float UISwipeSpeed = 8;

    public GameObject ShipHolder { get { return shipHolder; } }
    public float ShipSpacing { get { return shipSpacing; } }

    List<GameObject> statsUI = new List<GameObject>();
    Vector3 offset;
    Vector3 targetPosition;
    int shipIndex = 0;

    #region Private:
    private void Awake() {

        InitializeStatsList();
        offset = new Vector3(shipSpacing, 0, 0);
    }

    private void Update() {

        shipHolder.transform.position = NextUiPosition();
    }

    private Vector3 NextUiPosition() {

        Vector3 currentPosition = shipHolder.transform.position;
        float lerpTime = UISwipeSpeed * Time.deltaTime;
        return Vector2.Lerp(currentPosition, targetPosition, lerpTime);
    }

    private void InitializeStatsList() {

        for (int i = 0; i < shipHolder.transform.childCount; i++) {

            statsUI.Add(shipHolder.transform.GetChild(i).GetChild(2).gameObject);
            bool active = i == 0 ? true : false;
            statsUI[i].SetActive(active);
        }
    }
    #endregion

    #region Public:
    public void SwipeLeft(bool swipeLeft) {

        targetPosition.x = CalculateNextTargetPosition(swipeLeft);
        shipIndex = CalculateShipIndex(swipeLeft);
    }

    public void ToggleStatsUI(bool swipeLeft) {
        
        int toggle = swipeLeft ? 1 : -1;

        statsUI[shipIndex - toggle].SetActive(false);
        statsUI[shipIndex].SetActive(true);
    }
    #endregion

    #region Private:
    private float CalculateNextTargetPosition(bool swipeLeft) {

        float direction = swipeLeft ? 1.0f : -1.0f;
        targetPosition -= direction * offset;

        float minValue = (1 - shipHolder.transform.childCount) * offset.x;
        float maxValue = 0;
        return Mathf.Clamp(targetPosition.x, minValue, maxValue);
    }

    private int CalculateShipIndex(bool swipeLeft) {

        if (swipeLeft) {
            shipIndex++;
        }
        else {
            shipIndex--;
        }
        int minValue = 0;
        int maxValue = shipHolder.transform.childCount - 1;

        return Mathf.Clamp(shipIndex, minValue, maxValue);
    }
    #endregion
}
