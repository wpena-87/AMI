﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RowsManager : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform canvas;
    public Text continueLabel;
    float timeCounter;
    int y;

    void Start()
    {
        y = -66;
        timeCounter = 0;
        foreach (var item in ScoreManager.leaderboard)
        {
            GameObject row = Instantiate(rowPrefab, canvas);
            row.GetComponent<RectTransform>().localPosition = new Vector3(0, y, 0);
            row.transform.GetChild(0).GetComponent<Text>().text = (item.Position + 1) + ".-";
            row.transform.GetChild(1).GetComponent<Text>().text = "" + item.StatValue;
            if (item.PlayFabId == Initializer.myId)
            {
                Color color = new Color(1, 174 / 255f, 57 / 255f);
                row.transform.GetChild(0).GetComponent<Text>().color = color;
                row.transform.GetChild(1).GetComponent<Text>().color = color;
            }
            y -= 70;
        }
    }

    void OnMouseDown()
    {
        SceneManager.LoadScene("Menu");
    }

    void Update()
    {
        Color color = continueLabel.color;
        timeCounter += Time.deltaTime;
        if (timeCounter < 0.75)
        {
            color.a -= Time.deltaTime;
        }
        else if (timeCounter < 1.5)
        {
            color.a += Time.deltaTime;
        }
        else
        {
            timeCounter = 0;
        }
        continueLabel.color = color;
    }
}
