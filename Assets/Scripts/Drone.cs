using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Target
{
    [SerializeField] private int droneSpeed;
    [SerializeField] [Range(0.0f, 1.0f)] protected float battery;
    private Vector3 targetPoint;
    public Transform restPoint;
    private Quaternion rotateTowards;
    [SerializeField] private float rotationSpeed;

    private int fleeTime;

    protected State currentState;

    protected enum State
    {
        idle,
        flee,
        toRestpoint,
        rest,
        die
    };

    private void Start()
    {
        target = this.gameObject;
        currentState = State.idle;
    }

    private void FixedUpdate()
    {
        Debug.Log(currentState);
        DroneStates();
        if (battery <= 0.0)
        {
            currentState = State.die;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RestPoint"))
        {
            currentState = State.rest;
        }
    }

    void DroneStates()
    {
        switch (currentState)
        {
            case (State.idle):
                Move(1);

                break;
            case (State.flee):
                Move(3);

                break;
            case (State.toRestpoint):
                Move(1);

                break;
            case (State.rest):
                if (battery < 1.0)
                {
                    battery += 0.01f;
                } else
                {
                    currentState = State.idle;
                }

                break;
            case (State.die):
                target.GetComponent<Rigidbody>().useGravity = true;

                break;
        }
    }

    /// <summary>
    /// move towards destination
    /// </summary>
    /// <param name="moveSpeed">multiplier for movement speed</param>
    private void Move(float moveSpeed)
    {
        if (Quaternion.Angle(target.transform.rotation, rotateTowards) >= 0.05)
        {
            //rotate towards destination
            target.transform.rotation = Quaternion.RotateTowards(target.transform.rotation, rotateTowards, rotationSpeed * moveSpeed * Time.deltaTime);
        } else {
            //move towards destination
            transform.Translate(Vector3.forward * droneSpeed * moveSpeed * Time.deltaTime);
        }

        battery -= (0.001f * moveSpeed);

        //if the drone isn't already moving towards the restpoint
        if (!(currentState == State.toRestpoint)){
            //drone moves towards restpoint when the battery drops below 30%
            if (battery <= 0.3) {
                targetPoint = restPoint.position;
                rotateTowards = Quaternion.LookRotation(targetPoint, Vector3.up);
                currentState = State.toRestpoint;
                return;
            } //set a new destination when the drone has reached the previous and has more than 30% battery
            else             
            if (Vector3.Distance(target.transform.position, targetPoint) <= 0.05f)
            {
                randomDestination();
            }
        }
    }

    /// <summary>
    /// set new random destination
    /// </summary>
    private void randomDestination() //doesn't work yet!
    {
        Vector3 newDestination = Random.insideUnitSphere * 3;
        targetPoint.x = newDestination.x;
        targetPoint.z = newDestination.z;
        rotateTowards = Quaternion.LookRotation(targetPoint, Vector3.up);
    }

    public override void TargetHit()
    {
        base.TargetHit();
        if (currentState != State.die)
        {
            currentState = State.flee;
            fleeTime += 5;
            if (fleeTime <= 5)
            {
                StartCoroutine(ReturnToIdle());
            }
        }
    }

    IEnumerator ReturnToIdle()
    {
        while (fleeTime > 0)
        {
            fleeTime--;
            yield return new WaitForSeconds(1f);
        }
        currentState = State.idle;
 }
}
