using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Health Health;
    public RawImage HealthBarImg;
    public Image HealthBarBackground;

    private float _height;
    private float _maxWidth;
    private float _xLoc;
    private float _yLoc;

    void Start()
    {
        _maxWidth = HealthBarImg.rectTransform.sizeDelta.x;
        _height = HealthBarImg.rectTransform.sizeDelta.y;
        _xLoc = HealthBarImg.rectTransform.anchoredPosition.x;
        _yLoc = HealthBarImg.rectTransform.anchoredPosition.y;
    }

    void Update()
    {
        HealthBarImg.rectTransform.sizeDelta = new Vector2(_maxWidth * Health.HP / Health.MaxHealth, _height);
        HealthBarImg.rectTransform.anchoredPosition = new Vector2(_xLoc + HealthBarImg.rectTransform.sizeDelta.x / 2 - _maxWidth / 2, _yLoc);
    }
}
