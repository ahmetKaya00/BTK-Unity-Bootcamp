using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitcer : MonoBehaviour
{
    [SerializeField] private Light Light;
    void Start()
    {
        if (Light == null)
        {
            Debug.LogError("Iþýk kaynaðý bulunamadý!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Light.enabled = !Light.enabled;
        }
    }
}
