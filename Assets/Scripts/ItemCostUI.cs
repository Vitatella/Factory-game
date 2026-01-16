using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCostUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private Image _image;

    public void SetSprite(Sprite sprite)
    {
        _image.sprite = sprite;
    }
   
    public void SetText(string text)
    {
        _textMeshPro.text = text;
    }

}
