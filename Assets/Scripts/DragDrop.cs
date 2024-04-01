using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject Hand;
    public GameObject Placement;
    public GameObject Card;

    private bool isDragging=false;
    private bool detectedCollision=false;
    private bool canMove=true;
    
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
        Debug.Log("Object entered collision");
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Object is colliding");
        if(collision.gameObject == Card || collision.gameObject == Placement)
        {
            detectedCollision=true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Object exit collision");
        detectedCollision=false;
    }

    bool CheckParent()
    {
        if(Card.transform.parent == Hand.transform)
        {
            Debug.Log("Card is a child of Hand");
            return true;
        }
        else
        {
            Debug.Log("Card is not a child of Hand");
            return false;
        }
    }

    void Update()
    {
        if(isDragging&&canMove)
        {
            transform.position= new Vector2(Input.mousePosition.x,Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
        if(!isDragging&&!detectedCollision&&canMove&&!CheckParent())
        {
            Card.transform.SetParent(Hand.transform, true);
            Debug.Log("Card returned to hand");
        }

    }
}
