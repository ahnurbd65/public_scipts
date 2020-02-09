using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    Rigidbody playerRigidbody;

    Vector3 lastPointerPosition, currentPointerPosition;

    [SerializeField]
    float moveVelocity = 25;
    [SerializeField]
    float moveAcceleration = 5;
    [SerializeField]
    float rotationSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        getInput();
    }

    void getInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPointerPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            movePlayer();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            stopPlayer();
        }
    }

    void movePlayer()
    {
        currentPointerPosition = Input.mousePosition;
        Vector3 directionVector = currentPointerPosition - lastPointerPosition;
        directionVector = new Vector3(directionVector.x, 0f, directionVector.y);
        directionVector.Normalize();
        if (directionVector.magnitude > 0)
        {
            rotatePlayer(directionVector);
        }
        Vector3 targetVelocity = directionVector * moveVelocity;
        playerRigidbody.velocity = Vector3.Lerp(playerRigidbody.velocity, targetVelocity, Time.fixedDeltaTime * moveAcceleration);
        lastPointerPosition = currentPointerPosition;
    }

    void stopPlayer()
    {
        playerRigidbody.velocity = Vector3.zero;
    }

    void rotatePlayer(Vector3 rotationVector)
    {
        Quaternion targetRotation = Quaternion.LookRotation(rotationVector);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
    }
}
