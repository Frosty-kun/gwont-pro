using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckList : MonoBehaviour
{
    public List <GameObject> cardsList;
    public GameObject Hand;
    public GameObject DeleteZone;
    public GameObject Cemetery;
    public GameObject childCard;
    public int firstDraw;
    public GameObject[] childrenCards;

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

    public void KillCards()
    {
        for(int i=1; i<=3; i++)
        {
            Cemetery = GameObject.Find("Cemetery"+i);
            Debug.Log(i);

            for(int j=0; j<=2;j++)
            {
                if(j==0)
                {
                    DeleteZone = GameObject.Find("MeleeZone"+i);
                }
                else if(j==1)
                {
                    DeleteZone = GameObject.Find("RangedZone"+i);
                }
                else if(j==2)
                {
                    DeleteZone = GameObject.Find("SiegeZone"+i);
                }

                if(DeleteZone!=null)
                {
                    childrenCards = new GameObject[DeleteZone.transform.childCount];
                    for(int k=0; k<childrenCards.Length; k++)
                    {
                        Debug.Log("Warning");
                        childrenCards[k] = DeleteZone.transform.GetChild(0).gameObject;
                        DeleteZone.transform.GetChild(0).transform.SetParent(Cemetery.transform);
                    }
                }
            }
        }
        // childrenCards = DeleteZone.GetComponentsInChildren<Transform>();
        // foreach(Transform childCard in childrenCards)
        // {
        //     childCard.SetParent(Cemetery.transform);
        // }

    }





}
