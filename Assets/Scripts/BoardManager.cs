using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    private int damageCounter = 0;

    private CardStats cardStats;
    public CardStats[] FieldList1;
    public CardStats[] FieldList2;
    public CardStats[] HandList1;
    public CardStats[] HandList2;
    private GameObject Player1Damage;
    private GameObject Player2Damage;
    GameObject areaReference;

    private void OnEnable()
    {
        cardStats = GetComponent<CardStats>();

        Player1Damage = GameObject.Find("Player1Damage");
        Player2Damage = GameObject.Find("Player2Damage");
    }

    public int FieldDamage(bool player)
    {
        damageCounter=0;
        if(!player)
        {
            areaReference = GameObject.Find("SetZone1");
            FieldList1 = areaReference.GetComponentsInChildren<CardStats>();
            foreach(CardStats cardStats in FieldList1)
            {
                damageCounter+=cardStats.attackDamage;
            }
            
            Player1Damage.GetComponent<Text>().text = "Ataque:"+damageCounter;
            return damageCounter;
        }
        else
        {
            areaReference = GameObject.Find("SetZone2");
            FieldList2 = areaReference.GetComponentsInChildren<CardStats>();
            foreach(CardStats cardStats in FieldList2)
            {
                damageCounter+=cardStats.attackDamage;
            }
            Player2Damage.GetComponent<Text>().text = "Ataque:"+damageCounter;
            return damageCounter;
        }
    }

    public void HandManager()
    {
        areaReference = GameObject.Find("Hand1");
        
        HandList1 = areaReference.GetComponentsInChildren<CardStats>();
        foreach(CardStats cardStats in HandList1)
        {
        
        }
        areaReference = GameObject.Find("Hand2");
        HandList2 = areaReference.GetComponentsInChildren<CardStats>();
        foreach(CardStats cardStats in HandList2)
        {
        
        }
    }
}
