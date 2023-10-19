namespace CardLogic
{
    using UnityEngine;

    public class CardDisplay : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;

        public void SetCardSprite(Sprite newSprite)
        {
            spriteRenderer.sprite = newSprite;
        }
    }
}