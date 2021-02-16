using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    int id;
    Sprite face;
    Sprite back;
    SpriteRenderer spriteRenderer;

    bool isMatched = false;
    bool isDisappeared = false;

    static List<Card> shownCards = new List<Card>();


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMouseDown()
    {
        if (isMatched)
        {
            return;
        }
        
        if (shownCards.Contains(this))
        {
            spriteRenderer.sprite = back;
            shownCards.Remove(this);
            return;
        }

        if (shownCards.Count < 2)
        {
            spriteRenderer.sprite = face;
            shownCards.Add(this);
        }

        if (shownCards.Count == 2 && shownCards[0].face == shownCards[1].face)
        {
            shownCards[0].isMatched = true;
            shownCards[1].isMatched = true;
            shownCards.Clear();
            ScoreManager.RaiseScore(25);
        }
    }

    private void Disappear()
    {

        if (gameObject.GetComponent<Transform>().localScale.x > 0)
        {
            gameObject.GetComponent<Transform>().localScale -= 2 * Vector3.one * Time.deltaTime;
        }
        else
        {
            gameObject.GetComponent<Transform>().localScale = Vector3.zero;
            isDisappeared = true;
        }

    }
    private IEnumerator Appear()
    {
        yield return new WaitForSeconds(1);
        if (gameObject.GetComponent<Transform>().localScale.x < 1)
        {
            gameObject.GetComponent<Transform>().localScale += 2.5f * Vector3.one * Time.deltaTime;
        }
        else
        {
            gameObject.GetComponent<Transform>().localScale = Vector3.one;
        }

    }
    private IEnumerator MoveAway()
    {
        yield return new WaitForSeconds(1.5f + id/4f);
        Vector3 xVelocity = new Vector3(2000, 0, 0);
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
        if (isMatched && !isDisappeared)
        {
            Disappear();
        }
        if (ScoreManager.GetScore() == 100)
        {
            StartCoroutine(Appear());
            StartCoroutine(MoveAway());
        }
    }
}
