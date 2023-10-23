using System;
using UnityEngine;
using UnityEngine.TestTools;

namespace CardLogic
{
    using UnityEngine;
    using UnityEngine.UI;

    public class CardDisplay : MonoBehaviour
    {
        private Image cardImage;

        /*private void Start()
        {
            cardImage = GameObject.FindGameObjectWithTag("CardImage").GetComponent<Image>();
        }*/
        
        private void Start()
        {
            GameObject cardImageObject = GameObject.FindGameObjectWithTag("CardImage");
            if (cardImageObject != null)
            {
                cardImage = cardImageObject.GetComponent<Image>();
            }
            else
            {
                Debug.LogError("No GameObject with the tag 'CardImage' found.");
            }
        }


        public void SetCardSprite(Sprite newSprite)
        {
            this.gameObject.GetComponent<Image>().sprite = newSprite;
            //cardImage.sprite = null; //= //newSprite;
            Debug.Log("Card sprite set to: " + newSprite.name + " for " + gameObject.name);
        }
    }


}