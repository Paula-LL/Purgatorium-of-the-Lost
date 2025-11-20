using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TarotCards/AttackUp")]

public class AttackValueBuffModifier : AttackModifier
{
    public float attackUp1;
    public override void ApplyAttackModifier(Attack attack)
    {
        attack.attackDamage += attackUp1;
    }


}
