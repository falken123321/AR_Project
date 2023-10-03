using System.Collections;
using System.Collections.Generic;
using CardEums;
using UnityEngine;

namespace CardEums
{
    public enum Suits
    {
        Hearts,
        Clubs,
        Spades,
        Diamonds
    };

    public enum Type
    {
        A = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
    }
};

public class Card
{
    public Suits suit { get; private set; }
    public Type type { get; private set; }

    public Card(Type type, Suits suit)
    {
        this.type = type;
        this.suit = suit;
    }
    
    public string toString()
    {
        return "[Type: " + type + ", Suit: " + suit + "]";
    }
}
