using UnityEngine;

namespace Item
{
    public abstract class Item : MonoBehaviour
    {
        protected int _durability { get; set; }

        protected Item() { }
    

        protected abstract void UpdateMe(int a);
    }
}
