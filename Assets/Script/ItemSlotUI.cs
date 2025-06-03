using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI amountText; 

    public void SetItem(Sprite icon, int amount)
    {
        iconImage.sprite = icon;
        amountText.text = amount.ToString();
    }
}
