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
    public CardStats[][] Fields;
    public CardStats[] HandList1;
    public CardStats[] HandList2;
    private GameObject[] PlayerDamages;
    // private GameObject Player1Damage;
    // private GameObject Player2Damage;
    private GameObject areaReference;

    public int player1damage=0;
    public int player2damage=0;

    private void OnEnable()
    {
        cardStats = GetComponent<CardStats>();

        Fields = new CardStats[3][];

        PlayerDamages = new GameObject[3];

        Fields[1] = FieldList1;
        Fields[2] = FieldList2;

        PlayerDamages[1] = GameObject.Find("Player1Damage");
        PlayerDamages[2] = GameObject.Find("Player2Damage");
    }

    public void FieldDamage()
    {
        for(int i=1; i<=2; i++)
        {
            damageCounter=0;
            areaReference = GameObject.Find("SetZone"+i);
            Fields[i] = areaReference.GetComponentsInChildren<CardStats>();          
            foreach(CardStats cardStats in Fields[i])
            {
                damageCounter+=cardStats.attackDamage;
            }   
            PlayerDamages[i].GetComponent<Text>().text = "Ataque:"+damageCounter;
            if(i==1)
            {
                player1damage=damageCounter;
            }
            else
            {
                player2damage=damageCounter;
            }
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
