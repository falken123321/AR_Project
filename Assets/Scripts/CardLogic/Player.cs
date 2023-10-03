using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private List<Card> hand;

    public List<Card> SetCards(Card[] cards)
    {
        hand = new List<Card>();
        hand.AddRange(cards);
        hand.Add(cards[0]);
        hand.Add(cards[1]);
        return hand;
    }

    public void Fold()
    {
        hand.Clear();
    }

}
