using UnityEngine;

namespace CardLogic
{
    using UnityEngine;
    using UnityEngine.UI;

    public class CardDisplay : MonoBehaviour
    {
        public Image cardImage;

        public void SetCardSprite(Sprite newSprite)
        {
            cardImage.sprite = newSprite;
            Debug.Log("Card sprite set to: " + newSprite.name + " for " + gameObject.name);
        }
    }


}