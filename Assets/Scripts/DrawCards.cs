using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawCards : MonoBehaviour
{
    public GameObject Art;
    public GameObject DarkArmy;
    public GameObject Hand;

    public void OnClick()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject card = Instantiate(DarkArmy, new Vector2(0, 0), Quaternion.identity);
            card.transform.SetParent(Hand.transform, false);
            card.name = $"{card.name}{i}";
        }
    }

     public void SetArtImage(Sprite image)
    {
        if (Art == null || Art.GetComponent<Image>() == null)
        {
            Debug.LogError("Art GameObject or its Image component is missing.");
            return;
        }

        Art.GetComponent<Image>().sprite = image;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
