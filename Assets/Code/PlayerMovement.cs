using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lookSpeed;
    [SerializeField] private Transform lookTarget;
    private Rigidbody rb;
    private Vector3 moveVector;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            Vector3 camForward = lookTarget.forward;
            camForward.y = 0;
            camForward.Normalize();
            // float yVel = rb.velocity.y;
            rb.velocity = moveVector.z * camForward;
            camForward = lookTarget.right;
            camForward.y = 0;
            camForward.Normalize();
            rb.velocity += moveVector.x * camForward;
            rb.velocity *= moveSpeed;
            yield return null;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        moveVector = new Vector3(move.x, moveVector.y, move.y);
    }

    public void Look(InputAction.CallbackContext context)
    {
        Vector2 look = context.ReadValue<Vector2>();
        // Debug.Log(look.x + ", " + look.y);
        lookTarget.rotation *= Quaternion.AngleAxis(look.x * lookSpeed, Vector3.up);
        lookTarget.rotation *= Quaternion.AngleAxis(-look.y * lookSpeed, Vector3.right);
        var angles = lookTarget.localEulerAngles;
        var angle = angles.x;
        if (angle > 180 && angle < 320)
        {
            angles.x = 320;
        }
        else if (angle < 180 && angle > 30)
        {
            angles.x = 30;
        }

        lookTarget.transform.localEulerAngles = angles;

        transform.rotation = Quaternion.Euler(0, lookTarget.rotation.eulerAngles.y, 0);
        lookTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }
}
