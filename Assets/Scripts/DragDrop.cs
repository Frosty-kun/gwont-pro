using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DragDrop : MonoBehaviour
{
    public GameObject Canvas;
    private GameObject TurnText;
    private GameObject Hand;
    private GameObject Placement;
    private GameObject Card;
    private GameObject draggedCard;
    private CardStats cardStats;
    private CardStats draggedCardStats;
    private CardStats targetCardStats;
    private TurnHandler turnHandler;
    private BoardManager boardManager;
    private GameObject targetCard;

    private bool wichPlayer=false;
    private int wichRange;
    private bool isDragging=false;
    private bool dummyCheck=false;
    private bool detectedCollision=false;
    private bool dummyCollision=false;
    public bool inHand=true;
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
        boardManager = Canvas.GetComponent<BoardManager>();

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
                Placement = GameObject.Find("SiegeZone2");
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
                Placement = GameObject.Find("SiegeZone1");
            }
        }
    }

    public void StartDrag()
    {
        draggedCard = gameObject;
        draggedCardStats = draggedCard.GetComponent<CardStats>();
        isDragging=true;
    }

    public void EndDrag()
    {
        draggedCard=null;
        draggedCardStats = null;
        isDragging=false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(Card.name+" entered collision");
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        // Debug.Log(Card.name+" is colliding");
        if(!cardStats.isDummy)
        {
            if(collision.gameObject == Card || collision.gameObject == Placement)
            {
                detectedCollision=true;
            }
        }
        else if(draggedCardStats!=null&&draggedCardStats.isDummy)
        {
            Debug.Log("Dummy Test");
            
            if(collision.gameObject!=null&&collision.gameObject.GetComponent<CardStats>()!=null)
            {
                targetCardStats = collision.gameObject.GetComponent<CardStats>();
                targetCard = collision.gameObject;
                DragDrop targetCardDragDrop = collision.gameObject.GetComponent<DragDrop>();

                if(!targetCardDragDrop.inHand)
                {
                    string targetCardPlacementName =  targetCard.transform.parent.name;
                    Placement = GameObject.Find(targetCardPlacementName);
                    dummyCollision=true;   
                    dummyCheck=true;
                }
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        // Debug.Log(Card.name+" exit collision");
        detectedCollision=false;
        dummyCollision=false;
        dummyCheck=false;
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

    void SetCardToZone()
    {
        //Debug.Log("Setting card to zone");
        Card.transform.SetParent(Placement.transform, true);
        inHand=false;

        boardManager.HandManager();

        hasPassed = turnHandler.player1Passed;
        hasPassed = turnHandler.player2Passed;
        boardManager.FieldDamage();
        // boardManager.FieldDamage(false);
        // boardManager.FieldDamage(true);

        if(turnHandler.player1Turn && boardManager.HandList1.Length == 0)
        {
            itsTurn = turnHandler.player1Turn;
            
            turnHandler.Pass();
        }
        else if(turnHandler.player2Turn && boardManager.HandList2.Length == 0)
        {
            itsTurn = turnHandler.player2Turn;
                
            turnHandler.Pass();
        }
        else if(turnHandler.player1Turn && !turnHandler.player2Passed)
        {
            turnHandler.ChangeDragTurn();
            turnHandler.ChangeTurn();
        }
        else if(turnHandler.player2Turn && !turnHandler.player1Passed)
        {
            turnHandler.ChangeDragTurn();
            turnHandler.ChangeTurn();
        }
    }

    void Update()
    {
        if(isDragging&&inHand&&itsTurn&&!hasPassed)
        {
            transform.position= new Vector2(Input.mousePosition.x,Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
        if(!isDragging&&!detectedCollision&&inHand&&!CheckParent())
        {
            Card.transform.SetParent(Hand.transform, true);
            //Debug.Log(Card.name+" returned to hand");
        }
        if(!isDragging&&inHand&&detectedCollision&&dummyCheck==false)
        {
            SetCardToZone();
        }
        if(!isDragging&&inHand&&dummyCollision&&dummyCheck==true&&!targetCardStats.isGolden)
        {
            if(targetCardStats!=null)
            {
                if(targetCardStats.player==false)
                {
                    Hand = GameObject.Find("Hand1");
                    targetCard.transform.SetParent(Hand.transform);
                }
                else
                {
                    Hand = GameObject.Find("Hand2");
                    targetCard.transform.SetParent(Hand.transform);

                }
            }
            SetCardToZone();
        }
    }
}
