using System;
using System.Collections;
using System.Collections.Generic;
using CardEums;
using UnityEngine;

public class Deck
{

    public List<Card> cards { get; private set; }

    public void PopulateDeck()
    {
        this.cards = new List<Card>(52);

        //Populate deck - relevant når vi skal udregne procenter
        //Ingen grund til at shuffle?
        foreach (Suits suit in Enum.GetValues(typeof(Suits)))
        {
            foreach (CardType type in Enum.GetValues(typeof(CardType)))
            {
                cards.Add(new Card(type, suit));
            }
        }
    }

    public Deck()
    {
        PopulateDeck();
    }

    public void ResetDeck()
    {
        PopulateDeck();
    }

    public bool PopCard(Card card)
    {
        var index = cards.IndexOf(card);
        if(index >= 0)
        {
            cards.Remove(card);
            return true;
        } else { return false; }
    }

}
