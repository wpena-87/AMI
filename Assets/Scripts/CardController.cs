using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    Sprite face;
    Sprite back;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMouseDown()
    {
        spriteRenderer.sprite = face;
    }

    public void setFace(Sprite face)
    {
        this.face = face;
    }

    public void setBack(Sprite back)
    {
        this.back = back;
    }
}
