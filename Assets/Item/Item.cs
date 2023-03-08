using UnityEngine;

namespace Item
{
    public abstract class Item : MonoBehaviour
    {
        protected int _durability { get; set; }

        public Item() { }
    

        protected abstract void UpdateMe(int a);
    }
}
