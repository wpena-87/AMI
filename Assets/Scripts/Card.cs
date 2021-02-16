using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    Sprite face;
    Sprite back;
    SpriteRenderer spriteRenderer;

    bool matched = false;

    static List<Card> shownCards = new List<Card>();


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMouseDown()
    {
        if (matched)
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
            shownCards[0].matched = true;
            shownCards[1].matched = true;
            shownCards.Clear();
            ScoreManager.RaiseScore(25);
        }
    }

    private void Disappear()
    {

        if (gameObject.GetComponent<Transform>().localScale.x > 0)
        {
            gameObject.GetComponent<Transform>().localScale -= 3 * Vector3.one * Time.deltaTime;
        }
        else
        {
            gameObject.GetComponent<Transform>().localScale = Vector3.zero;
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

    private void Update()
    {
        if (matched)
        {
            Disappear();
        }
    }
}
