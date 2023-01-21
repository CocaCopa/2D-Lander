using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] SpaceshipData m_data;
    [SerializeField] bool invertAxis = false;
    [SerializeField] float smoothRotationTime;

    PlayerHandler handler;
    Rigidbody2D rigidBody;

    float forceAmount = 0;
    float maxVelocity = 0;
    float rotationSpeed = 0;
    bool b_move = false;

    private void Awake() {

        rigidBody = GetComponent<Rigidbody2D>();
        handler = GetComponent<PlayerHandler>();

        InitializeVariables();
    }

    private void FixedUpdate() {

        if (b_move) {

            rigidBody.AddForce(transform.up * forceAmount, ForceMode2D.Impulse);
            rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxVelocity);
        }
    }

    private void Update() {

        b_move = handler.GetMoveInput();
        transform.rotation = handler.HandleRotation(invertAxis, rotationSpeed);
    }

    private void InitializeVariables() {

        forceAmount = m_data.force;
        maxVelocity = m_data.maxVeclocity;
        rigidBody.mass = m_data.shipMass;
        rotationSpeed = m_data.rotationSpeed;
    }
 }
