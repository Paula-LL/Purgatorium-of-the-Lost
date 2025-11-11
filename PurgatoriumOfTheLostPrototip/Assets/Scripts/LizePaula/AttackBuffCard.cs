using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="TarotCardsBuffs/AttackBuff")]

public class AttackBuffCard : TarotCardsObject
{
    public float attackBuffedAmount; 

    public override void Apply(GameObject target)
    {
         //target.GetComponent</*Placeholder Attack Script*/>().attack.value += attackBuffedAmount                                                     
    }
}
