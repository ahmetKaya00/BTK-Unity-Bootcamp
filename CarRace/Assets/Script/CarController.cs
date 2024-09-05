using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    public AudioSource engineSourceSound;
    public AudioClip engineSourceClip;

    public float trailDuration = 3.0f; 

    private bool isBraking = false;

    public Text speedText;

    private Rigidbody rb;

    public float antiRoll = 500.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        speedText = GameObject.Find("Ibre").GetComponent<Text>();
        if(engineSourceSound != null && engineSourceClip != null)
        {
            engineSourceSound.clip = engineSourceClip;
            engineSourceSound.loop = true;
            engineSourceSound.Play();
        }


    }

    void ManageEngineSound(float motor)
    {
        if(engineSourceSound != null)
        {
            if(motor != 0)
            {
                if (!engineSourceSound.isPlaying)
                {
                    engineSourceSound.Play();
                }
            }
            else
            {
                if (engineSourceSound.isPlaying)
                {
                    engineSourceSound.Stop();
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        ApplySteering(steering);
        ApplyDrive(motor);
        ApplyBrakes();
        UpdateWheelPoses();
        ManageTrails();
        ManageEngineSound(motor);

        float speed = rb.velocity.magnitude * 3.6f;

        speedText.text = "Speed: " + Mathf.RoundToInt(speed).ToString() + "km/h";

        ApplyAntiRoll(frontLeftWheel, frontRightWheel);
        ApplyAntiRoll(rearLeftWheel, rearRightWheel);

    }

    void ApplyAntiRoll(WheelCollider leftWhell, WheelCollider rightWhell)
    {
        WheelHit hit;

        float TravelL = 1.0f;
        float TravelR = 1.0f;

        bool groundL = leftWhell.GetGroundHit(out hit);
        if (groundL)
            TravelL = (-leftWhell.transform.InverseTransformPoint(hit.point).y - leftWhell.radius) / leftWhell.suspensionDistance;
        bool groundR = rightWhell.GetGroundHit(out hit);
        if (groundR)
            TravelL = (-rightWhell.transform.InverseTransformPoint(hit.point).y - rightWhell.radius) / rightWhell.suspensionDistance;

       float antiRollForce = (TravelL - TravelR) * antiRoll;
        if (groundL)
            rb.AddForceAtPosition(leftWhell.transform.up * -antiRollForce, leftWhell.transform.position);
        if (groundR)
            rb.AddForceAtPosition(rightWhell.transform.up * -antiRollForce, rightWhell.transform.position);
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