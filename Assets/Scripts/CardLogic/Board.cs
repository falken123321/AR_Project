using System;
using System.Collections;
using System.Collections.Generic;
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


    //STARTER P� HAND FUNCTIONERNE -------------------------------------------------
    //
    public void CheckHand()
    {
        if (this.player == null) return;
        //K�r alle functionerne under

        CheckPair();
        CheckThreeOfAKind();

        
    }

    //Retunere list af pair, s� man kan se hvilke kort er pairet
    //+ mulighed for at f� en Liste af lister af cards for at f� flere pairs
    void CheckPair()
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
        foreach (Card card in this.player.hand) { 
            foreach(Card boardCard in this.boardCards)
            {
                if(card.type == boardCard.type)
                {
                    pairs++;
                    List<Card> pair = new List<Card>();
                    pair.Add(boardCard);
                    pair.Add(card);
                    cardPairs.Add(pair);
                }
            }
        }
    }

    bool CheckThreeOfAKind()
    {
        int[] suitsCount = { 0, 0, 0, 0 };
        bool hasThreeOfAKind = false;

        foreach(Card card in this.combinedCard)
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

        foreach(int i in suitsCount)
        {
            if (i == 3) hasThreeOfAKind = true;
        }


        return hasThreeOfAKind;
    }

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
        } else {
            //Calculate something (:
            displayInstructions("Du er færdig. Du har {ADD PAIR OR WHATEVER}");
        } 
        

    }
}
