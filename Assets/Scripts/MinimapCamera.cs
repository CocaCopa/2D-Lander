using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    private void Awake() {

        transform.SetPositionAndRotation(SetCameraFollowPosition(), Quaternion.Euler(0, 0, 0));
    }

    private void Update() {

        transform.position = SetCameraFollowPosition();
    }

    private Vector3 SetCameraFollowPosition() {

        Vector3 cameraFollowPosition = playerTransform.position;
        cameraFollowPosition.z = transform.position.z;

        return cameraFollowPosition;
    }
}
