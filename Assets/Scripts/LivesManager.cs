﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    public GameObject heartPrefab;
    private static int lives;
    private static Heart[] hearts;
    private static LivesManager instance;
    int x;
    int y;

    void Awake()
    {
        lives = 3;
        hearts = new Heart[lives];
        x = 410;
        y = 160;
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < lives; i++)
        {
            GameObject heart = Instantiate(heartPrefab, new Vector3(x, y, 0), Quaternion.identity);
            hearts[i] = heart.GetComponent<Heart>();
            y -= 75;
        }
    }

    public static void DecreaseOneLive()
    {
        lives--;
        hearts[lives].Destroy();
        if (lives == 0)
        {
            instance.StartCoroutine(GameOver());
        }
    }

    private static IEnumerator GameOver()
    {
        MusicController.decreaseMusicVolume = true;
        ScoreManager.UpdateLeaderboard();
        yield return new WaitForSeconds(1.25f);
        GameObject.Find("Vanish").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(4.5f);
        SceneManager.LoadScene("GameOver");
    }

    public static bool HaveLost()
    {
        return lives == 0;
    }

    public static int getLives()
    {
        return lives;
    }
}
