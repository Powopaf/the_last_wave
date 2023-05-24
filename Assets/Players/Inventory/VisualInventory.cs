using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Players.Inventory
{
    public class VisualInventory : MonoBehaviour
    {
        public TMP_Text textHelmet;
        public TMP_Text textChestPlate;
        public TMP_Text textBoots;
        public TMP_Text textGloves;
        public TMP_Text textSword;
        public TMP_Text textMoney;

        public void UpdateText(Inventory inventory, ItemEnum item)
        {
            Dictionary<ItemEnum, int> index = new GetItem().GetInv;
            switch (item)
            {
                case ItemEnum.Helmet:
                    textHelmet.text = $"lvl {inventory.Inv[index[item]].Item2}";
                    break;
                case ItemEnum.ChestPlate:
                    textChestPlate.text = $"lvl {inventory.Inv[index[item]].Item2}";
                    break;
                case ItemEnum.Boots:
                    textBoots.text = $"lvl {inventory.Inv[index[item]].Item2}";
                    break;
                case ItemEnum.Gloves:
                    textGloves.text = $"lvl {inventory.Inv[index[item]].Item2}";
                    break;
                case ItemEnum.Sword:
                    textSword.text = $"lvl {inventory.Inv[index[item]].Item2}";
                    break;
            }
        }

        public void UpdateMoney(int money)
        {
            textMoney.text = money.ToString();
        }
    }
}