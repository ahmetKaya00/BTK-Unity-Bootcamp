using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //WhelCollider
    [SerializeField] private WheelCollider fronLeftWhell, fronRightWhell, rearLeftWhell, rearRightWhell;

    //Teker transformu
    [SerializeField] private Transform fronLeftTransform, fronRightTransform, rearLeftTransform, rearRightTransform;

    public float maxMotorTorque = 1500f;
    public float maxSteeringAngle = 30f;
    public float breakeTorque = 5000f;

    private void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        ApplySteering(steering);
        ApplyDriver(motor);
        ApplyBreak();

        UpdateWhellPoses();

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
        if (Input.GetKey(KeyCode.Space))
        {
            rearLeftWhell.brakeTorque = breakeTorque;
            rearRightWhell.brakeTorque = breakeTorque;
        }
        else if (!Input.GetButton("Vertical"))
        {
            rearLeftWhell.brakeTorque = breakeTorque;
            rearRightWhell.brakeTorque = breakeTorque;
        }
        else
        {
            rearLeftWhell.brakeTorque = 0;
            rearRightWhell.brakeTorque = 0;
        }
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
