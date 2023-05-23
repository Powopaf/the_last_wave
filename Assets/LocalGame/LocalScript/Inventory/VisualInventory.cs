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

        public void InitText(Text text) => text.text = "1";
    }
}