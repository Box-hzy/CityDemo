using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UImanager : MonoBehaviour
{
    public static UImanager instance;

    public int totalScore;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI resultText;
    public GameObject resultPanel;

    private void Awake()
    {
        if(instance == null)
        instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        resultPanel.SetActive(false);
        totalScore = 0;
        totalScoreText.text = totalScore.ToString();
    }

    public void UpdateScore(int score)
    { 
       totalScore += score;
        totalScoreText.text = totalScore.ToString();
    }

    public void ShowResult()
    {
        Time.timeScale = 0;
        resultPanel.SetActive(true);
        resultText.text = totalScoreText.text;
    }
}
