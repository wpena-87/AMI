﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text label;
    public Image logo;
    public Sprite win;
    public Sprite loss;

    void Start()
    {
        ScoreManager.GetLeaderboard();
        if (ScoreManager.GetMacthes() == 4)
        {
            label.text = "You Won!";
            logo.sprite = win;
            GameObject.Find("Victory").GetComponent<AudioSource>().Play();
            StartCoroutine(showLeaderboard(5));
        }
        else
        {
            label.text = "You Lost!";
            logo.sprite = loss;
            GameObject.Find("Defeat").GetComponent<AudioSource>().Play();
            logo.rectTransform.position += new Vector3(0, 78.125f, 0);
            StartCoroutine(showLeaderboard(18));
        }
    }

    private void Move()
    {
        Vector3 xSpeed = new Vector3(1000 * Time.deltaTime, 0, 0);
        if (logo.GetComponent<RectTransform>().localPosition.x < 0)
        {
            logo.GetComponent<RectTransform>().localPosition += xSpeed;
            label.GetComponent<RectTransform>().localPosition -= xSpeed;
        }
    }

    void Update()
    {
        Move();
    }

    IEnumerator showLeaderboard(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("Leaderboard");
    }
}
