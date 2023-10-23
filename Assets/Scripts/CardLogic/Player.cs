using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player
{
    public List<Card> hand { get; private set; }
    

    public Player()
    {
        this.hand = new List<Card>();
    }

    public void emptyHand()
    {
        this.hand = new List<Card>();
    }
    public List<Card> SetCards(Card[] cards)
    {
        hand.AddRange(cards);
        hand.Add(cards[0]);
        hand.Add(cards[1]);
        return hand;
    }

    public List<Card> RegisterCard(Card card)
    {
        if (hand.Count < 2)
        {
            hand.Add(card);
        }
        return hand;
    }

    public void Fold()
    {
        hand.Clear();
    }

    public bool isHandFull()
    {
        return hand.Count >= 2;
    }

}
