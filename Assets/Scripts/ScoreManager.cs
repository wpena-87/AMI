using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    
    private static int score = 0;
    private static Text scoreText;
    private static ScoreManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        RefreshScoreText();
    }

    private static void RefreshScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    public static void RaiseScore(int points)
    {
        score += points;
        RefreshScoreText();
        
        if (score == 100)
        {
            instance.StartCoroutine(GameOver());
        }
    }

    static IEnumerator GameOver()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("GameOver");
    }

    public static int GetScore()
    {
        return score;
    }
}
