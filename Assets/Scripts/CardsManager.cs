using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    public GameObject Card;
    public Sprite[] cardFaces;
    public Sprite cardBack;
    static System.Random random = new System.Random();
    
    void Start()
    {
        PlaceCards();
    }

    private List<Sprite> GetShuffleFaces()
    {
        List<Sprite> shuffledFaces = new List<Sprite>();

        for (int i = 0; i < 4; i++)
        {
            Sprite face = cardFaces[random.Next(cardFaces.Length)];
            while (shuffledFaces.Contains(face))
            {
                face = cardFaces[random.Next(cardFaces.Length)];
            }
            shuffledFaces.Add(face);
            shuffledFaces.Insert(random.Next(shuffledFaces.Count), face);
        }
        
        return shuffledFaces;
    }

    private void PlaceCards()
    {
         List<Sprite> shuffledFaces = GetShuffleFaces();

        int x = -357;
        int y =  198;
        
        int k = 0;
        foreach (Sprite cardFace in shuffledFaces)
        {
            GameObject card = Instantiate(Card, new Vector3(x, y, 0), Quaternion.identity);
            card.GetComponent<Card>().setFace(cardFace);
            card.GetComponent<Card>().setBack(cardBack);
            x += 200;
            if (k == 3)
            {
                x = -357;
                y = - 52;
            }
            k++;
        }
    }

}
