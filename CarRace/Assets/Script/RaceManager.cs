using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    public static RaceManager instance;
    public Text finishText;
    public GameObject finishPanel;


    private List<CarTemas> finishedCars = new List<CarTemas> ();

    int totalCars = 2;

    private void Awake()
    {
        finishPanel.SetActive (false);
        if(instance == null)
        {
            instance = this;
        }
    }

    public void CarFinished(CarTemas car)
    {
        if (!finishedCars.Contains(car)){

            finishedCars.Add(car);

            if(finishedCars.Count == totalCars)
            {
                DisplayFinishPanel();
            }
        }
    }

    void DisplayFinishPanel()
    {
        if(finishPanel == null)
        {
            Debug.LogError("Invalid panel");
            return;
        }

        if (finishText == null)
        {
            Debug.LogError("Invalid text");
            return;
        }

        finishPanel.SetActive (true);
        string result = "Race Result:\n";

        finishedCars.Sort((x, y) => x.finishTime.CompareTo(y.finishTime));

        for(int i = 0; i < finishedCars.Count; i++)
        {
            result += (i + 1) + ". " + finishedCars[i].name + " - " + finishedCars[i].finishTime.ToString("f2") + "s\n";
        }

        finishText.text = result;


    }
}
