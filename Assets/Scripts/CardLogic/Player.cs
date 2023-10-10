using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public List<Card> hand { get; private set; }

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
