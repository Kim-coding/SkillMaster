using UnityEngine;
using UnityEngine.UI;

public class PickUpSlot : MonoBehaviour
{
    public Button button;
    public Image rarityColor;

    public Image itemImage;
    public Equip currentEquip = null;

    private void Awake()
    {
        TryGetComponent<Button>(out button);
        if (button != null)
        {
            button.onClick.AddListener(OnbuttonClick);
        }
    }

    public void SetData(Equip equipData)
    {
        if (equipData != null)
        {
            currentEquip = equipData;
            itemImage.sprite = equipData.icon;
            Color newColor = Color.white;
            switch (equipData.rarerityType)
            {
                case RarerityType.None:
                    ColorUtility.TryParseHtmlString("#C4C4C4", out newColor);
                    break;
                case RarerityType.C:
                    ColorUtility.TryParseHtmlString("#97846B", out newColor);
                    break;
                case RarerityType.B:
                    ColorUtility.TryParseHtmlString("#6AAC8D", out newColor);
                    break;
                case RarerityType.A:
                    ColorUtility.TryParseHtmlString("#A4BDFF", out newColor);
                    break;
                case RarerityType.S:
                    ColorUtility.TryParseHtmlString("#C188D7", out newColor);
                    break;
                case RarerityType.SS:
                    ColorUtility.TryParseHtmlString("#F4C56B", out newColor);
                    break;
                case RarerityType.SSS:
                    ColorUtility.TryParseHtmlString("#C74B46", out newColor);
                    break;
            }
            rarityColor.color = newColor;
        }
    }

    public void OnbuttonClick()
    {
        GameMgr.Instance.uiMgr.uiWindow.pickUpItemPanel.gameObject.SetActive(true);
        GameMgr.Instance.uiMgr.uiWindow.pickUpItemPanel.SetItemInfoPanel(currentEquip);
    }


}
