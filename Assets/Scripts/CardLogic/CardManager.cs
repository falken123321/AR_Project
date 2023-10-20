﻿using System;
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
                    cardsShown++;
                    Debug.Log(cards.Count);
                    GameObject cardObj = Instantiate(cardPrefab, cardParent.position, Quaternion.identity, cardParent);

                    Sprite newSprite =
                        cardSprites.FirstOrDefault(s => s.name == card.type.ToString() + "_" + card.suit.ToString());
                    cardObj.GetComponent<CardDisplay>().SetCardSprite(newSprite);

                    cardObj.transform.position += new Vector3(4f * cards.IndexOf(card), 0, 0);
                    drawnCards.Add(card);

                    if (cardsShown == 3)
                    {
                        button.text = "Show Next Card";
                    }
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
                    Debug.Log("funktionen Show Next");
                    var card = deck1.DrawNextCard();
                    GameObject cardObj = Instantiate(cardPrefab, cardParent);
    
                    Sprite newSprite = cardSprites.FirstOrDefault(s => s.name == card.type.ToString() + "_" + card.suit.ToString());
                    cardObj.GetComponent<CardDisplay>().SetCardSprite(newSprite);

                    
                    cardObj.transform.position = cardParent.position + new Vector3(4f * cardsShown, 0, 0);
                    cardsShown++;
                }
            }
        }

