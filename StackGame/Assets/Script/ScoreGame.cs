using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreGame : MonoBehaviour
{
    private int score;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        GameManager.OnCubeSpawned += GameManager_OnCubespawned;
    }

    private void OnDestroy()
    {
        GameManager.OnCubeSpawned -= GameManager_OnCubespawned;
    }

    private void GameManager_OnCubespawned()
    {
        score++;
        text.text = "Score: " + score;
    }
}
