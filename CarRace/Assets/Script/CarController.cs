using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour
{
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    public Transform frontLeftTransform;
    public Transform frontRightTransform;
    public Transform rearLeftTransform;
    public Transform rearRightTransform;

    public float maxMotorTorque = 1500f;
    public float maxSteeringAngle = 30f;
    public float brakeTorque = 5000f; 

    public TrailRenderer frontLeftTrail;
    public TrailRenderer frontRightTrail;
    public TrailRenderer rearLeftTrail;
    public TrailRenderer rearRightTrail;

    public float trailDuration = 3.0f; 

    private bool isBraking = false;

    private void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        ApplySteering(steering);
        ApplyDrive(motor);
        ApplyBrakes();
        UpdateWheelPoses();
        ManageTrails();
    }

    private void ApplyDrive(float motor)
    {
        if (Input.GetButton("Vertical"))
        {
            rearLeftWheel.motorTorque = motor;
            rearRightWheel.motorTorque = motor;
            rearLeftWheel.brakeTorque = 0;
            rearRightWheel.brakeTorque = 0;
        }
        else
        {
            rearLeftWheel.motorTorque = 0;
            rearRightWheel.motorTorque = 0;
        }
    }

    private void ApplySteering(float steering)
    {
        frontLeftWheel.steerAngle = steering;
        frontRightWheel.steerAngle = steering;
    }

    private void ApplyBrakes()
    {
        bool wasBraking = isBraking;

        if (Input.GetButton("Jump")) 
        {
            rearLeftWheel.brakeTorque = brakeTorque;
            rearRightWheel.brakeTorque = brakeTorque;
            frontLeftWheel.brakeTorque = brakeTorque;
            frontRightWheel.brakeTorque = brakeTorque;

            isBraking = true;
        }
        else
        {
            rearLeftWheel.brakeTorque = 0;
            rearRightWheel.brakeTorque = 0;
            frontLeftWheel.brakeTorque = 0;
            frontRightWheel.brakeTorque = 0;

            isBraking = false;
        }

        if (wasBraking && !isBraking)
        {
            StartCoroutine(StopTrailsAfterDelay(trailDuration));
        }
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftWheel, frontLeftTransform);
        UpdateWheelPose(frontRightWheel, frontRightTransform);
        UpdateWheelPose(rearLeftWheel, rearLeftTransform);
        UpdateWheelPose(rearRightWheel, rearRightTransform);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        Vector3 pos;
        Quaternion quat;
        collider.GetWorldPose(out pos, out quat);
        transform.position = pos;
        transform.rotation = quat;
    }

    private void ManageTrails()
    {
        if (isBraking)
        {
            frontLeftTrail.emitting = true;
            frontRightTrail.emitting = true;
            rearLeftTrail.emitting = true;
            rearRightTrail.emitting = true;
        }
        else
        {
            frontLeftTrail.emitting = false;
            frontRightTrail.emitting = false;
            rearLeftTrail.emitting = false;
            rearRightTrail.emitting = false;
        }
    }

    private IEnumerator StopTrailsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        frontLeftTrail.emitting = false;
        frontRightTrail.emitting = false;
        rearLeftTrail.emitting = false;
        rearRightTrail.emitting = false;
    }
}