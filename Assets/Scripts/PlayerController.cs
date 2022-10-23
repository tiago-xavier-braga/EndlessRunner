using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 0.03f;
    [SerializeField] private float forwardSpeed = 0.05f;
    private void Update()
    {
        Vector3 targetPosition = transform.position;
        if (Input.GetKey(KeyCode.A))
        {
            targetPosition += Vector3.left * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            targetPosition += Vector3.right * Time.deltaTime;
        }

        targetPosition += Vector3.forward * forwardSpeed;

        transform.position = targetPosition;
    }    

}
