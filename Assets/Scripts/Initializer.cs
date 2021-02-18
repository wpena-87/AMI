using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Initializer : MonoBehaviour
{
    public Text label;
    float timeCounter = 0;

    void OnMouseDown()
    {
        SceneManager.LoadScene("Menu");
    }

    void Update()
    {
        Color color = label.color;
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
        label.color = color;
    }
}
