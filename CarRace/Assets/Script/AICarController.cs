using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarController : MonoBehaviour
{
    public Transform[] wayPoint;
    public Transform[] whells;
    public WheelCollider[] wheelCollider;

    public float maxMotorTorque = 1500f;
    public float maxSteeringAngle = 30f;
    public float lookAheadDistance = 5f;

    private int currentWayIndx = 0;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Drive();
        CheckWaypointDistance();
        UpdateWhellVisual();
    }

    void Drive()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(wayPoint[currentWayIndx].position);

        float newSteer = Mathf.Clamp((relativeVector.x / relativeVector.magnitude) * maxSteeringAngle, -maxSteeringAngle,maxSteeringAngle);
  
        for(int i = 0; i < wheelCollider.Length; i++)
        {
            if (wheelCollider[i].transform.localPosition.z > 0)
            {
                wheelCollider[i].steerAngle = newSteer;
            }
        }

        foreach(WheelCollider wheel in wheelCollider)
        {
            wheel.motorTorque = maxMotorTorque;
        }
    }

    void CheckWaypointDistance()
    {
        float distance = Vector3.Distance(transform.position, wayPoint[currentWayIndx].position);
        if(distance < lookAheadDistance)
        {
            currentWayIndx = (currentWayIndx + 1) % wayPoint.Length;
        }
    }

    void UpdateWhellVisual()
    {
        for(int i = 0; i < wheelCollider.Length; i++)
        {
            Quaternion quat;
            Vector3 position;

            wheelCollider[i].GetWorldPose(out position, out quat);

            whells[i].position = position;
            whells[i].rotation = quat;
        }
    }
}
