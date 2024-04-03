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
    public CardStats[] cardStatsList;
    public DragDrop[] dragDropList;

    GameObject areaReference;

    void Start()
    {
        cardStats = GetComponent<CardStats>();
        dragDrop = GetComponent<DragDrop>();
        TurnText = GameObject.Find("TurnText");

        TurnText.GetComponent<Text>().text = "Turno:"+turnCounter;
    }

    public void DisableInteraction()
    {
        areaReference = GameObject.Find("Main Canvas");
        if(!playerTurn)
        {
            cardStatsList = areaReference.GetComponentsInChildren<CardStats>();
            dragDropList = areaReference.GetComponentsInChildren<DragDrop>();

            foreach(DragDrop dragDrop in dragDropList)
            {
                dragDrop.itsTurn = !dragDrop.itsTurn;
            }
        }
        else
        {
            cardStatsList = areaReference.GetComponentsInChildren<CardStats>();
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
        areaReference = GameObject.Find("Player1Area");
    }

    public void OnClick()
    {
        turnCounter++;
        Debug.Log("Turn:"+turnCounter);
        ChangeTurn();
        DisableInteraction();
    }
}
