using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckList : MonoBehaviour
{
    public List <GameObject> cardsList;
    public GameObject Hand;
    public int firstDraw;

    void Start()
    {
        int i=0;
        while(i!=firstDraw)
        {
            GameObject card = Instantiate(cardsList[i], new Vector2(0, 0), Quaternion.identity);
            card.transform.SetParent(Hand.transform, false);
            cardsList.RemoveAt(firstDraw);
            i++;
            // card.name = $"{card.name}{i}";
         }
    }
}
