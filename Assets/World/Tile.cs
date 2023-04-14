using UnityEngine;
using System;

namespace World
{
    [Serializable]
    public class Tile
    {
        public string name;
        public GameObject visual;
        public bool iswalkable = true;
    }

    [Serializable]
    public class Side
    {
        public string name;
        public GameObject visual;
    }

    [Serializable]
    public class Props
    {
        public string name;
        public GameObject visual;
    }
}
