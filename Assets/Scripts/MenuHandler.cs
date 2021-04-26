using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public GameObject titleText;
    public GameObject playButton;
    public GameObject howToButton;
    public GameObject goBackButton;
    public GameObject howToPlayText;
    public GameObject quitButton;
    public GameObject creditsText;

    public void EndGame()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void HowToPlay()
    {
        titleText.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        howToButton.gameObject.SetActive(false);
        goBackButton.gameObject.SetActive(true);
        howToPlayText.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(false);
        creditsText.gameObject.SetActive(false);
    }

    public void GoBack()
    {
        titleText.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        howToButton.gameObject.SetActive(true);
        goBackButton.gameObject.SetActive(false);
        howToPlayText.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(true);
        creditsText.gameObject.SetActive(true);
    }

}
