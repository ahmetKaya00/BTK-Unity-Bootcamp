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
        StartCoroutine(WaitForRaceSceneManager());
    }

    private IEnumerator WaitForRaceSceneManager()
    {
        yield return new WaitUntil(() => RaceSceneManager.PlayerCar != null);
        playerCar = RaceSceneManager.PlayerCar;
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
        var carController = playerCar?.GetComponent<CarController>();

        if (carController != null) {
            carController.enabled = false;
        }

        introCamera.Priority = 11;
        introCamera.LookAt = playerCar?.transform;
        introCamera.Follow = playerCar?.transform;
        mainCamera.Priority = 9;
        mainCamera.LookAt = playerCar?.transform;
        mainCamera.Follow = playerCar?.transform;

        countDownAudio.Play();

        yield return new WaitForSeconds(countDownAudio.clip.length);

        playerCar.GetComponent<CarController>().enabled = true;

        if (carController != null)
        {
            carController.enabled = true;
        }
        mainCamera.Priority = 11;
        introCamera.Priority = 9;
    }

}
