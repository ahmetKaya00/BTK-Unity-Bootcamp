using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button myButton;
    void Start()
    {
        myButton.onClick.AddListener(onButtonClicked);
    }

    public void onButtonClicked()
    {
        Debug.Log("Butona Týklandý!");
    }
}
