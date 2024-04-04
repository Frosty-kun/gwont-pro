using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnHandler : MonoBehaviour
{
    public bool player1Turn=true;
    public bool player2Turn=false;
    public bool player1Passed=false;
    public bool player2Passed=false;
    private int turnCounter=1;
    private int roundCounter=1;
    private int player1Score=0;
    private int player2Score=0;
    private string playerTurnName="1";

    private CardStats cardStats;
    private DragDrop dragDrop;
    private GameObject TurnText;
    private GameObject Player1Damage;
    private GameObject Player2Damage;
    public CardStats[] FieldList1;
    public CardStats[] FieldList2;
    public CardStats[] HandList1;
    public CardStats[] HandList2;
    public DragDrop[] dragDropList;

    GameObject areaReference;

    void Start()
    {
        cardStats = GetComponent<CardStats>();
        dragDrop = GetComponent<DragDrop>();
        TurnText = GameObject.Find("TurnText");

        Player1Damage = GameObject.Find("Player1Damage");
        Player2Damage = GameObject.Find("Player2Damage");

        TurnText.GetComponent<Text>().text = "turno del jugador "+playerTurnName;
    }

    public void ChangeCardTurn()
    {
        areaReference = GameObject.Find("CardsArea");
        dragDropList = areaReference.GetComponentsInChildren<DragDrop>();

        foreach(DragDrop dragDrop in dragDropList)
        {
            dragDrop.itsTurn = !dragDrop.itsTurn;
        }
    }

    //cuando pasas el jugador no vuelve a jugar en la ronda, si los dos jugadores pasan entonces termina la ronda
    public void Pass()
    {
        //comprueba si le toca jugar al jugador 1 y si es asi pasa la ronda
        if(player1Turn)
        {
            player1Passed=true;

            areaReference = GameObject.Find("Player1Area");
            dragDropList = areaReference.GetComponentsInChildren<DragDrop>();

            foreach(DragDrop dragDrop in dragDropList)
            {
                dragDrop.hasPassed = true;
            }
            ChangeTurn();
            ChangeCardTurn();
        }
        else if(player2Turn)
        {
            player2Passed=true;

            areaReference = GameObject.Find("Player2Area");
            dragDropList = areaReference.GetComponentsInChildren<DragDrop>();

            foreach(DragDrop dragDrop in dragDropList)
            {
                dragDrop.hasPassed = true;
            }
            ChangeTurn();
            ChangeCardTurn();
        }
        
        

        //comprueba si los dos jugadores pasaron y termina la ronda
        if(player1Passed&&player2Passed)
        {
            ResolveRound();
        }
    }

    public void ResolveRound()
    {
        Debug.Log("ROUND RESOLVED");
    }

    public void ChangeTurn()
    {
        turnCounter++;

        if(player1Turn || player1Passed)
        {
            player1Turn = false;
        }
        else
        {
            player1Turn=true;
        }

        if(player2Turn || player2Passed)
        {
            player2Turn = false;
        }
        else
        {
            player2Turn = true;
        }

        Debug.Log("Se ha cambiado de turno");
        
        if(player1Turn)
        {
            playerTurnName="1";
        }
        else if(player2Turn)
        {
            playerTurnName="2";
        }

        TurnText.GetComponent<Text>().text = "turno del jugador "+playerTurnName;
        //Debug.Log("Turn:"+turnCounter.ToString());
    }

    public void FieldDamage()
    {
        int damageCounter = 0;

        areaReference = GameObject.Find("SetZone1");
        FieldList1 = areaReference.GetComponentsInChildren<CardStats>();
        foreach(CardStats cardStats in FieldList1)
        {
            damageCounter+=cardStats.attackDamage;
        }
        Player1Damage.GetComponent<Text>().text = "Ataque:"+damageCounter;

        damageCounter = 0;

        areaReference = GameObject.Find("SetZone2");
        FieldList2 = areaReference.GetComponentsInChildren<CardStats>();
        foreach(CardStats cardStats in FieldList2)
        {
            damageCounter+=cardStats.attackDamage;
        }
        Player2Damage.GetComponent<Text>().text = "Ataque:"+damageCounter;
    }

    public void HandManager()
    {
        areaReference = GameObject.Find("Hand1");
        HandList1 = areaReference.GetComponentsInChildren<CardStats>();
        foreach(CardStats cardStats in HandList1)
        {
            
        }
        areaReference = GameObject.Find("Hand2");
        HandList2 = areaReference.GetComponentsInChildren<CardStats>();
        foreach(CardStats cardStats in HandList2)
        {
            
        }
    }

    public void OnClick()
    {   
        Pass();
    }
}
