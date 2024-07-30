using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public ItemSlot prefabSlot;
    public GameObject pickUpPanel;

    private List<ItemSlot> pickUpitems = new List<ItemSlot>();
    public Button pickUpButton_1;
    public Button pickUpButton_10;
    public Button pickUpButton_30;

    private void Awake()
    {
        pickUpButton_1.onClick.AddListener(() => { PickUpItem(1); });
        pickUpButton_10.onClick.AddListener(() => { PickUpItem(10); });
        pickUpButton_30.onClick.AddListener(() => { PickUpItem(30); });
    }

    public void PickUpItem(int i)
    {
        while(pickUpitems.Count > 0)
        {
            Destroy(pickUpitems[0].gameObject);
            pickUpitems.RemoveAt(0);
        }
        for(int j = 0; j < i; j++)
        {
            StringBuilder sb = new StringBuilder();
            var randomTypeNum = Random.Range(1,7);
            switch (randomTypeNum)
            {
                case 1:
                    sb.Append("Hair");
                    break;
                case 2:
                    sb.Append("Eye");
                    break;
                case 3:
                    sb.Append("Cloth");
                    break;
                case 4:
                    sb.Append("Pant");
                    break;
                case 5:
                    sb.Append("Back");
                    break;
                case 6:
                    sb.Append("Weapon");
                    break;
            }

            var randomRarityNum = Random.Range(1,7);
            switch (randomRarityNum)
            { 
                case 1:
                    sb.Append("_C");
                    break;
                case 2:
                    sb.Append("_B");
                    break;
                case 3:
                    sb.Append("_A");
                    break;
                case 4:
                    sb.Append("_S");
                    break;
                case 5:
                    sb.Append("_SS");
                    break;
                case 6:
                    sb.Append("_SSS");
                    break;
            }
            var randomColorNum = Random.Range(1, 4);
            switch (randomColorNum)
            {
                case 1:
                    sb.Append("_1");
                    break;
                case 2:
                    sb.Append("_2");
                    break;
                case 3:
                    sb.Append("_3");
                    break;
            }
            var iconimage = Resources.LoadAll<Sprite>("Equipment/"+sb.ToString());
            var equip = new Equip(iconimage, "·£´ýÀåºñ", ++GameMgr.Instance.playerMgr.playerInfo.obtainedItem);
            equip.SetEquipItem((EquipType)randomTypeNum, (RarerityType)randomRarityNum);
            InstantiateSlot(equip);
            GameMgr.Instance.uiMgr.uiInventory.InstantiateSlot(equip);
        }
    }


    public void InstantiateSlot(Equip equip)
    {
        var newSlot = Instantiate(prefabSlot, pickUpPanel.transform);
        newSlot.SetData(equip);
        newSlot.ButtonOff();
        pickUpitems.Add(newSlot);
    }
}
