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


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (isMatched || shownCards.Contains(this) || shownCards.Count == 2)
        {
            return;
        }

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
            }
            else
            {
                shownCards[0].canFlip = true;
                shownCards[1].canFlip = true;
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
            ScoreManager.RaiseScore(12.5f);
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

    private void Update()
    {
        if (spriteRenderer.color.a < 1)
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

        if (ScoreManager.GetScore() == 100)
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
