using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 15;
    [SerializeField] private float forwardSpeed = 10;
    [SerializeField] private float laneDistanceX = 2;

    [SerializeField] private float jumpDistanceZ = 5;
    [SerializeField] private float jumpHeightY = 2;

    Vector3 initialPosition;

    float targetPositionX;
    float jumpStartZ;
    public bool IsJumping { get; private set;}

    private float LeftLaneX => initialPosition.x - laneDistanceX;
    private float RightLaneX => initialPosition.x + laneDistanceX;
    
    private void Awake() {
        initialPosition = transform.position;
    }

    private void Update()
    {
        ProcessInput();

        Vector3 position = transform.position;

        position.x = ProcessLaneMovement();
        position.y = ProcessJump();
        position.z = ProcessForwardMovement();

        transform.position = position;
    }    

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            targetPositionX += laneDistanceX;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetPositionX -= laneDistanceX;
        }
        if (Input.GetKeyDown(KeyCode.W) && !IsJumping)
        {
            IsJumping = true;
            jumpStartZ = transform.position.z;
        }

        targetPositionX = Mathf.Clamp(targetPositionX, LeftLaneX, RightLaneX);
    }

    private float ProcessLaneMovement()
    {
        return Mathf.Lerp(transform.position.x, targetPositionX, Time.deltaTime * horizontalSpeed);
    }

    private float ProcessForwardMovement()
    {
        return transform.position.z + forwardSpeed * Time.deltaTime;
    }
    
    private float ProcessJump()
    {
        float deltaY = 0;
        if (IsJumping)
        {
            float jumpPercent = (transform.position.z - jumpStartZ) / jumpDistanceZ;
            if (jumpPercent >= 1)
            {
                IsJumping = false;
            } 
            else
            {
                deltaY = Mathf.Sin(Mathf.PI*jumpPercent) * jumpHeightY;
            }
        }
        return initialPosition.y + deltaY;
    }
}
