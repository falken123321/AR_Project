using UnityEngine;

namespace CardLogic
{
 public class CardDisplay : MonoBehaviour
 {
     public SpriteRenderer spriteRenderer;
 
     public void SetCardSprite(Sprite newSprite)
     {

         spriteRenderer.sprite = newSprite;
         Debug.Log("Card sprite set to: " + newSprite.name + " for " + gameObject.name);
     }
 }

}