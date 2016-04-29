using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AutoTransparency : MonoBehaviour {

    private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;
    private Color _transparentColor;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;
        _transparentColor = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0.3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && other.GetComponent<SpriteRenderer>().sortingOrder < _spriteRenderer.sortingOrder )
            _spriteRenderer.color = _transparentColor;  
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
            _spriteRenderer.color = _defaultColor;
    }
	
}
