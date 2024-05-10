using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnHandler : MonoBehaviour
{
    //Se encarga de cambiar de turno y rondas, y verifica que jugador ganó la ronda y la partida

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

    private DeckList deckList1;
    private DeckList deckList2;
    private BoardManager boardManager;
    private DragDrop dragDrop;
    private GameObject TurnText;
    
    
    public DragDrop[] dragDropList;

    GameObject areaReference;

    void OnEnable()
    {
        deckList1 = GameObject.Find("Deck1").GetComponent<DeckList>();
        deckList2 = GameObject.Find("Deck2").GetComponent<DeckList>();
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

            areaReference = GameObject.Find("PlayerArea1");
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

            areaReference = GameObject.Find("PlayerArea2");
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

    private void NewRoundStart(int i)
    {
        if(i==1)
        {
            player1Turn = true;
        }
        else
        {
            player2Turn = true;
        }

        player1Passed = false;
        player2Passed = false;
        areaReference = GameObject.Find("CardsArea");
        dragDropList = areaReference.GetComponentsInChildren<DragDrop>();
        foreach(DragDrop dragDrop in dragDropList)
        {
            dragDrop.hasPassed = false;
            dragDrop.itsTurn = false;
        }

        areaReference = GameObject.Find("PlayerArea"+i);
        dragDropList = areaReference.GetComponentsInChildren<DragDrop>();
        
        foreach(DragDrop dragDrop in dragDropList)
        {
            dragDrop.itsTurn = true;
        }

        GameObject[] WeathersToDestroy = GameObject.FindGameObjectsWithTag("Weather");
        foreach(GameObject gameObject in WeathersToDestroy)
        {
            if(gameObject.transform.childCount!=0)
            {
                Destroy(gameObject.transform.GetChild(0).gameObject);
            }
        }

        deckList1.DrawTwo();
        deckList2.DrawTwo();

        TurnText.GetComponent<Text>().text = "turno del jugador "+i;
    }

    public void ResolveRound()
    {
        int resolveDamage1 = boardManager.player1damage;
        int resolveDamage2 = boardManager.player2damage;

        Debug.Log(resolveDamage1+" "+resolveDamage2);

        if(resolveDamage1>resolveDamage2)
        {
            player1Score++;
            Debug.Log("PLAYER 1 WON THE ROUND");
            NewRoundStart(1);
        }
        else if(resolveDamage1<resolveDamage2)
        {
            player2Score++;
            Debug.Log("PLAYER 2 WON THE ROUND");
            NewRoundStart(2);
        }
        else
        {
            player1Score++;
            player2Score++;
            Debug.Log("ROUND TIED");
            NewRoundStart(1);
        }

        boardManager.KillCards();

        boardManager.FieldDamage();

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
