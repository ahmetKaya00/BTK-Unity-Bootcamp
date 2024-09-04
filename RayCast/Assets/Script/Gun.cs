using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 10f;
    [SerializeField] Camera cam;
    [SerializeField] GameObject impactEffect;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position,cam.transform.forward, out hit, range))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();

                if(rb != null)
                {
                    rb.AddForce(-hit.normal * damage, ForceMode.Impulse);

                    Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }

                Destroy(hit.transform.gameObject, 3f); // D��man� 3 saniye sonra yok et!
            }
        }
    }


}
