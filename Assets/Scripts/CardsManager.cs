using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Sprite[] cardFaces;
    public Sprite cardBack;
    public static Card[] cards;
    static System.Random random = new System.Random();
    
    void Start()
    {
        cards = new Card[8];
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
        for (int i = 0; i < shuffledFaces.Count; i++)
        {
            Sprite cardFace = shuffledFaces[i];
            GameObject card = Instantiate(cardPrefab, new Vector3(x, y, 0), Quaternion.identity);
            cards[i] = card.GetComponent<Card>();
            cards[i].setFace(cardFace);
            cards[i].setBack(cardBack);
            cards[i].setId(k);
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
