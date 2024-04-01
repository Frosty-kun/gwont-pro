using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject CardVisualizer;
    public GameObject VisualizedCard;
    GameObject cardCopy;
    
    public bool checkMouse=false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse entered button");

        cardCopy = Instantiate(VisualizedCard, CardVisualizer.transform);
        cardCopy.GetComponent<DragDrop>().enabled = false;
        cardCopy.name = "Visualizer Card";

        if(cardCopy!=null)
        {
                Debug.Log("Parent set to CardVisualizer");
                // cardCopy.transform.localPosition = CardVisualizer.transform.localPosition;
                cardCopy.transform.localPosition = new Vector2(0f,0f);
                cardCopy.transform.localScale = new Vector3(4f, 4f, 4f);
                cardCopy.transform.localRotation = VisualizedCard.transform.localRotation;
                cardCopy.transform.localEulerAngles = new Vector3(0f,0f,270f);
                // cardCopy.transform.localRotation = CardVisualizer.transform.localRotation;
        }
        checkMouse=true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Do something here when the mouse exits the button
        Debug.Log("Mouse exited button");
        Destroy(cardCopy, 0f);      
        checkMouse=false;
    }

    void Update()
    {
        if(checkMouse)
        {
            
        }
        else
        {

        }
    }

}
