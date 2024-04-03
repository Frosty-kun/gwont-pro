using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnHandler : MonoBehaviour
{
    public bool playerTurn=false;
    private int turnCounter=1;

    private CardStats cardStats;
    private DragDrop dragDrop;
    private GameObject TurnText;
    private GameObject Player1Damage;
    private GameObject Player2Damage;
    public CardStats[] cardStatsList1;
    public CardStats[] cardStatsList2;
    public DragDrop[] dragDropList;

    GameObject areaReference;

    void Start()
    {
        cardStats = GetComponent<CardStats>();
        dragDrop = GetComponent<DragDrop>();
        TurnText = GameObject.Find("TurnText");

        Player1Damage = GameObject.Find("Player1Damage");
        Player2Damage = GameObject.Find("Player2Damage");

        TurnText.GetComponent<Text>().text = "Turno:"+turnCounter;
    }

    public void DisableInteraction()
    {
        areaReference = GameObject.Find("CardsArea");
        if(!playerTurn)
        {
            dragDropList = areaReference.GetComponentsInChildren<DragDrop>();

            foreach(DragDrop dragDrop in dragDropList)
            {
                dragDrop.itsTurn = !dragDrop.itsTurn;
            }
        }
        else
        {
            dragDropList = areaReference.GetComponentsInChildren<DragDrop>();

            foreach(DragDrop dragDrop in dragDropList)
            {
                dragDrop.itsTurn = !dragDrop.itsTurn;
            }
        }     
    }

    public void ChangeTurn()
    {
        playerTurn = !playerTurn;
        TurnText.GetComponent<Text>().text = "Turno:" + turnCounter.ToString();
    }

    public void FieldDamage()
    {
        int damageCounter = 0;

        areaReference = GameObject.Find("SetZone1");
        cardStatsList1 = areaReference.GetComponentsInChildren<CardStats>();
        foreach(CardStats cardStats in cardStatsList1)
        {
            damageCounter+=cardStats.attackDamage;
        }

        Player1Damage.GetComponent<Text>().text = "Ataque:"+damageCounter;

        damageCounter = 0;

        areaReference = GameObject.Find("SetZone2");
        cardStatsList2 = areaReference.GetComponentsInChildren<CardStats>();
        foreach(CardStats cardStats in cardStatsList2)
        {
            damageCounter+=cardStats.attackDamage;
        }

        Player2Damage.GetComponent<Text>().text = "Ataque:"+damageCounter;

    }

    public void OnClick()
    {
        turnCounter++;
        Debug.Log("Turn:"+turnCounter);
        ChangeTurn();
        DisableInteraction();
    }
}
