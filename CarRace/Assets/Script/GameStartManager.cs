using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public CinemachineFreeLook introCamera;
    public CinemachineVirtualCamera mainCamera;
    public GameObject playerCar;
    public float rotationSpeed = 10f;
    public AudioSource countDownAudio;

    private void Start()
    {
        StartCoroutine(IntroSequce());
    }

    private void Update()
    {
        if(introCamera.Priority > mainCamera.Priority)
        {
            introCamera.m_XAxis.Value += rotationSpeed * Time.deltaTime;
        }
    }

    private IEnumerator IntroSequce()
    {
        playerCar.GetComponent<CarController>().enabled = false;

        introCamera.Priority = 11;
        introCamera.LookAt = playerCar.transform;
        introCamera.Follow = playerCar.transform;
        mainCamera.Priority = 9;

        countDownAudio.Play();

        yield return new WaitForSeconds(countDownAudio.clip.length);

        playerCar.GetComponent<CarController>().enabled = true;
        mainCamera.Priority = 11;
        introCamera.Priority = 9;
    }

}
