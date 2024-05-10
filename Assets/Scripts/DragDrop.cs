using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DragDrop : MonoBehaviour
{
    //Administra el arrastre y el posicionamiento de cada carta, verifica si la mano paso del limite de 10 cartas, contiene la orden de pasar de turno


    public GameObject Canvas;
    private GameObject TurnText;
    private GameObject Hand;
    private GameObject Placement;
    private GameObject Card;
    private GameObject draggedCard;
    private GameObject targetZone;
    private GameObject cardToClear;
    private CardStats cardStats;
    private CardStats draggedCardStats;
    private CardStats targetCardStats;
    private TurnHandler turnHandler;
    private BoardManager boardManager;
    private WeatherManager weatherManager;
    private BuffManager buffManager;
    private GameObject targetCard;

    private bool wichPlayer=false;
    private int wichRange;
    private bool isDragging=false;
    private bool weatherCheck=true;
    private bool weatherCollision=false;
    private bool effectCollision=false;
    private bool detectedCollision=false;
    private bool dummyCheck=false;
    private bool dummyCollision=false;
    public bool inHand=true;
    public bool itsTurn;
    public bool hasPassed;
    
    
    public void OnEnable()
    {
        cardStats = GetComponent<CardStats>();

        weatherManager = GameObject.Find("Weathers").GetComponent<WeatherManager>();
        buffManager = GameObject.Find("CardsArea").GetComponent<BuffManager>();

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
        if(draggedCard!=null&&draggedCard.CompareTag("WeatherCard"))
        {
            if(collision.gameObject!=null&&collision.gameObject.name=="MeleeWeather"&&collision.transform.childCount==0)
            {
                Placement = GameObject.Find("MeleeWeather");
                targetZone=Placement;
                weatherCollision = true;
            }
            else if(collision.gameObject!=null&&collision.gameObject.name=="RangedWeather"&&collision.transform.childCount==0)
            {
                Placement = GameObject.Find("RangedWeather");
                targetZone=Placement;
                weatherCollision = true;
            }
            else if(collision.gameObject!=null&&collision.gameObject.name=="SiegeWeather"&&collision.transform.childCount==0)
            {
                Placement = GameObject.Find("SiegeWeather");
                targetZone=Placement;
                weatherCollision = true;
            }
        }
        if(draggedCard!=null&&draggedCard.CompareTag("ClearCard"))
        {
            if(collision.gameObject!=null&&collision.gameObject.CompareTag("WeatherCard")&&!collision.gameObject.GetComponent<DragDrop>().inHand)
            {
                cardToClear=collision.gameObject;
                Debug.Log("Card to clear found");
            }
        }

        int i;
        if(!cardStats.player)
        {
            i=1;
        }
        else
        {
            i=2;
        }
        
        if(draggedCard!=null&&draggedCard.CompareTag("EffectCard"))
        {
            if(collision.gameObject!=null&&collision.gameObject.name=="MeleeBuff"+i&&collision.transform.childCount==0)
            {
                 
                Placement = GameObject.Find("MeleeBuff"+i);
                targetZone=Placement;
                effectCollision = true;
            }
            else if(collision.gameObject!=null&&collision.gameObject.name=="RangedBuff"+i&&collision.transform.childCount==0)
            {
                Placement = GameObject.Find("RangedBuff"+i);
                targetZone=Placement;
                effectCollision = true;
            }
            else if(collision.gameObject!=null&&collision.gameObject.name=="SiegeBuff"+i&&collision.transform.childCount==0)
            {
                Placement = GameObject.Find("SiegeBuff"+i);
                targetZone=Placement;
                effectCollision = true;
            }
        }
        
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if(draggedCardStats!=null&&draggedCardStats.isDummy)
        {
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
        else if(draggedCardStats!=null&&draggedCardStats.isGiant)
        {
            if(collision.gameObject!=null&&collision.gameObject.CompareTag("SetZone1"))
            {
                Placement = collision.gameObject;
                detectedCollision=true;
                Debug.Log("SetZoneFound");
            }
            if(collision.gameObject!=null&&collision.gameObject.CompareTag("SetZone2"))
            {
                Placement = collision.gameObject;
                detectedCollision=true;
                Debug.Log("SetZoneFound");
            }
        }
        else
        {
            if(collision.gameObject == Card || collision.gameObject == Placement)
            {
                detectedCollision=true;
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        detectedCollision=false;
        dummyCollision=false;
        dummyCheck=false;
        weatherCollision=false;
        effectCollision=false;
        cardToClear=null;
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

    void HandLimitCheck()
    {
        if(Hand.transform.childCount>10)
        {
            Destroy(Hand.transform.GetChild(10).gameObject);
        }
    }

    void PassTurn()
    {
        boardManager.HandManager();

        hasPassed = turnHandler.player1Passed;
        hasPassed = turnHandler.player2Passed;
        boardManager.FieldDamage();

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
        
        if(turnHandler.player1Turn && !turnHandler.player2Passed)
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

    void SetCardToZone()
    {
        Card.transform.SetParent(Placement.transform, true);
        inHand=false;

        PassTurn();
    }

    void Update()
    {
        weatherManager.CastWeather();

        
        HandLimitCheck();
        if(isDragging&&inHand&&itsTurn&&!hasPassed)
        {
            transform.position= new Vector2(Input.mousePosition.x,Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
        if(!isDragging&&!detectedCollision&&inHand&&!CheckParent())
        {
            Card.transform.SetParent(Hand.transform, true);
            weatherManager.CastWeather();
        }

        if(!isDragging&&inHand&&cardToClear!=null)
        {
            Destroy(cardToClear);
            Destroy(gameObject);
            boardManager.FieldDamage();
            PassTurn();
        }

        if(!isDragging&&inHand&&effectCollision&&targetZone.transform.childCount==0)
        {
            SetCardToZone();
            buffManager.ApplyBuff();
            weatherManager.CastWeather();
        }
        else if(!isDragging&&inHand&&detectedCollision&&!dummyCheck&&!weatherCollision&&!effectCollision)
        {
            SetCardToZone();
        }

        if(!isDragging&&inHand&&weatherCollision&&weatherCheck&&targetZone.transform.childCount==0)
        {
            weatherCheck = false;
            SetCardToZone();
        }
        if(!isDragging&&inHand&&dummyCollision&&dummyCheck&&!targetCardStats.isGolden)
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
