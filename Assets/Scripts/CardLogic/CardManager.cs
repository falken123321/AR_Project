using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;

namespace CardLogic
{
    using UnityEngine;

    public class CardManager : MonoBehaviour
    {

        public GameObject cardPrefab;
        public Transform cardParent;
        public Sprite[] cardSprites;
        public Deck deck1;
        public TextMeshProUGUI button;
        public List<Card> drawnCards = new List<Card>();
        private float placement;

        private int cardsShown = 0;

        public void ShowFlop()
        {
            var cards = deck1.DrawRandomFlopCards();
            Debug.Log(cards.Count);
            Debug.Log(cardsShown);
            if (cardsShown >= 5)
            {
                Debug.Log("Alle kort er lavt");
            }
            else if (cardsShown >= 3)
            {
                button.text = "Show Final Card";
                Debug.Log("funktionen Show Next bliver kaldt fra ShowFlop");
                ShowNextCard();
            }
            else
            {
                foreach (var card in cards)
                {
                    Vector3 spawnPosition = cardParent.position + new Vector3(cardsShown*100, 0, 0);
                    GameObject cardObj = Instantiate(cardPrefab, spawnPosition, Quaternion.identity, cardParent);

                    Sprite newSprite =
                        cardSprites.FirstOrDefault(s => s.name == card.type.ToString() + "_" + card.suit.ToString());
                    cardObj.GetComponent<CardDisplay>().SetCardSprite(newSprite);

                    cardsShown++; // Inkrementer tælleren for hvert kort, du instantierer
                }
            }
        }


        public List<Card> GetDrawnCards()
        {
            Debug.Log(drawnCards);
            return drawnCards;
        }


        public void reset()
        {
            foreach (Transform child in cardParent)
            {
                Destroy(child.gameObject);
            }


            button.text = "Show Flop";
            cardsShown = 0;

        }

        public void ShowNextCard()
        {

            var card = deck1.DrawNextCard();

            Vector3 spawnPosition = cardParent.position + new Vector3(cardsShown*100, 0, 0);
            GameObject cardObj = Instantiate(cardPrefab, spawnPosition, Quaternion.identity, cardParent);

            Sprite newSprite =
                cardSprites.FirstOrDefault(s => s.name == card.type.ToString() + "_" + card.suit.ToString());
            cardObj.GetComponent<CardDisplay>().SetCardSprite(newSprite);

            cardsShown++; // Inkrementer tæl
        }
    }
}

