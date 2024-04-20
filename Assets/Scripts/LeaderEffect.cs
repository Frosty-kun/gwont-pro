using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderEffect : MonoBehaviour
{
    //Administra los efectos de los lideres que son llamados mediante las funciones GoldenLeaderEffect y DarkLeaderEffect

    private TurnHandler turnHandler;
    private DeckList deckList1; 
    private DeckList deckList2;
    public bool leaderEffectAviable=true;
    private GameObject Leader1;
    private GameObject Leader2;

    void OnEnable()
    {
        turnHandler = GameObject.Find("TurnText").GetComponent<TurnHandler>();
        deckList1 = GameObject.Find("Deck1").GetComponent<DeckList>();
        deckList2 = GameObject.Find("Deck2").GetComponent<DeckList>();

        Leader1 = GameObject.Find("Leader1");
        Leader2 = GameObject.Find("Leader2");
    }

    void Update()
    {
        if(turnHandler.player1Turn)
        {
            if(Input.GetKey(KeyCode.Space)&&Leader1.GetComponent<LeaderEffect>().leaderEffectAviable)
            {
                Debug.Log("useleader1 effect");
                GoldenLeaderEffect();
                Leader1.GetComponent<LeaderEffect>().leaderEffectAviable=false;
            }
        }
        else if(turnHandler.player2Turn)
        {
            if(Input.GetKey(KeyCode.Space)&&Leader2.GetComponent<LeaderEffect>().leaderEffectAviable)
            {
                Debug.Log("useleader2 effect");
                DarkLeaderEffect();
                Leader2.GetComponent<LeaderEffect>().leaderEffectAviable=false;
            }
        }
    }

    void GoldenLeaderEffect()
    {
        for(int i=0; i<deckList1.cardsList.Count;i++)
        {
            if(deckList1.cardsList[i]!=null&&deckList1.cardsList[i].name=="WarCry")
            {
                GameObject referenceCard = Instantiate(deckList1.cardsList[i]);
                deckList1.cardsList.RemoveAt(i);
                break;
            }            

        }
    }
    void DarkLeaderEffect()
    {
        for(int i=0; i<deckList2.cardsList.Count;i++)
        {
            if(deckList2.cardsList[i]!=null&&deckList2.cardsList[i].name=="RedMoon")
            {
                GameObject referenceCard = Instantiate(deckList2.cardsList[i]);
                referenceCard.transform.Rotate(0f,0f,180f);
                referenceCard.GetComponent<DragDrop>().itsTurn=true;
                deckList2.cardsList.RemoveAt(i);
                break;
            }            
        }
    }
}
