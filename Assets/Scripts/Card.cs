using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    int id;
    float timeCounter = 0;

    Sprite face;
    Sprite back;
    SpriteRenderer spriteRenderer;

    bool isMatched = false;
    bool isDisappeared = false;
    bool canFlip = false;

    static List<Card> shownCards = new List<Card>();
    static AudioSource matchSound;
    static AudioSource wrongSound;
    static AudioSource clickSound;



    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        matchSound = GameObject.Find("Match").GetComponent<AudioSource>();
        wrongSound = GameObject.Find("Wrong").GetComponent<AudioSource>();
        clickSound = GameObject.Find("Click").GetComponent<AudioSource>();
    }

    void Start()
    {
        Color color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.color = color;
    }

    private void FlipBack()
    {
        if (timeCounter < 0.5f)
        {
            timeCounter += Time.deltaTime;
            return;
        }
        Vector3 xVelocity = new Vector3(6, 0, 0);
        if (spriteRenderer.transform.localScale.x > 0 && spriteRenderer.sprite == face)
        {
            spriteRenderer.transform.localScale -= xVelocity * Time.deltaTime;
            if (spriteRenderer.transform.localScale.x <= 0)
            {
                spriteRenderer.sprite = back;
            }
        }
        else if (spriteRenderer.transform.localScale.x < 1 && spriteRenderer.sprite == back)
        {
            spriteRenderer.transform.localScale += xVelocity * Time.deltaTime;
            if (spriteRenderer.transform.localScale.x >= 1)
            {
                spriteRenderer.transform.localScale = Vector3.one;
                canFlip = false;
                shownCards.Clear();
                timeCounter = 0;
            }
        }
    }

    public void OnMouseDown()
    {
        if (isMatched ||
            shownCards.Contains(this) ||
            shownCards.Count == 2 ||
            LivesManager.HaveLost())
        {
            return;
        }

        clickSound.Play();

        if (shownCards.Count < 2)
        {
            spriteRenderer.sprite = face;
            shownCards.Add(this);
        }

        if (shownCards.Count == 2)
        {
            if (shownCards[0].face == shownCards[1].face)
            {
                shownCards[0].isMatched = true;
                shownCards[1].isMatched = true;
                matchSound.Play();
            }
            else
            {
                shownCards[0].canFlip = true;
                shownCards[1].canFlip = true;
                wrongSound.Play();
                LivesManager.DecreaseOneLive();
            }
        }
    }

    private void Disappear()
    {
        if (timeCounter < 0.5f)
        {
            timeCounter += Time.deltaTime;
            return;
        }
        if (gameObject.GetComponent<Transform>().localScale.x > 0)
        {
            gameObject.GetComponent<Transform>().localScale -= 3 * Vector3.one * Time.deltaTime;
        }
        else
        {
            gameObject.GetComponent<Transform>().localScale = Vector3.zero;
            isDisappeared = true;
            shownCards.Clear();
            timeCounter = 0;
            ScoreManager.RaiseScore(12.5f * LivesManager.getLives());
        }

    }
    private void Appear()
    {
        if (gameObject.GetComponent<Transform>().localScale.x < 1)
        {
            gameObject.GetComponent<Transform>().localScale += 2.5f * Vector3.one * Time.deltaTime;
        }
        else
        {
            gameObject.GetComponent<Transform>().localScale = Vector3.one;
        }

    }
    private void MoveAway()
    {
        Vector3 xVelocity = new Vector3(3000, 0, 0);
        if (gameObject.GetComponent<Transform>().position.x < 650)
        {
            gameObject.GetComponent<Transform>().position += xVelocity * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void setFace(Sprite face)
    {
        this.face = face;
    }

    public void setBack(Sprite back)
    {
        this.back = back;
    }

    public void setId(int id)
    {
        this.id = id;
    }

    private void RotateAndVanish()
    {
        foreach (Card card in CardsManager.cards)
        {
            if (card.canFlip)
            {
                return;
            }
        }
        timeCounter += Time.deltaTime;
        if (timeCounter >= 1)
        {
            Vector3 angles = spriteRenderer.transform.rotation.eulerAngles;
            angles.z += 100 * Time.deltaTime;
            spriteRenderer.transform.rotation = Quaternion.Euler(angles);
            Color color = spriteRenderer.color;
            color.a -= 0.5f * Time.deltaTime;
            spriteRenderer.color = color;
            if (spriteRenderer.transform.localScale.x > 0)
            {
                spriteRenderer.transform.localScale -= 0.5f * Vector3.one * Time.deltaTime;
            }
            
        }
        
    }

    private void Update()
    {
        if (LivesManager.HaveLost())
        {
            RotateAndVanish();
        }
        else if (spriteRenderer.color.a < 1)
        {
            Color color = spriteRenderer.color;
            color.a += 4 * Time.deltaTime;
            spriteRenderer.color = color;
        }

        if (canFlip)
        {
            FlipBack();
        }

        if (isMatched && !isDisappeared)
        {
            Disappear();
        }

        if (ScoreManager.GetMacthes() == 4)
        {
            if (timeCounter < 1)
            {
                timeCounter += Time.deltaTime;
                return;
            }
            else if (timeCounter < 1.5f + id / 4f)
            {
                timeCounter += Time.deltaTime;
                Appear();
                return;
            }
            MoveAway();
        }
    }
}
