using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TarotCards/SpeedUp")]

public class MovementValueBuffModifier : MovementModifier
{
    public float dashSpeedUp;
    public override void ApplyMovementModifier(Movement movement)
    {
        movement.dashSpeed += dashSpeedUp;
    }
}
