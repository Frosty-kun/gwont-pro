using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckList : MonoBehaviour
{
    //Crea listas para almacenar las cartas de cada deck

    public List <GameObject> cardsList;
    public GameObject Hand;
    private GameObject tempCard;
    public int firstDraw;
    public bool randomize;
    

    void Start()
    {
        if(randomize)
        {
            for(int j = 0; j < cardsList.Count; j++)
            {
                int Index = Random.Range(0, cardsList.Count);
                tempCard = cardsList[j];
                cardsList[j] = cardsList[Index];
                cardsList[Index] = tempCard;
            }
        }

        int i=0;
        while(i!=firstDraw)
        {
            if(cardsList[0]!=null)
            {
                GameObject card = Instantiate(cardsList[0], new Vector2(0, 0), Quaternion.identity);
                card.transform.SetParent(Hand.transform, false);
            }
            cardsList.RemoveAt(0);
            i++;
        }
    }

    public void DrawTwo()
    {
        for(int i=0; i<2; i++)
        {
            if(cardsList[0]!=null)
            {
                GameObject card = Instantiate(cardsList[0], new Vector2(0, 0), Quaternion.identity);
                card.transform.SetParent(Hand.transform, false);
            }
            cardsList.RemoveAt(0);
        }
    }

}