using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCards : MonoBehaviour
{
    public TarotCardsObject cardsBuff;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player") { 
        Destroy(gameObject);
        cardsBuff.Apply(collision.gameObject);
        }
    }
}
