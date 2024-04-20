using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CardStats : MonoBehaviour
{
    //Almacena las estadisticas de la carta como el daño, el rango, si es dorada

    public GameObject NameText;
    public GameObject DescriptionText;
    public GameObject ArtImage;
    public GameObject DamageText;
    public GameObject RangeImage;

    public bool player=false;
    public string nameString;
    public string descriptionString;
    public int originalAttackDamage;
    public int attackDamage;
    public int tempDamage;
    public int attackRange;
    public bool isGolden;
    public bool isDummy;
    public bool isGiant;


    private void OnEnable()
    {
        NameText.GetComponent<Text>().text=nameString;
        
        DescriptionText.GetComponent<Text>().text=descriptionString;

        DamageText.GetComponent<Text>().text=attackDamage.ToString();

        if(attackRange==0)
        {
            RangeImage.GetComponent<Image>().sprite=Resources.Load<Sprite>("Silver Sword");
        }
        else if(attackRange==1)
        {
            RangeImage.GetComponent<Image>().sprite=Resources.Load<Sprite>("Bow");
        }
        else if(attackRange==2)
        {
            RangeImage.GetComponent<Image>().sprite=Resources.Load<Sprite>("Wooden Staff");
        }
        else if(attackRange==4)
        {
            isDummy=true;
        }
    }
}
