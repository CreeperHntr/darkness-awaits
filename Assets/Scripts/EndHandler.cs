using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndHandler : MonoBehaviour
{

    [SerializeField] private Text winnerText;
    [SerializeField] private Text readoutText;

    // Start is called before the first frame update
    void Awake()
    {
        if(GlobalVars.GetIsWinner())
        {
            winnerText.text = "You Won!";
        }
        else
        {
            winnerText.text = "You Lose";
        }
        readoutText.text = "Score: " + GlobalVars.GetTotalScore() + "\nDepth: " + GlobalVars.GetTotalDepth().ToString("F0");
    }

    public void MainMenu()
    {
        GlobalVars.ResetScore();
        GlobalVars.ResetDepth();
        GlobalVars.ResetTotalScore();
        GlobalVars.ResetTotalDepth();
        SceneManager.LoadScene("Title");
    }
}
