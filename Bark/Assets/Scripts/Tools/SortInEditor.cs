using UnityEngine;

[ExecuteInEditMode]
public class SortInEditor : MonoBehaviour {

    private SpriteRenderer _renderer;

    void Awake()
    {
        if (FindObjectOfType<GameController>() != null)
            enabled = false;
    }

	void OnGUI()
    {
        if (_renderer == null)
            _renderer = GetComponent<SpriteRenderer>();

        _renderer.sortingOrder = 100 - Mathf.FloorToInt(transform.position.y * 4);

    }

}
