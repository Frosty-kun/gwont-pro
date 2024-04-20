using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    //Amdministra el visor de daño del tablero, se encarga de eliminar las cartas al final de la ronda

    private int damageCounter = 0;

    public GameObject DeleteZone;
    public GameObject Cemetery;
    private CardStats cardStats;
    public CardStats[] FieldList1;
    public CardStats[] FieldList2;
    public CardStats[][] Fields;
    public CardStats[] HandList1;
    public CardStats[] HandList2;
    private GameObject[] PlayerDamages;
    public GameObject childCard;
    public GameObject[] childrenCards;
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
    public void KillCards()
    {
        for(int i=1; i<=3; i++)
        {
            Cemetery = GameObject.Find("Cemetery"+i);
            Debug.Log(i);

            for(int j=0; j<=2;j++)
            {
                if(j==0)
                {
                    DeleteZone = GameObject.Find("MeleeZone"+i);
                }
                else if(j==1)
                {
                    DeleteZone = GameObject.Find("RangedZone"+i);
                }
                else if(j==2)
                {
                    DeleteZone = GameObject.Find("SiegeZone"+i);
                }

                if(DeleteZone!=null)
                {
                    childrenCards = new GameObject[DeleteZone.transform.childCount];
                    for(int k=0; k<childrenCards.Length; k++)
                    {
                        Debug.Log("Warning");
                        childrenCards[k] = DeleteZone.transform.GetChild(0).gameObject;
                        DeleteZone.transform.GetChild(0).transform.SetParent(Cemetery.transform);
                    }
                }
            }
        }
    }
}
