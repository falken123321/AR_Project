using System.Linq;

namespace CardLogic
{
    using UnityEngine;

    public class CardManager : MonoBehaviour
    {
        
        public GameObject cardPrefab; 
        public Transform cardParent;
        public Sprite[] cardSprites;
        public Deck deck1;

        public void ShowFlop()
        {
            var cards = deck1.DrawRandomFlopCards();
    
            foreach (var card in cards)
            {
                GameObject cardObj = Instantiate(cardPrefab, cardParent.position, Quaternion.identity, cardParent);
        
                Sprite newSprite = cardSprites.FirstOrDefault(s => s.name == card.type.ToString() + "_" + card.suit.ToString());
                cardObj.GetComponent<CardDisplay>().SetCardSprite(newSprite);
                
                cardObj.transform.position += new Vector3(4f * cards.IndexOf(card), 0, 0); 
            }
        }
        


        public void ShowNextCard()
        {
            var card = deck1.DrawNextCard();
            GameObject cardObj = Instantiate(cardPrefab, cardParent);
            
            Sprite newSprite = cardSprites.FirstOrDefault(s => s.name == card.type.ToString() + "_" + card.suit.ToString());

            cardObj.GetComponent<CardDisplay>().SetCardSprite(newSprite);
        }
    }

}