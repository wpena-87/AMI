using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text label;
    public Image logo;
    public Sprite win;
    public Sprite loss;

    void Start()
    {
        if (ScoreManager.GetScore() == 100)
        {
            label.text = "You Won!";
            logo.sprite = win;
            GameObject.Find("Victory").GetComponent<AudioSource>().Play();
        }
        else
        {
            label.text = "You Lost!";
            logo.sprite = loss;
            GameObject.Find("Defeat").GetComponent<AudioSource>().Play();
            logo.rectTransform.position += new Vector3(0, 100, 0);
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
}
