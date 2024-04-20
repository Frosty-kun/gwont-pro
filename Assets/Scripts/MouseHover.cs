using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Administra el visor de cartas

    private GameObject CardVisualizer;
    private GameObject VisualizedCard;
    private CardStats cardStats;
    
    GameObject cardCopy;

    private bool descriptionToggle=false;

    public void OnEnable()
    {
        cardStats=GetComponent<CardStats>();
        
    }
    public void OnMouseDown()
    {
        descriptionToggle=!descriptionToggle;
        //Debug.Log("Description Toggle : "+descriptionToggle);
        if(cardCopy!=null)
            {
                if(descriptionToggle)
                {
                    CardStats descriptionStats = cardCopy.GetComponent<CardStats>();
                    descriptionStats.DescriptionText.transform.SetSiblingIndex(6);

                    CardStats imageFiltering = cardCopy.GetComponent<CardStats>();
                    Image image = imageFiltering.ArtImage.GetComponent<Image>();
                    image.color = new Color(0f,0f,0f,1f);
                }
                else
                {
                    CardStats descriptionStats = cardCopy.GetComponent<CardStats>();
                    descriptionStats.DescriptionText.transform.SetSiblingIndex(0);

                    CardStats imageFiltering = cardCopy.GetComponent<CardStats>();
                    Image image = imageFiltering.ArtImage.GetComponent<Image>();
                    image.color = new Color(255f,255f,255f,1f);
                }
            }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse entered button");


        CardVisualizer = GameObject.Find("CardVisualizer");

        VisualizedCard = gameObject;

        cardCopy = Instantiate(VisualizedCard, CardVisualizer.transform);
        cardCopy.GetComponent<DragDrop>().enabled = false;
        cardCopy.name = "Visualizer Card";

        if(cardCopy!=null)
        {
                //Debug.Log("Parent set to CardVisualizer");
                cardCopy.transform.localPosition = new Vector2(0f,0f);
                cardCopy.transform.localScale = new Vector3(4f, 4f, 4f);
                cardCopy.transform.localRotation = VisualizedCard.transform.localRotation;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse exited button");
        descriptionToggle=false;
        Destroy(cardCopy, 0f);
    }
}
