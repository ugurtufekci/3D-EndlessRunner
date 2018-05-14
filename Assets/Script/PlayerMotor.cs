using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private const float LANE_DISTANCE = 3.0f;
    //Movement
    private CharacterController controller;
    private float jumpForce = 4.0f;
    private float gravity = 12.0f;
    private float verticalVelocity;
    private float speed = 7.0f;
    private int desiredLane= 1; //0 = left 1= Middle, 2=Right

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //Gather the inputs on which lane we should be
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLane(false); //move left
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveLane(true); //move right
        }

        //Calculate where we should be in the future
        Vector3 targetPosition = transform.position.normalized.z * Vector3.forward;
        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        }else if(desiredLane == 2)
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }

        //Calculate our move delta
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;
        moveVector.y = -0.1f;
        moveVector.z = speed;

        //Move the Player
        controller.Move(moveVector * Time.deltaTime);

    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2); //edge controls
    }
}
