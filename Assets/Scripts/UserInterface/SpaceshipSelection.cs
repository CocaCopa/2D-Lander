using System;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipSelection : MonoBehaviour
{
    [SerializeField] GameObject shipHolder;
    [SerializeField] float shipSpacing = 15;
    [SerializeField] float UISwipeSpeed = 8;

    public GameObject ShipHolder { get { return shipHolder; } }
    public float ShipSpacing { get { return shipSpacing; } }

    Vector3 offset;
    Vector3 targetPosition;

    #region Private:
    private void Awake() {

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

    public void SwipeRight(bool swipeRight) {

        float direction = swipeRight ? 1 : -1;
        targetPosition -= direction * offset;

        float minValue = (-shipHolder.transform.childCount + 1) * offset.x;
        float maxValue = 0;
        targetPosition.x = Mathf.Clamp(targetPosition.x, minValue, maxValue);
    }
    #endregion
}
