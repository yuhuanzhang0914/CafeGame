using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float ratateSpeed = 10;
    [SerializeField] private GameInput gameInput;
    private bool isWalking = false;

    private void FixedUpdate()
    {
        Vector3 direction = gameInput.GetMovementDirectionNormalized();

        isWalking = direction != Vector3.zero;

        transform.position += direction * Time.deltaTime * moveSpeed;
        if (direction != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * ratateSpeed);
        }
    }
    public bool IsWalking
    {
        get
        {
            return isWalking;
        }
    }
}

