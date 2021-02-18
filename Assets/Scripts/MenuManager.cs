using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject[] leftCards;
    public GameObject[] rightCards;
    public Button startButton;
    public AudioSource startSound;
    public AudioSource awakeSound;
    int x;
    float timeCounter;
    readonly float SPEED = 3000;
    enum State{
        IN,
        OUT,
        WAIT,
        END
    }
    State state = State.IN;

    private void OnStartButtonClick()
    {
        state = State.OUT;
        startSound.Play();
        startButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {

        x = -357;
        timeCounter = 0;
        awakeSound.Play();
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void MoveCards()
    {
        Vector3 xVelocity = new Vector3(SPEED, 0, 0) * Time.deltaTime;
        for (int i = 0; i < 4; i++)
        {
            leftCards[i].transform.position += xVelocity;
            rightCards[i].transform.position -= xVelocity;
        }
    }

    private void MoveButton(int dir)
    {
        Vector3 yVelocity = new Vector3(0, SPEED, 0) * Time.deltaTime;
        startButton.transform.position += dir * yVelocity;
    }

    void Update()
    {
        switch (state)
        {
            case State.IN:
                MoveCards();
                MoveButton(1);
                if (leftCards[0].transform.position.x >= x)
                {
                    Vector3 pos;
                    state = State.WAIT;
                    for (int i = 0; i < 4; i++)
                    {
                        pos = leftCards[i].transform.position;
                        pos.x = x;
                        leftCards[i].transform.position = pos;

                        pos = rightCards[i].transform.position;
                        pos.x = x;
                        rightCards[i].transform.position = pos;

                        x += 200;
                    }
                    pos = startButton.transform.localPosition;
                    pos.y = -195.3125f;
                    startButton.transform.localPosition = pos;
                }
                break;
            case State.WAIT:
                timeCounter += Time.deltaTime;
                if (timeCounter < 0.75)
                {
                    Color buttonColor = startButton.image.color;
                    buttonColor.a -= Time.deltaTime;
                    startButton.image.color = buttonColor;
                }
                else if (timeCounter < 1.5)
                {
                    Color buttonColor = startButton.image.color;
                    buttonColor.a += Time.deltaTime;
                    startButton.image.color = buttonColor;
                }
                else
                {
                    timeCounter = 0;
                }
                break;
            case State.OUT:
                MoveCards();
                MoveButton(-1);
                if (rightCards[0].transform.position.x <= -1600)
                {
                    state = State.END;
                }
                break;
            case State.END:
                timeCounter += Time.deltaTime;
                if (timeCounter > 3.325)
                {
                    SceneManager.LoadScene("InGame");
                }
                break;
        }
    }
}
