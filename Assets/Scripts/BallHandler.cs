using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private float detachDelay = .5f;
    [SerializeField] private SpringJoint2D currentBallSpringJoint;
    [SerializeField] private Rigidbody2D currentBallRigidBody;

    private Camera mainCamera;
    private bool isDraggring;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBallRigidBody == null) return;

        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (isDraggring)
            {
                LaunchBall();
            }

            isDraggring = false;

            return;
        }

        isDraggring = true;
        currentBallRigidBody.isKinematic = true;

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        currentBallRigidBody.position = worldPosition;
    }

    void LaunchBall()
    {
        currentBallRigidBody.isKinematic = false;
        currentBallRigidBody = null;

        Invoke(nameof(DetachBall), detachDelay);
    }

    void DetachBall()
    {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;
    }
}
