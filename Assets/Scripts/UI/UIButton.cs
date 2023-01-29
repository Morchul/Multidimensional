using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class UIButton : Button
{
    private bool disable = false;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if (disable) return;
        UISoundManager.Instance.PlayButtonHoverSound();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (disable) return;
        UISoundManager.Instance.PlayButtonClickSound();
    }

    public void Disable()
    {
        TextMeshProUGUI textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.color = Color.black;
        disable = true;
    }
}
