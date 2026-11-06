using UnityEngine;

public class ReelController : MonoBehaviour
{    
    private SpriteRenderer symbolSpriteRenderer;

    private void Awake()
    {
        symbolSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void DisplaySymbol(Sprite symbolSprite)
    {
        symbolSpriteRenderer.sprite = symbolSprite;
    }
}
