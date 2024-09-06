using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTemas : MonoBehaviour
{
    public bool hasFinished = false;
    public float finishTime;
    public Rigidbody rb;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("FinishLine") && !hasFinished)
        {
            hasFinished = true;
            finishTime = Time.timeSinceLevelLoad;
            RaceManager.instance.CarFinished(this);
        }
    }


}
