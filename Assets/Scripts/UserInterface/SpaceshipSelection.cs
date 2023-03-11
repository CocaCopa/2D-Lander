using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipSelection : MonoBehaviour
{
    [SerializeField] GameObject shipHolder;
    [SerializeField] float UISwipeSpeed = 8;
    [SerializeField] float shipSpacing = 6;

    Vector3 offset;
    Vector3 targetPosition;
    
    public void NextSpaceship() {

        SetTargetPosition(true);
    }

    public void PreviousSpaceship() {

        SetTargetPosition(false);
    }

    public void SelectSpaceship() {


    }

    private void Awake() {

        SetSpaceshipPositions();
    }

    private void Update() {

        shipHolder.transform.position = NextUiPosition();
    }

    private Vector3 NextUiPosition() {

        Vector3 currentPosition = shipHolder.transform.position;
        float lerpTime = UISwipeSpeed * Time.deltaTime;
        return Vector2.Lerp(currentPosition, targetPosition, lerpTime);
    }

    private void SetTargetPosition(bool moveLeft) {

        float direction = moveLeft ? 1 : -1;
        targetPosition -= direction * offset;

        float minValue = (-shipHolder.transform.childCount + 1) * offset.x;
        float maxValue = 0;
        targetPosition.x = Mathf.Clamp(targetPosition.x, minValue, maxValue);
    }

    private void SetSpaceshipPositions() {

        offset = new Vector3(shipSpacing, 0, 0);
        Vector3 shipPosition = Vector3.zero;

        for (int i = 0; i < shipHolder.transform.childCount; i++) {

            GameObject spaceship = shipHolder.transform.GetChild(i).gameObject;
            spaceship.transform.position = shipPosition;

            shipPosition += offset;
        }
    }
}
