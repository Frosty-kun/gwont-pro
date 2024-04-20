using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    //Adminsitra los aumentos mediante la funcion ApplyBuff

    public GameObject MeleeBuff;
    public GameObject RangedBuff;
    public GameObject SiegeBuff;
    private GameObject areaReference;
    public CardStats[][] Fields;
    public CardStats[] FieldList1;
    public CardStats[] FieldList2;
    public CardStats cardStats;
    private BoardManager boardManager;

    public void ApplyBuff()
    {
        boardManager = GameObject.Find("Main Canvas").GetComponent<BoardManager>();
        Fields = new CardStats[3][];

        for(int i=1; i<=2;i++)
        {
            MeleeBuff = GameObject.Find("MeleeBuff"+i);
            if(MeleeBuff.transform.childCount>0)
            {   
                areaReference = GameObject.Find("MeleeZone"+i);
                Fields[i] = areaReference.GetComponentsInChildren<CardStats>();
                foreach(CardStats cardStats in Fields[i])
                {
                    if(!cardStats.isGolden)
                    {
                        cardStats.originalAttackDamage=cardStats.originalAttackDamage+1;
                        cardStats.attackDamage=cardStats.originalAttackDamage;
                    }
                }
            }
            RangedBuff = GameObject.Find("RangedBuff"+i);
            if(RangedBuff.transform.childCount>0)
            {   
                
                areaReference = GameObject.Find("RangedZone"+i);
                Fields[i] = areaReference.GetComponentsInChildren<CardStats>();
                foreach(CardStats cardStats in Fields[i])
                {
                    if(!cardStats.isGolden)
                    {
                        cardStats.originalAttackDamage=cardStats.originalAttackDamage+1;
                        cardStats.attackDamage=cardStats.originalAttackDamage;
                    }
                }
            }
            SiegeBuff = GameObject.Find("SiegeBuff"+i);
            if(SiegeBuff.transform.childCount>0)
            {   
                areaReference = GameObject.Find("SiegeZone"+i);
                Fields[i] = areaReference.GetComponentsInChildren<CardStats>();
                foreach(CardStats cardStats in Fields[i])
                {
                    if(!cardStats.isGolden)
                    {
                        cardStats.originalAttackDamage=cardStats.originalAttackDamage+1;
                        cardStats.attackDamage=cardStats.originalAttackDamage;
                    }
                }
            }
        }
    }



}
