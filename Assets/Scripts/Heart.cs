using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    enum HeartState
    {
        ACCENT,
        VANISH,
        NOTHING
    }
    HeartState state = HeartState.NOTHING;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        switch (state)
        {
            case HeartState.ACCENT:
                Color color = spriteRenderer.color;
                color.a += 2 * Time.deltaTime;
                spriteRenderer.color = color;
                if (color.a >= 2)
                {
                    color.a = 1;
                    spriteRenderer.color = color;
                    state = HeartState.VANISH;
                }
                break;
            case HeartState.VANISH:
                color = spriteRenderer.color;
                color.a -= Time.deltaTime;
                spriteRenderer.color = color;
                if (color.a <= 0)
                {
                    Destroy(gameObject);
                    state = HeartState.NOTHING;
                }
                break;
            case HeartState.NOTHING:
                break;
        }
    }

    public void Destroy()
    {
        state = HeartState.ACCENT;
    }
}
