using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBuffCards : BuffCards
{
    [SerializeField]
    MovementModifier movementModifier;
    public override void PickUpCard(Collider collision)
    {
        base.PickUpCard(collision);
        collision.GetComponent<Player_controller>().AddModifier(movementModifier);
    }
}
