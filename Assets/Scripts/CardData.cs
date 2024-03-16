using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardData : MonoBehaviour
{
    public Image imageComponent;
    public Sprite spriteToAssign;


    void Start()
    {
        imageComponent.sprite = spriteToAssign;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
