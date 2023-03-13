using TMPro;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [Header("--- Float Behaviour ---")] [Space(5.0f)]
    [SerializeField] Transform movingObject;
    [SerializeField] Transform targetPoint;
    [SerializeField] AnimationCurve curve;
    [Space(15.0f)]
    [SerializeField] float speed = 0.2f;
    [SerializeField] float waitTime = 0.01f;

    Vector3 startPosition;
    Vector3 targetPosition;
    Vector3 waypoint1;
    Vector3 waypoint2;

    float waitTimer = 0;
    float curvePoints;

    private void Awake() {

        waypoint1 = startPosition = movingObject.localPosition;
        waypoint2 = targetPosition = targetPoint.localPosition;
    }

    private void Update() {

        FloatingSpaceship();
    }

    private void FloatingSpaceship() {

        if (waitTimer > 0) {
            
            waitTimer -= Time.deltaTime;
        }
        else {
            
            movingObject.localPosition = LerpPosition();
            PingPongEffect();
        }
    }

    private Vector3 LerpPosition() {

        curvePoints += speed * Time.deltaTime;
        return Vector3.Lerp(waypoint1, waypoint2, curve.Evaluate(curvePoints));
    }

    private void PingPongEffect() {

        if (movingObject.localPosition.Equals(waypoint2)) {

            curvePoints = 0;
            waitTimer = waitTime;

            if (waypoint1 == targetPosition) {

                waypoint1 = startPosition;
                waypoint2 = targetPosition;
            }
            else {

                waypoint1 = targetPosition;
                waypoint2 = startPosition;
            }
        }
    }

    #region Gizmos:
    private void OnDrawGizmos() {

        if (targetPoint == null || movingObject == null) {

            Debug.Log("Requiered 'Transforms' have not been assigned");
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(movingObject.position, 0.2f);
        Gizmos.DrawSphere(targetPoint.position, 0.2f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(movingObject.position, targetPoint.position);
        LineThickness();
    }

    private void LineThickness() {

        Gizmos.DrawLine(movingObject.position + new Vector3(0.01f, 0, 0), targetPoint.position + new Vector3(0.01f, 0, 0));
        Gizmos.DrawLine(movingObject.position + new Vector3(0.02f, 0, 0), targetPoint.position + new Vector3(0.02f, 0, 0));
        Gizmos.DrawLine(movingObject.position + new Vector3(0.03f, 0, 0), targetPoint.position + new Vector3(0.03f, 0, 0));
        Gizmos.DrawLine(movingObject.position + new Vector3(-0.01f, 0, 0), targetPoint.position + new Vector3(-0.01f, 0, 0));
        Gizmos.DrawLine(movingObject.position + new Vector3(-0.02f, 0, 0), targetPoint.position + new Vector3(-0.02f, 0, 0));
        Gizmos.DrawLine(movingObject.position + new Vector3(-0.03f, 0, 0), targetPoint.position + new Vector3(-0.03f, 0, 0));
    }
    #endregion
}