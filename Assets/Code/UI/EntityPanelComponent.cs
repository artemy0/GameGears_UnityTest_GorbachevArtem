using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityPanelComponent : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI title;

    public void Initialize(Sprite sprite, string title)
    {
        Initialize(sprite);
        this.title.text = title;
    }
    public void Initialize(Sprite sprite, float value)
    {
        Initialize(sprite);
        title.text = value.ToString();
    }

    private void Initialize(Sprite sprite)
    {
        image.sprite = sprite;
    }
}