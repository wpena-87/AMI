using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviorManager : MonoBehaviour
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
        }
        else
        {
            label.text = "You Lost!";
            logo.sprite = loss;
            logo.rectTransform.position += new Vector3(0, 100, 0);
        }
    }

    private void Move()
    {
        Vector3 xSpeed = new Vector3(1000 * Time.deltaTime, 0, 0);
        if (logo.GetComponent<RectTransform>().position.x < 512)
        {
            logo.GetComponent<RectTransform>().position += xSpeed;
            label.GetComponent<RectTransform>().position -= xSpeed;
        }
        else
        {

        }
    }

    void Update()
    {
        Move();
    }
}
