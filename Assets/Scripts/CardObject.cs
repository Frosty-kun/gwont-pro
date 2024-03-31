using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class CardObject : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite artwork;
    public int range;
    public int attack;
    public int effect;
}
