using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardStats : MonoBehaviour
{
    public GameObject NameText;
    public GameObject DescriptionText;
    public GameObject ArtImage;
    public GameObject DamageText;

    public int attackDamage;

    void Update()
    {
        DamageText.GetComponent<Text>().text=attackDamage.ToString();
    }
}
