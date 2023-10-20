using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CardEums;
using Hands;
using TMPro;
using UnityEngine;

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

    public TextMeshProUGUI textOBJ;
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
        displayStatus("Status: Game start");

    }

    public void Reset()
    {
        displayStatus("Status: Game reset");
    }

    void RegisterHand()
    {

    }

    void RegisterBoard()
    {

    }


    //STARTER PÅ HAND FUNCTIONERNE -------------------------------------------------
    //
    public void CheckHand()
    {
        if (this.player == null) return;
        //Kør alle functionerne under
        //Kunne faktisk gøre noget her på et tidspunkt med at vægten af dem

        CheckPair();
        CheckThreeOfAKind();
        CheckFullHouse();
        CheckFlush();
        CheckStraight();
        CheckStraightFlush();
        CheckRoyalFlush();
    }

    //Retunere list af pair, så man kan se hvilke kort er pairet
    //+ mulighed for at få en Liste af lister af cards for at få flere pairs
    bool CheckPair()
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

    bool CheckThreeOfAKind()
    {
        int[] suitsCount = { 0, 0, 0, 0 };
        bool hasThreeOfAKind = false;

        foreach (Card card in this.combinedCard)
        {
            switch (card.suit)
            {
                case Suits.Clubs:
                    suitsCount[0]++; break;
                case Suits.Diamonds:
                    suitsCount[1]++; break;
                case Suits.Hearts:
                    suitsCount[2]++; break;
                case Suits.Spades:
                    suitsCount[3]++; break;
            }
        }

        foreach (int i in suitsCount)
        {
            if (i == 3) hasThreeOfAKind = true;
        }


        return hasThreeOfAKind;
    }

    bool CheckStraight()
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

    bool CheckFullHouse()
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

    bool CheckFlush()
    {
        //Lidt usikker på den her, men tror det virker??
        //Tror måske det fucker, hvis de ikke er i række? - bare tilføj en suit sorteret række if not
        Suits flushSuit = Suits.Hearts;

        foreach (Card card in this.combinedCard)
        {
            if (flushSuit == Suits.Hearts) 
            {
                flushSuit = card.suit;
            }
            else if (flushSuit != card.suit) 
            {
                return false;
            }
        }

        return true;
    }

    bool CheckStraightFlush()
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

    bool CheckRoyalFlush()
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

    public void displayStatus(string text)
    {
        //set Text mesh pro text
        textOBJ.text = text;
    }

    public void displayInstructions(string text)
    {
        //set Text mesh pro text
        instructions.text = text;
    }

    public void update()
    {
        if (!player.isHandFull())
        {
            switch (player.hand.Count)
            {
                case 0:
                    displayInstructions("Vis dit første kort!");
                    break;
                case 1:
                    displayInstructions("Vis dit andet kort!");
                    break;
            }
        }
        else
        {
            //Calculate something (:
            displayInstructions("Du er færdig. Du har {ADD PAIR OR WHATEVER}");
        }


    }
}
