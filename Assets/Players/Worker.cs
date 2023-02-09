using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    class Worker : Player
    {
        public Worker()
        {
            new Player("Worker", 175, 35, 25);
        }
    }
}