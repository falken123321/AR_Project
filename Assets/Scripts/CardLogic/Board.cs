using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using CardEums;
using Hands;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Hands
{
    enum Plays //Sorteret efter power af hand
    {
        RoyalFlush,
        StraightFlus,
        FourOfAKind,
        FullHouse,
        Flush,
        Straight,
        ThreeOfAKind,
        TwoPair,
        Pair,
        HighCard,
        None,
    }
}

public class Board : MonoBehaviour
{
    private Deck DealerDeck; //Kort i dealerns d�k / dem man ikke kender / dem enemy spiller kan have
    private List<Card> boardCards; //Kort p� bordet
    public Player player; // Dine kort
    private Plays play; // Viser om du har et play
    
    public TextMeshProUGUI instructions;

    private List<Card> combinedCard;

    // Start is called before the first frame update
    public void Start()
    {
        this.DealerDeck = new Deck();
        this.player = new Player();
        this.boardCards = new List<Card>(5);
        this.combinedCard = new List<Card>(7);
        this.play = Plays.None;
        updateInstructions();
        DrawFlop();
    }

    public List<Card> DrawFlop()
    {
        if(player.isHandFull()){
        //ENSURE THAT PLAYER CARDS ARE POPPED FROM DECK FIRST
        DealerDeck.PopCard(player.hand[0]);
        DealerDeck.PopCard(player.hand[1]);
        }
        List<Card> cards = DealerDeck.DrawRandomFlopCards();
       // Debug.Log(cards);
        cards.ForEach(card =>
        {
            boardCards.Add(card);
        });
        
        return cards;
    }

    public List<Card> getBoardCards()
    {
        return boardCards;
    }

    public Card drawNextDealerCard() //Vi kunne også bare retunere listen tbf
    {
        Card card = DealerDeck.DrawNextCard();
        boardCards.Add(card);
        return card;
    }

    public void Reset()
    {
        this.combinedCard = new List<Card>();
        this.player.emptyHand();
        this.boardCards = new List<Card>();
        updateInstructions();
    }

    void RegisterHand()
    {

    }

    void RegisterBoard()
    {

    }

    private void populateCombinedCards()
    {
        foreach (var card in player.hand)
        {
            this.combinedCard.Add(card);
        }
        foreach (var card in boardCards)
        {
            this.combinedCard.Add(card);
        }
    }
    //STARTER PÅ HAND FUNCTIONERNE -------------------------------------------------
    //
    public String CheckHand() {
        if (this.player == null) return "";
        //Kør alle functionerne under
        //Kunne faktisk gøre noget her på et tidspunkt med at vægten af dem


        populateCombinedCards();

        // Check if any of the conditions are true and return the corresponding message
        if (checkRoyalFlush()) return "Du har et Royal Flush!";
        if (checkStraightFlush()) return "Du har et Straight Flush!";
        if (checkStraight()) return "Du har et Straight!";
        if (checkFlush()) return "Du har et Flush!";
        if (checkFullHouse()) return "Du har fuld hus!";
        if (checkThreeOfAKind()) return "Du har tre af en slags!";
        if (checkPair()) return "Du har et par!";

        
        
        

        // If none of the conditions are met, return a default message
        return "Du har dsv ingenting.";
    }

    //Retunere list af pair, så man kan se hvilke kort er pairet
    //+ mulighed for at få en Liste af lister af cards for at få flere pairs
    bool checkPair()
    {
        var pairs = 0;
        List<List<Card>> cardPairs = new List<List<Card>>();

        //Check if hand is pair
        if (this.player.hand[0].type == this.player.hand[1].type)
        {
            pairs++;
            List<Card> pair = new List<Card>();
            pair.Add(this.player.hand[0]);
            pair.Add(this.player.hand[1]);
            cardPairs.Add(pair);
        }

        //Check hver kort p� h�nd om det pair med en p� board
        foreach (Card card in this.player.hand)
        {
            foreach (Card boardCard in this.boardCards)
            {
                if (card.type == boardCard.type)
                {
                    pairs++;
                    List<Card> pair = new List<Card>();
                    pair.Add(boardCard);
                    pair.Add(card);
                    cardPairs.Add(pair);
                }
            }
        }

        if (pairs > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool checkThreeOfAKind()
    {
        // Dictionary = Map
        Dictionary<CardType, int> typeCounts = new Dictionary<CardType, int>();

        // Sætter alle values til 0
        foreach (CardType key in Enum.GetValues(typeof(CardType)))
        {
            typeCounts[key] = 0;
        }
        
        foreach (var card in combinedCard)
        {
            // Tæller hver slags
            typeCounts[card.type]++;
        }
        
        foreach (var count in typeCounts.Values)
        {
            //Hvis en af typerne har mere end 3, er der "Tre af en slags"
            if (count >= 3) {
                return true;
            }
        }

        return false;
    }


    bool checkStraight()
    {
        //Bare tæl op på en sorteret liste

        List<Card> sorted = this.combinedCard.OrderBy(card => (int)card.type).ToList();

        int Amount = 1;
        CardType previousCardType = sorted[0].type;

        for (int i = 1; i < sorted.Count; i++)
        {
            CardType currentCardType = sorted[i].type;

            if ((int)currentCardType == (int)previousCardType + 1)
            {
                Amount++;
            }
            else
            {
                Amount = 1; 
            }

            if (Amount >= 5)
            {
                return true;
            }

            previousCardType = currentCardType;
        }

        return false; 
    }

    bool checkFullHouse()
    {
        int[] typeCounts = new int[14]; 

        //Incrementer kortet i arrayet hvis der er flere af dem
        foreach (Card card in this.combinedCard)
        {
            typeCounts[(int)card.type]++;
        }

        bool hasThreeOfAKind = false;
        bool hasPair = false;

        //Check for om der er 3ofakind
        for (int i = 1; i < typeCounts.Length; i++)
        {
            if (typeCounts[i] == 3)
            {
                hasThreeOfAKind = true;
                break; //stop
            }
        }

        //Check for om der er par
        for (int i = 1; i < typeCounts.Length; i++)
        {
            if (typeCounts[i] == 2)
            {
                hasPair = true;
                break;
            }
        }

        //Kombi = fullhus
        return hasThreeOfAKind && hasPair;
    }

    bool checkFlush()
    {
        int diamond = 0;
        int spades = 0;
        int hearts = 0;
        int clubs = 0;
        
        foreach (var card in combinedCard)
        {
            switch (card.suit)
            {
                case Suits.Diamonds:
                    diamond++;
                    break;
                case Suits.Spades:
                    spades++;
                    break;
                case Suits.Hearts:
                    hearts++;
                    break;
                case Suits.Clubs:
                    clubs++;
                    break;
            }
        }
        return diamond == 5 || spades == 5 || hearts == 5 || clubs == 5;
    }

    bool checkStraightFlush()
    {
        Suits flushSuit = Suits.Hearts;
        int amount = 1;
        CardType previous = this.combinedCard[0].type;
        foreach (Card card in this.combinedCard)
        {
            //Tjek suit (flush)
            if (flushSuit == Suits.Hearts) 
            {
                flushSuit = card.suit;
            }
            else if (flushSuit != card.suit) 
            {
                return false;
            }
            //Tjek type (straight)
            if ((int)card.type == (int)previous + 1)
            {
                amount++;
            }
            else if (card.type != previous) 
            {
                amount = 1;
            }
            if (amount >= 5) //5 af samme suit i straihgt
            {
                return true;
            }
            previous = card.type;
        }
        return false; 
    }

    bool checkRoyalFlush()
    {
        Suits flushSuit = Suits.Hearts; 
        int amount = 0; 

        foreach (Card card in this.combinedCard)
        {
            //flush delen
            if (flushSuit == Suits.Hearts) 
            {
                flushSuit = card.suit;
            }
            else if (flushSuit != card.suit) 
            {
                return false;
            }

            // Tjek om topdawgs i kort listen 
            switch (card.type)
            {
                case CardType.Ten:
                case CardType.Jack:
                case CardType.Queen:
                case CardType.King:
                case CardType.A:
                    amount++;
                    break;
                default:
                    amount = 0;
                    break;
            }

            if (amount >= 5)
            {
                return true;
            }
        }

        return false; 
    }


    //END HAND FUNCTIONERNE -------------------------------------------------

    public void displayInstructions(string text)
    {
        //set Text mesh pro text
        instructions.text = text;
    }


    public void updateInstructions()
    {
        switch (player.hand.Count)
        {
            case 0:
                displayInstructions("Vis dit første kort!");
                break;
            case 1: 
                displayInstructions("Vis dit andet kort!");
                break;
            default:
                if (this.boardCards.Count < 5) {
                    displayInstructions("Vend alle dealerens kort");    
                } else {
                    displayInstructions("Du er færdig. " + CheckHand());
                }
                break;
        }


    }
}
