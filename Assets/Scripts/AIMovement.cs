using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{

    public float rayDistance = 1;
    public float speed = 1;
    public float radiusOfSatisfaction = .01f;
    public float radiusOfEnding = .1f;

    private bool inMovement = false;
    private Vector3 moveDirection = Vector3.zero;
    private string lastDirection = "up";
    private CharacterController AIController;
    private Vector3 targetLocation;
    private Vector3 endLocation = new Vector3(9, .5f, 9);
    

    // Use this for initialization
    void Start()
    {
        AIController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inMovement)
        {
            RaycastHit hitInfo;
            if (lastDirection == "up")
            {
                if (!Physics.Raycast(transform.position, transform.right, out hitInfo, rayDistance))
                {
                    moveDirection = Vector3.right;
                    lastDirection = "right";
                }
                else if (Physics.Raycast(transform.position, transform.right, out hitInfo, rayDistance))
                {
                    if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance))
                    {
                        if (Physics.Raycast(transform.position, -transform.right, out hitInfo, rayDistance))
                        {
                            moveDirection = -Vector3.forward;
                            lastDirection = "down";
                        }
                        else
                        {
                            moveDirection = -Vector3.right;
                            lastDirection = "left";
                        }
                    }
                    else
                    {
                        moveDirection = Vector3.forward;
                        lastDirection = "up";
                    }
                }
            }

            else if (lastDirection == "right")
            {
                if (!Physics.Raycast(transform.position, -transform.forward, out hitInfo, rayDistance))
                {
                    moveDirection = -Vector3.forward;
                    lastDirection = "down";
                }
                else if (Physics.Raycast(transform.position, -transform.forward, out hitInfo, rayDistance))
                {
                    if (Physics.Raycast(transform.position, transform.right, out hitInfo, rayDistance))
                    {
                        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance))
                        {
                            moveDirection = -Vector3.right;
                            lastDirection = "left";
                        }
                        else
                        {
                            moveDirection = Vector3.forward;
                            lastDirection = "up";
                        }
                    }
                    else
                    {
                        moveDirection = Vector3.right;
                        lastDirection = "right";
                    }
                }
            }

            else if (lastDirection == "down")
            {
                if (!Physics.Raycast(transform.position, -transform.right, out hitInfo, rayDistance))
                {
                    moveDirection = -Vector3.right;
                    lastDirection = "left";
                }
                else if (Physics.Raycast(transform.position, -transform.right, out hitInfo, rayDistance))
                {
                    if (Physics.Raycast(transform.position, -transform.forward, out hitInfo, rayDistance))
                    {
                        if (Physics.Raycast(transform.position, transform.right, out hitInfo, rayDistance))
                        {
                            moveDirection = Vector3.forward;
                            lastDirection = "up";
                        }
                        else
                        {
                            moveDirection = Vector3.right;
                            lastDirection = "right";
                        }
                    }
                    else
                    {
                        moveDirection = -Vector3.forward;
                        lastDirection = "down";
                    }
                }
            }

            else if (lastDirection == "left")
            {
                if (!Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance))
                {
                    moveDirection = Vector3.forward;
                    lastDirection = "up";
                }
                else if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance))
                {
                    if (Physics.Raycast(transform.position, -transform.right, out hitInfo, rayDistance))
                    {
                        if (Physics.Raycast(transform.position, -transform.forward, out hitInfo, rayDistance))
                        {
                            moveDirection = Vector3.right;
                            lastDirection = "right";
                        }
                        else
                        {
                            moveDirection = -Vector3.forward;
                            lastDirection = "down";
                        }
                    }
                    else
                    {
                        moveDirection = -Vector3.right;
                        lastDirection = "left";
                    }
                }
            }

            inMovement = true;
            targetLocation = transform.position + moveDirection;
        }

        if (inMovement)
        {
            if (Vector3.Distance(targetLocation, transform.position) < radiusOfSatisfaction)
            {
                inMovement = false;
                moveDirection = Vector3.zero;
            }
        }
        print(endLocation);
        print(transform.position);
        if (Vector3.Distance(endLocation, transform.position) < radiusOfEnding)
        {
            print("true");
            GetComponent<AIMovement>().enabled = false;
        }
        AIController.Move(moveDirection.normalized * speed * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * rayDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position - transform.forward * rayDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * rayDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position - transform.right * rayDistance);
    }
}