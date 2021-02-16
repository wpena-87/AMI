using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    
    private static int score = 0;
    private static Text scoreText;
    private static ScoreManager self;

    private void Awake()
    {
        self = this;
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
            self.StartCoroutine(GameOver());
        }
    }

    static IEnumerator GameOver()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("GameOver");
    }

}
