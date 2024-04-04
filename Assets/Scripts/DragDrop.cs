using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private GameObject Canvas;
    private GameObject TurnText;
    private GameObject Hand;
    private GameObject Placement;
    private GameObject Card;
    private CardStats cardStats;
    private TurnHandler turnHandler;

    private bool wichPlayer=false;
    private int wichRange;
    private bool isDragging=false;
    private bool detectedCollision=false;
    private bool inHand=true;
    public bool itsTurn;
    public bool hasPassed;
    

    public void OnEnable()
    {
        cardStats = GetComponent<CardStats>();

        wichPlayer = cardStats.player;
        wichRange = cardStats.attackRange;

        Canvas = GameObject.Find("Main Canvas");
        
        Card=gameObject;

        TurnText = GameObject.Find("TurnText");

        turnHandler = TurnText.GetComponent<TurnHandler>();

        itsTurn=!cardStats.player;

        if(wichPlayer)
        {
            Hand = GameObject.Find("Hand2");
            if(wichRange==0)
            {
                Placement = GameObject.Find("MeleeZone2");
            }
            else if(wichRange==1)
            {
                Placement = GameObject.Find("RangedZone2");
            }
            else if(wichRange==2)
            {
                Placement = GameObject.Find("ArtilleryZone2");
            }
        }
        else
        {
            Hand = GameObject.Find("Hand1");
            if(wichRange==0)
            {
                Placement = GameObject.Find("MeleeZone1");
            }
            else if(wichRange==1)
            {
                Placement = GameObject.Find("RangedZone1");
            }
            else if(wichRange==2)
            {
                Placement = GameObject.Find("ArtilleryZone1");
            }
        }
    }

    public void StartDrag()
    {
        isDragging=true;
        
    }

    public void EndDrag()
    {
        isDragging=false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(Card.name+" entered collision");
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        // Debug.Log(Card.name+" is colliding");
        if(collision.gameObject == Card || collision.gameObject == Placement)
        {
            detectedCollision=true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        // Debug.Log(Card.name+" exit collision");
        detectedCollision=false;
    }

    bool CheckParent()
    {
        if(Card.transform.parent == Hand.transform)
        {
            //Debug.Log(Card.name+" is a child of Hand");
            return true;
        }
        else
        {
            //Debug.Log(Card.name+" is not a child of Hand");
            return false;
        }
    }

    void Update()
    {
        turnHandler.FieldDamage();

        if(isDragging&&inHand&&itsTurn&&!hasPassed)
        {
            transform.position= new Vector2(Input.mousePosition.x,Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
        if(!isDragging&&!detectedCollision&&inHand&&!CheckParent())
        {
            Card.transform.SetParent(Hand.transform, true);
            // Debug.Log(Card.name+" returned to hand");
        }
        if(!isDragging&&inHand&&detectedCollision)
        {
            Card.transform.SetParent(Placement.transform, true);
            inHand=false;

            turnHandler.HandManager();

            hasPassed = turnHandler.player1Passed;
            hasPassed = turnHandler.player2Passed;

            if(turnHandler.player1Turn && turnHandler.HandList1.Length == 0)
            {
                itsTurn = turnHandler.player1Turn;
                
                turnHandler.Pass();
            }
            else if(turnHandler.player2Turn && turnHandler.HandList2.Length == 0)
            {
                itsTurn = turnHandler.player2Turn;
                
                turnHandler.Pass();
            }
            else if(turnHandler.player1Turn && !turnHandler.player2Passed)
            {
                turnHandler.ChangeCardTurn();
                turnHandler.ChangeTurn();
            }
            else if(turnHandler.player2Turn && !turnHandler.player1Passed)
            {
                turnHandler.ChangeCardTurn();
                turnHandler.ChangeTurn();
            }
        }
    }
}
