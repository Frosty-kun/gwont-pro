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
    public bool lastRoundWinner=false;
    private int turnCounter=1;
    private int roundCounter=1;
    private int player1Score=0;
    private int player2Score=0;
    private string playerTurnName="1";

    private BoardManager boardManager;
    private DragDrop dragDrop;
    private GameObject TurnText;
    
    
    public DragDrop[] dragDropList;

    GameObject areaReference;

    void OnEnable()
    {
        boardManager = GameObject.Find("Main Canvas").GetComponent<BoardManager>();
        dragDrop = GetComponent<DragDrop>();
        TurnText = GameObject.Find("TurnText");
     
        TurnText.GetComponent<Text>().text = "turno del jugador "+playerTurnName;
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
        if(player1Turn)
        {
            playerTurnName="1";
        }
        else if(player2Turn)
        {
            playerTurnName="2";
        }

        TurnText.GetComponent<Text>().text = "turno del jugador "+playerTurnName;
    }

    public void ChangeDragTurn()
    {
        areaReference = GameObject.Find("CardsArea");
        dragDropList = areaReference.GetComponentsInChildren<DragDrop>();

        foreach(DragDrop dragDrop in dragDropList)
        {
            dragDrop.itsTurn = !dragDrop.itsTurn;
        }
    }

    public void Pass()
    {
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
            ChangeDragTurn();
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
            ChangeDragTurn();
        }
        if(player1Passed&&player2Passed)
        {
            ResolveRound();
        }
    }

    public void ResolveRound()
    {
        int resolveDamage1 = boardManager.FieldDamage(false);
        int resolveDamage2 = boardManager.FieldDamage(true);

        Debug.Log(resolveDamage1+" "+resolveDamage2);

        if(resolveDamage1>resolveDamage2)
        {
            player1Score++;
            Debug.Log("PLAYER 1 WON THE ROUND");
        }
        else if(resolveDamage1<resolveDamage2)
        {
            player2Score++;
            Debug.Log("PLAYER 2 WON THE ROUND");
        }
        else
        {
            player1Score++;
            player2Score++;
            Debug.Log("ROUND TIED");
        }

        if(player1Score == 2 && player1Score != player2Score)
        {
            Debug.Log("PLAYER 1 WON THE GAME");
        }
        if(player2Score == 2 && player1Score != player2Score)
        {
            Debug.Log("PLAYER 2 WON THE GAME");
        }
        else if(player1Score==2 && player1Score == player2Score)
        {
            Debug.Log("GAME TIED");
        }

        roundCounter++;
    }

    public void OnClick()
    {   
        Pass();
    }
}
