using UnityEngine;

public class DecoyTrait : MonsterTrait {

    public Monster Monster;
    public SpriteRenderer SpriteRenderer;
    public Animator Animator;
    public GameObject HealthBar;

    public Sprite ItemBoxSprite;

	void Start()
    {
        HealthBar.SetActive(false);
        Monster.IsDecoy = true;
        SpriteRenderer.sprite = ItemBoxSprite;
        if (Animator != null)
            Animator.enabled = false;
    }

}
