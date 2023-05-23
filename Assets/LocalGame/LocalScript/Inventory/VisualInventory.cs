using UnityEngine;
using UnityEngine.UI;

namespace LocalGame.LocalScript
{
    public class VisualInventory : MonoBehaviour
    {
        public Text textHelmet;
        public Text textChestPlate;
        public Text textBoots;
        public Text textGloves;
        public Text textSword;
        public Text textMoney;

        public void InitText()
        {
            textBoots.text = "1";
            textGloves.text = "1";
            textHelmet.text = "1";
            textMoney.text = "1";
            textSword.text = "1";
            textChestPlate.text = "1";
        }

        public void SetText(int value, Text text) => text.text = value.ToString();
    }
}