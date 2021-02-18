using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class Initializer : MonoBehaviour
{
    public Text label;
    float timeCounter;
    public static string myId;

    void Start()
    {
        timeCounter = 0;
        Login();
    }

    static void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    static void OnSuccess(LoginResult result)
    {
        Debug.Log("Succesful login/register account");
        myId = result.PlayFabId;
    }

    static void OnError(PlayFabError error)
    {
        Debug.Log("Error while login/register account");
        Debug.Log(error.GenerateErrorReport());
    }


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
