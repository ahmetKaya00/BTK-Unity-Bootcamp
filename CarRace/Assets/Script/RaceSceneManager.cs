using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceSceneManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] carPrefabs;
    private const string SelectedCarKey = "SelectedCarIndex";

    public static GameObject PlayerCar;

    private void Start()
    {
        int selecteedCarIndex = PlayerPrefs.GetInt(SelectedCarKey,0);

        if(selecteedCarIndex < 0 || selecteedCarIndex >= carPrefabs.Length)
        {
            Debug.LogWarning("Invalid Car");
            return;
        }

        PlayerCar = Instantiate(carPrefabs[selecteedCarIndex], spawnPoint.position, spawnPoint.rotation);
    }
}
