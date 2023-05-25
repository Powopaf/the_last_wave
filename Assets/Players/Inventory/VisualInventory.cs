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
        public TMP_Text textWood;
        public TMP_Text textStone;

        public void UpdateText(int lvl, ItemEnum item)
        {
            switch (item)
            {
                case ItemEnum.Helmet:
                    textHelmet.text = "lvl " + lvl;
                    break;
                case ItemEnum.ChestPlate:
                    textChestPlate.text = "lvl " + lvl;
                    break;
                case ItemEnum.Boots:
                    textBoots.text = "lvl " + lvl;
                    break;
                case ItemEnum.Gloves:
                    textGloves.text = "lvl " + lvl;
                    break;
                default:
                    textSword.text = "lvl " + lvl;
                    break;
            }
        }
    }
}