using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    //Adminstra los distintos climas usando la funcion CastWeather

    public GameObject MeleeWeather;
    public GameObject RangedWeather;
    public GameObject SiegeWeather;
    private GameObject areaReference;
    public CardStats[][] Fields;
    public CardStats[] FieldList1;
    public CardStats[] FieldList2;
    public CardStats cardStats;
    private BoardManager boardManager;


    public void CastWeather()
    {
        boardManager = GameObject.Find("Main Canvas").GetComponent<BoardManager>();
        Fields = new CardStats[3][];

        
        Fields[1] = FieldList1;
        Fields[2] = FieldList2;

        if(MeleeWeather.transform.childCount==1)
        {   
            for(int i=1; i<=2; i++)
            {
                areaReference = GameObject.Find("MeleeZone"+i);
                Fields[i] = areaReference.GetComponentsInChildren<CardStats>();
                foreach(CardStats cardStats in Fields[i])
                {
                    if(!cardStats.isGolden)
                    {
                        cardStats.originalAttackDamage=cardStats.originalAttackDamage-1;
                    }
                }
            }
        }
        else
        {
            for(int i=1; i<=2; i++)
            {
                areaReference = GameObject.Find("MeleeZone"+i);
                Fields[i] = areaReference.GetComponentsInChildren<CardStats>();
                foreach(CardStats cardStats in Fields[i])
                {
                    if(!cardStats.isGolden)
                    {
                        cardStats.attackDamage=cardStats.originalAttackDamage;
                    }
                }
            }
        }

        if(RangedWeather.transform.childCount==1)
        {   
            for(int i=1; i<=2; i++)
            {
                areaReference = GameObject.Find("RangedZone"+i);
                Fields[i] = areaReference.GetComponentsInChildren<CardStats>();
                foreach(CardStats cardStats in Fields[i])
                {
                    if(!cardStats.isGolden)
                    {
                        cardStats.attackDamage=cardStats.originalAttackDamage-1;
                    }
                }
            }
        }
        else
        {
            for(int i=1; i<=2; i++)
            {
                areaReference = GameObject.Find("MeleeZone"+i);
                Fields[i] = areaReference.GetComponentsInChildren<CardStats>();
                foreach(CardStats cardStats in Fields[i])
                {
                    if(!cardStats.isGolden)
                    {
                        cardStats.attackDamage=cardStats.originalAttackDamage;
                    }
                }
            }
        }

        if(SiegeWeather.transform.childCount==1)
        {   
            for(int i=1; i<=2; i++)
            {
                areaReference = GameObject.Find("SiegeZone"+i);
                Fields[i] = areaReference.GetComponentsInChildren<CardStats>();
                foreach(CardStats cardStats in Fields[i])
                {
                     if(!cardStats.isGolden)
                    {
                        cardStats.attackDamage=cardStats.originalAttackDamage-1;
                    }
                }
            }
        }
        else
        {
            for(int i=1; i<=2; i++)
            {
                areaReference = GameObject.Find("MeleeZone"+i);
                Fields[i] = areaReference.GetComponentsInChildren<CardStats>();
                foreach(CardStats cardStats in Fields[i])
                {
                    if(!cardStats.isGolden)
                    {
                        cardStats.attackDamage=cardStats.originalAttackDamage;
                    }
                }
            }
        }
        boardManager.FieldDamage();
    }
}
