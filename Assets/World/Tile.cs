using UnityEngine;

namespace World
{
    [System.Serializable]
    public class Tile
    {
        public string name;
        public GameObject visual;
        public bool iswalkable = true;
    }

    [System.Serializable]
    public class Side
    {
        public string name;
        public GameObject visual;
    }
}
