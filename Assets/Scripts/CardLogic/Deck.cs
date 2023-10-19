using System;
using System.Collections;
using System.Collections.Generic;
using CardEums;
using UnityEngine;

public class Deck
{
    private int drawnCount = 0; 

    public List<Card> cards { get; private set; }

    public void PopulateDeck()
    {
        this.cards = new List<Card>(52);

        //Populate deck - relevant n�r vi skal udregne procenter
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
   

    public List<Card> DrawRandomFlopCards()
    {
        List<Card> drawnCards = new List<Card>();
        System.Random random = new System.Random();

        if (drawnCount < 3) 
        {
            for (int i = 0; i < 3; i++)
            {
                if (cards.Count == 0) break;

                int randomIndex = random.Next(cards.Count);
                drawnCards.Add(cards[randomIndex]);
                cards.RemoveAt(randomIndex);
                drawnCount++; 
            }
        }

        return drawnCards;
    }

    public Card DrawNextCard()
    {
        if (drawnCount >= 5 || cards.Count == 0) return null; 

        System.Random random = new System.Random();
        int randomIndex = random.Next(cards.Count);
        Card drawnCard = cards[randomIndex];
        cards.RemoveAt(randomIndex);
        drawnCount++; 

        return drawnCard;
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
