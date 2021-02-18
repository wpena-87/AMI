using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    
    private static float score = 0;
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

    public static void RaiseScore(float points)
    {
        score += points;
        RefreshScoreText();
        
        if (score == 100)
        {
            instance.StartCoroutine(GameOver());
        }
    }

    private static IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1);
        GameObject.Find("MoveOut").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(4.5f);
        SceneManager.LoadScene("GameOver");
    }

    public static float GetScore()
    {
        return score;
    }
}
