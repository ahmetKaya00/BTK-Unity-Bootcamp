using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] carPrefab;
    private int selectedCarIndex = 0;

    private const string SelectedCarKey = "SelectedCarIndex";

    public void SelectedCar(int index)
    {
        if(index < 0 || index >= carPrefab.Length)
        {
            Debug.LogWarning("Invalid Car");
            return;
        }

        selectedCarIndex = index;
        PlayerPrefs.SetInt(SelectedCarKey, selectedCarIndex);
        PlayerPrefs.Save();

        Debug.Log($"Car {SelectedCarKey} selected");
    }

    public void StartRace(string sceneManager) {

        if (string.IsNullOrEmpty(sceneManager))
        {
            Debug.LogWarning("Invalid Scene");
            return;
        }

        SceneManager.LoadScene(sceneManager);
    
    }
}
