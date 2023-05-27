using UnityEngine;

namespace Players
{
    public class Wall : MonoBehaviour
    {
        private int _heath = 50;

        public bool DamageWall(int damage)
        {
            _heath -= damage;
            return _heath <= 0;
        }
    }
}