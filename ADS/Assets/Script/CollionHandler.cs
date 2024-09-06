using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollionHandler : MonoBehaviour
{
    private Ads ads;

    public GameObject panel;
    public Button contiuneButton, restartButton;

    private void Start()
    {
        ads = FindObjectOfType<Ads>();
        panel.SetActive(false);

        contiuneButton.onClick.AddListener(OnContiuneClicked);
        restartButton.onClick.AddListener(OnRestartClicked);
        ads.LoadAd();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Temas");
            ads.LoadInterstitialAd();
            panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void OnContiuneClicked()
    {
        panel.SetActive(false);
        Time.timeScale = 1.0f;
        ads.ShowRewardedAd();
    }

    private void OnRestartClicked()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
