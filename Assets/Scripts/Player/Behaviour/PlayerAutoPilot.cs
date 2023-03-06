using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoPilot : MonoBehaviour
{
    [Header("--- Cinematic Entrance ---")]
    [Space(5)]
    AnimationCurve curve;
    [SerializeField] Transform[] controlPoints = new Transform[4];
    [SerializeField] Transform[] angleChangePoints = new Transform[3];
    [SerializeField] float travelSpeed;
    [SerializeField] float spaceshipAngle;

    [Space(15)]

    [Header("--- End of Level Behaviour ---")]
    [Space(5)]
    [SerializeField] Transform landingTransform;
    [SerializeField] Vector3 landOffset = new Vector3(0,1);
    [SerializeField] float landSpeed = 0;
    [SerializeField] float rotationSpeedMultiplier = 2.5f;

    Rigidbody2D playerRb;
    float bezierPoint;
    float animationPoints = 0;

    #region Public:
    /// <summary>
    /// Lands the spaceship automatically
    /// </summary>
    public void HandleAutoLanding() {

        DisableRigidbody();
        transform.position = AutoLandPosition();
        transform.up = AutoLandAngle();
    }

    /// <summary>
    /// Moves the spaceship inside the scene smoothly.
    /// </summary>
    /// <returns>True, once the animation is completed</returns>
    public bool CinematicEntrance() {

        InitializeAnimationCurve();

        transform.SetPositionAndRotation(
            CalculateBezierPoints(out bool animationCompleted),
            CalculateSpaceshipAngle()
        );

        return animationCompleted;
    }
    #endregion

    #region Private:
    private void DisableRigidbody() {

        InitializeRigidbody();

        if (playerRb.simulated == true) {

            Vector2 resetVelocity = Vector2.zero;
            playerRb.velocity = resetVelocity;
            playerRb.simulated = false;
            playerRb.isKinematic = true;
        }
    }

    private Vector2 AutoLandPosition() {

        Vector3 current = transform.position;
        Vector3 target = landingTransform.position + landOffset;
        float lerpTime = landSpeed * Time.deltaTime;

        return Vector2.Lerp(current, target, lerpTime);
    }

    private Vector2 AutoLandAngle() {

        Vector3 current = transform.up;
        Vector3 target = Vector3.up;
        float lerpTime = landSpeed * rotationSpeedMultiplier * Time.deltaTime;

        return Vector2.Lerp(current, target, lerpTime);
    }

    private Vector3 CalculateBezierPoints(out bool onPosition) {

        Vector3 bezierPosition = Mathf.Pow(1 - bezierPoint, 3) * controlPoints[0].position +
                              3 * Mathf.Pow(1 - bezierPoint, 2) * bezierPoint * controlPoints[1].position +
                              3 * (1 - bezierPoint) * Mathf.Pow(bezierPoint, 2) * controlPoints[2].position +
                              Mathf.Pow(bezierPoint, 3) * controlPoints[3].position;

        bool onBezierCurve = Vector3.Distance(bezierPosition, controlPoints[3].position) > 0.1f;

        if (onBezierCurve) {

            bezierPoint += travelSpeed * Time.deltaTime;
            animationPoints += travelSpeed * Time.deltaTime;
            onPosition = false;
        }
        else { // Spaceship sits on the last bezier point

            onPosition = true;
        }

        Vector3 targetPosition = Vector3.Lerp(controlPoints[0].position, bezierPosition, curve.Evaluate(animationPoints));

        return targetPosition;
    }

    private Quaternion CalculateSpaceshipAngle() {

        float shipToPoint1 = Vector3.Distance(transform.position, angleChangePoints[0].position);
        float shipToPoint2 = Vector3.Distance(transform.position, angleChangePoints[1].position);
        float shipToPoint3 = Vector3.Distance(transform.position, angleChangePoints[2].position);
        float shipToEnd    = Vector3.Distance(transform.position, controlPoints[3].position);

        float startToPoint1  = Vector3.Distance(controlPoints[0].position, angleChangePoints[0].position);
        float point1ToPoint2 = Vector3.Distance(angleChangePoints[0].position, angleChangePoints[1].position);
        float point2ToPoint3 = Vector3.Distance(angleChangePoints[1].position, angleChangePoints[2].position);
        float point3ToEnd    = Vector3.Distance(angleChangePoints[2].position, controlPoints[3].position);

        float distanceSector1 = angleChangePoints[0].position.x - transform.position.x;
        float distanceSector2 = angleChangePoints[1].position.x - transform.position.x;
        float distanceSector3 = angleChangePoints[2].position.x - transform.position.x;
        float distanceSector4 = controlPoints[3].position.x - transform.position.x;

        float angle = 0;

        if (distanceSector1 > 0) {

            angle = (spaceshipAngle * shipToPoint1 / startToPoint1) - spaceshipAngle;
        }
        else if (distanceSector2 > 0) {

            angle = -spaceshipAngle * shipToPoint2 / point1ToPoint2;
        }
        else if (distanceSector3 > 0) {

            angle = spaceshipAngle - (spaceshipAngle * shipToPoint3 / point2ToPoint3);
        }
        else if (distanceSector4 > 0) {
            
            angle = spaceshipAngle * shipToEnd / point3ToEnd;
        }
        
        return Quaternion.Euler(0, 0, angle);
    }

    private void InitializeRigidbody() {

        if (playerRb == null) {
            playerRb = GetComponent<Rigidbody2D>();
        }
    }

    private void InitializeAnimationCurve() {

        if (curve == null) {
            curve = AnimationCurve.Linear(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
    #endregion
}
