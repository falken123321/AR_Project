using System.Collections;
using System.Collections.Generic;
using Hands;
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
    private Deck DealerDeck; //Kort i dealerns dæk / dem man ikke kender / dem enemy spiller kan have
    private List<Card> boardCards; //Kort på bordet
    private Player player; // Dine kort
    private Plays play; // Viser om du har et play

    // Start is called before the first frame update
    void Start()
    {
        this.DealerDeck = new Deck();
        this.player = new Player();
        this.boardCards = new List<Card>(5);
        this.play = Plays.None;
    }

    void RegisterHand()
    {

    }
    
    void RegisterBoard()
    {

    }


    //STARTER PÅ HAND FUNCTIONERNE -------------------------------------------------
    //
    void CheckHand()
    {
        if (this.player == null) return;
        //Kør alle functionerne under

        CheckPair();
    }

    //Retunere list af pair, så man kan se hvilke kort er pairet
    //+ mulighed for at få en Liste af lister af cards for at få flere pairs
    void CheckPair()
    {
        var pairs = 0;
        List<List<Card>> cardPairs = new List<List<Card>>();

        //Check if hand is pair
        if (this.player.hand[0].type == this.player.hand[1].type)
        {
            pairs++;
            var pair = new List<Card>();
            pair.Add(this.player.hand[0]);
            pair.Add(this.player.hand[1]);
            cardPairs.Add(pair);
        }

        //Check hver kort på hånd om det pair med en på board
        foreach (Card card in this.player.hand) { 
            foreach(Card boardCard in this.boardCards)
            {
                if(card.type == boardCard.type)
                {
                    pairs++;
                    var pair = new List<Card>();
                    pair.Add(boardCard);
                    pair.Add(card);
                    cardPairs.Add(pair);
                }
            }
        }
    }
}
