using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckList : MonoBehaviour
{
    public List <GameObject> cardsList;
    public GameObject Hand;
    void Start()
    {
        int i=0;
        while(i!=2)
        {
            GameObject card = Instantiate(cardsList[i], new Vector2(0, 0), Quaternion.identity);
            card.transform.SetParent(Hand.transform, false);
            cardsList.RemoveAt(i);
            i++;
            // card.name = $"{card.name}{i}";
         }
    }
}
