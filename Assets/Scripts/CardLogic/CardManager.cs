using System.Linq;

namespace CardLogic
{
    using UnityEngine;

    public class CardManager : MonoBehaviour
    {
        
        public GameObject cardPrefab; 
        public Transform cardParent;
        public Sprite[] cardSprites;
        private Deck Deck1;

        public void ShowFlop()
        {
            if (Deck1 == null)
            {
                Debug.LogError("Deck null!");
                return;
            }

            var cards = Deck1.DrawRandomFlopCards();
    
            if (cards == null || cards.Count == 0)
            {
                Debug.LogError("0 cards were drawn!");
                return;
            }

            foreach (var card in cards)
            {
                GameObject cardObj = Instantiate(cardPrefab, cardParent);

                if (cardObj == null)
                {
                    Debug.LogError("Failed to instantiate card.");
                    continue;
                }
        
                Sprite newSprite = cardSprites.FirstOrDefault(s => s.name == card.type.ToString() + "_" + card.suit.ToString());

                if (newSprite == null)
                {
                    Debug.LogError("Failed to find sprite for card: " + card.type.ToString() + "_" + card.suit.ToString());
                    continue;
                }

                var display = cardObj.GetComponent<CardDisplay>();
                if (display == null)
                {
                    Debug.LogError("Card object does not have a CardDisplay component.");
                    continue;
                }

                display.SetCardSprite(newSprite);
            }
        }


        public void ShowNextCard()
        {
            var card = Deck1.DrawNextCard();
            GameObject cardObj = Instantiate(cardPrefab, cardParent);
            
            Sprite newSprite = cardSprites.FirstOrDefault(s => s.name == card.type.ToString() + "_" + card.suit.ToString());

            cardObj.GetComponent<CardDisplay>().SetCardSprite(newSprite);
        }
    }

}