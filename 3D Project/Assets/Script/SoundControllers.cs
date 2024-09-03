using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControllers : MonoBehaviour
{
    private AudioSource AudioSource;
    public AudioClip AudioClip;
    public Transform Camera;
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.clip = AudioClip;
    }

    void Update()
    {
        float mesafe = Vector3.Distance(transform.position, Camera.position);
        float maxMesafe = 10f;
        float nomalizedDistance = Mathf.Clamp01(mesafe / maxMesafe);
        AudioSource.volume = 1f - nomalizedDistance;
    }
}
