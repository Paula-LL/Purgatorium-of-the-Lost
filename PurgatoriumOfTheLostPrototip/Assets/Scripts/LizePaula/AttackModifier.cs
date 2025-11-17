using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackModifier : ScriptableObject
{
    public abstract void ApplyModifier(Attack attack);
}
