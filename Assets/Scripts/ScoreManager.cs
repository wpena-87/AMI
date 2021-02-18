using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class ScoreManager : MonoBehaviour
{
    
    private static float score = 0;
    private static float matches = 0;
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
        matches += 0.5f;
        RefreshScoreText();
        
        if (matches == 4)
        {
            instance.StartCoroutine(GameOver());
        }
    }

    private static IEnumerator GameOver()
    {
        MusicController.decreaseMusicVolume = true;
        yield return new WaitForSeconds(1);
        GameObject.Find("MoveOut").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(4.5f);
        UpdateLeaderboard();
        SceneManager.LoadScene("GameOver");
    }

    public static float GetMacthes ()
    {
        return matches;
    }


    public static void UpdateLeaderboard()
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Score",
                    Value = (int)score,
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnLeaderboardError);
    }

    static void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfully leaderboard sent");
    }

    static void OnLeaderboardError(PlayFabError error)
    {
        Debug.Log("Error while updating leaderboard");
        Debug.Log(error.GenerateErrorReport());
    }
}
