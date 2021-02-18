using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    public GameObject heartPrefab;
    private static int lives = 3;
    private static Heart[] hearts = new Heart[lives];
    private static LivesManager instance;
    int x = 410;
    int y = 160;

    void Awake()
    {
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
        yield return new WaitForSeconds(1.25f);
        GameObject.Find("Vanish").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(4.5f);
        ScoreManager.UpdateLeaderboard();
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
