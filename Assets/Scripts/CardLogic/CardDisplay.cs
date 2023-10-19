namespace CardLogic
{
    using UnityEngine;

    using UnityEngine;
    using UnityEngine.UI;

    public class CardDisplay : MonoBehaviour
    {
        public Image image;
        public void SetCardSprite(Sprite newSprite)
        {
            image.sprite = newSprite; // Og denne linje
        }
    }


}