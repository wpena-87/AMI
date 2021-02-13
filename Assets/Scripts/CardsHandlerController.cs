using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsHandlerController : MonoBehaviour
{
    public GameObject cardPrefab;

    
    void Start()
    {
        int x = -357;
        int y =  198;
        for (int i = 0; i < 8; i++)
        {
            Instantiate(cardPrefab, new Vector3(x, y, 0), new Quaternion());
            x += 200;
            if (i == 3)
            {
                x = -357;
                y = - 52;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
