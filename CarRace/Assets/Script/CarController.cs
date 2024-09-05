using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //WhelCollider
    [SerializeField] private WheelCollider fronLeftWhell, fronRightWhell, rearLeftWhell, rearRightWhell;

    //Teker transformu
    [SerializeField] private Transform fronLeftTransform, fronRightTransform, rearLeftTransform, rearRightTransform;

    //Fren Ýzi
    [SerializeField] private TrailRenderer fronLeftTrail, fronRightTrail, rearLeftTrail, rearRightTrail;

    public float maxMotorTorque = 1500f;
    public float maxSteeringAngle = 30f;
    public float breakeTorque = 5000f;

    public float trailDuration = 3.0f;
    private bool isBraking = false;

    private void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        ApplySteering(steering);
        ApplyDriver(motor);
        ApplyBreak();

        UpdateWhellPoses();
        ManageTrail();

    }

    void ApplyDriver(float motor)
    {
        if (Input.GetButton("Vertical"))
        {
            //Motor Torku uygulandý
            rearLeftWhell.motorTorque = motor;
            rearRightWhell.motorTorque = motor;

            //fren torkunu sýfýrladým
            rearLeftWhell.brakeTorque = 0;
            rearRightWhell.brakeTorque = 0;
        }
        else
        {
            rearLeftWhell.motorTorque = 0;
            rearRightWhell.motorTorque = 0;
        }
    }

    void ApplySteering(float steering)
    {
        fronLeftWhell.steerAngle = steering;
        fronRightWhell.steerAngle = steering;
    }

    void ApplyBreak()
    {
        bool wasBraking = isBraking;

        if (Input.GetKey(KeyCode.Space))
        {
            rearLeftWhell.brakeTorque = breakeTorque;
            rearRightWhell.brakeTorque = breakeTorque;
           fronRightWhell.brakeTorque = breakeTorque;
           fronLeftWhell.brakeTorque = breakeTorque;

            isBraking = true;
        }
        else
        {
            rearLeftWhell.brakeTorque = 0;
            rearRightWhell.brakeTorque = 0;
            fronRightWhell.brakeTorque = 0;
            fronRightWhell.brakeTorque = 0;

            isBraking = false;
        }
        if(wasBraking && !isBraking)
        {
            StartCoroutine(StopTrailAfterDelay(trailDuration));
        }
    }

    void ManageTrail()
    {
        if (isBraking)
        {
            fronRightTrail.emitting = true;
            fronLeftTrail.emitting = true;
            rearLeftTrail.emitting = true;
            rearRightTrail.emitting = true;
        }
        else
        {
            fronRightTrail.emitting = false;
            fronLeftTrail.emitting = false;
            rearLeftTrail.emitting = false;
            rearRightTrail.emitting = false;
        }
    }

    private IEnumerator StopTrailAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        fronRightTrail.emitting = false;
        fronLeftTrail.emitting = false;
        rearLeftTrail.emitting = false;
        rearRightTrail.emitting = false;
    }

    void UpdateWhellPoses()
    {
        UpdateWhellPose(fronLeftWhell, fronLeftTransform);
        UpdateWhellPose(fronRightWhell, fronRightTransform);
        UpdateWhellPose(rearLeftWhell, rearLeftTransform);
        UpdateWhellPose(rearRightWhell, rearRightTransform);
    }

    void UpdateWhellPose(WheelCollider collider, Transform transform)
    {
        Vector3 pos;
        Quaternion quat;

        collider.GetWorldPose(out pos, out quat);

        transform.position = pos;
        transform.rotation = quat;
    }

}
