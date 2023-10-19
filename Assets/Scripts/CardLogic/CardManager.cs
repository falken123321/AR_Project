using System.Linq;

namespace CardLogic
{
    using UnityEngine;

    public class CardManager : MonoBehaviour
    {
        public Deck deck;
        public GameObject cardPrefab; 
        public Transform cardParent;
        public Sprite[] cardSprites;


        public void ShowFlop()
        {
            var cards = deck.DrawRandomFlopCards();
            foreach (var card in cards)
            {
                GameObject cardObj = Instantiate(cardPrefab, cardParent);
                
                Sprite newSprite = cardSprites.FirstOrDefault(s => s.name == card.type.ToString() + "_" + card.suit.ToString());

                cardObj.GetComponent<CardDisplay>().SetCardSprite(newSprite);
            }
        }

        public void ShowNextCard()
        {
            var card = deck.DrawNextCard();
            GameObject cardObj = Instantiate(cardPrefab, cardParent);
            
            Sprite newSprite = cardSprites.FirstOrDefault(s => s.name == card.type.ToString() + "_" + card.suit.ToString());

            cardObj.GetComponent<CardDisplay>().SetCardSprite(newSprite);
        }
    }

}