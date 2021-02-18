using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class ScoreManager : MonoBehaviour
{
    private static float lastScore = 0;
    private static float score = 0;
    private static float matches;
    private static Text scoreText;
    private static ScoreManager instance;
    public static List<PlayerLeaderboardEntry> leaderboard;

    private void Awake()
    {
        if (lastScore < 250)
        {
            score = 0;
        }
        lastScore = 0;
        matches = 0;
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
        lastScore += points;
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
        UpdateLeaderboard();
        yield return new WaitForSeconds(1);
        GameObject.Find("MoveOut").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(4.5f);
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
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnLeaderboardUpdatedError);
    }

    static void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfully leaderboard sent");
    }

    static void OnLeaderboardUpdatedError(PlayFabError error)
    {
        Debug.Log("Error while updating leaderboard");
        Debug.Log(error.GenerateErrorReport());
    }

    public static void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Score",
            StartPosition = 0,
            MaxResultsCount = 5
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnLeaderboardGetError);
    }

    static void OnLeaderboardGet(GetLeaderboardResult result)
    {
        Debug.Log("Successfully leaderboard get");
        leaderboard = result.Leaderboard;
    }

    static void OnLeaderboardGetError(PlayFabError error)
    {
        Debug.Log("Error while getting leaderboard");
        Debug.Log(error.GenerateErrorReport());
    }
}
