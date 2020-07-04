using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Target
{
    [SerializeField] private float droneSpeed;
    [SerializeField] [Range(0.0f, 1.0f)] protected float battery;
    private Vector3 targetPoint;
    private float destinationTimer;
    public GameObject restPoint;
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

    protected override void Start()
    {
        base.Start();
        randomDestination();
        currentState = State.idle;
    }

    private void FixedUpdate()
    {
        //Debug.Log(currentState);
        DroneStates();
        if (battery <= 0.0)
        {
            currentState = State.die;
        }
        destinationTimer -= 0.02f;
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
                    battery += 0.005f;
                } else
                {
                    currentState = State.idle;
                }

                break;
            case (State.die):
                //stop moving and fall
                target.GetComponent<Rigidbody>().useGravity = true;
                target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
                target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
                break;
        }
    }

    /// <summary>
    /// move towards destination
    /// </summary>
    /// <param name="moveSpeed">multiplier for movement speed</param>
    private void Move(float moveSpeed)
    {
        if (Quaternion.Angle(target.transform.rotation, rotateTowards) >= 0.01)
        {
            //rotate towards destination
            target.transform.rotation = Quaternion.RotateTowards(target.transform.rotation, rotateTowards, rotationSpeed * moveSpeed);
        } else {
            //move towards destination
            transform.Translate(Vector3.forward * droneSpeed * moveSpeed * Time.deltaTime);
        }

        battery -= (0.0005f * moveSpeed);

        //if the drone isn't already moving towards the restpoint
        if (!(currentState == State.toRestpoint)){
            //drone moves towards restpoint when the battery drops below 30%
            if (battery <= 0.5f) {
                targetPoint = restPoint.transform.position;
                targetPoint.y = 0;
                rotateTowards = Quaternion.LookRotation(targetPoint, Vector3.up);
                currentState = State.toRestpoint;
                Debug.Log(targetPoint);
                return;
            } //set a new destination when the drone has reached the previous and has more than 30% battery
            else
            if (destinationTimer <= 0 || Vector3.Distance(target.transform.position, targetPoint) <= 0.05)
            {
                randomDestination();
                destinationTimer = Random.Range(0.5f, 1.5f)*(1.5f/moveSpeed);
            }
        }
    }

    /// <summary>
    /// set new random destination
    /// </summary>
    private void randomDestination() 
    {
        targetPoint.x = Random.Range(-3, 3);
        targetPoint.z = Random.Range(-3f, 3);
        rotateTowards = Quaternion.LookRotation(targetPoint, Vector3.up);
    }

    /// <summary>
    /// start fleeing when hit
    /// </summary>
    public override void TargetHit()
    {
        base.TargetHit();

        //play effect and make it bigger every hit
        eventManager.GetComponent<ParticleManager>().HitParticles(target.GetComponentInChildren<ParticleSystem>(), health);

        if (currentState != State.die)
        {
            currentState = State.flee;
            fleeTime += 3;
            if (fleeTime <= 3)
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
