using UnityEngine;
using UnityEngine.Serialization;

namespace Players.PlayerFolder
{
    public class PlayerWeaponAttack : MonoBehaviour
    {
        public Vector2 PointerPosition { get; set; }
        public SpriteRenderer characterRenderer, weaponRenderer; 
    
    
        void Update()
        {
            Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
            transform.right = direction;

            Vector2 scale = transform.localScale;
            if (direction.x < 0)
            {
                scale.y = -1;
            }
            else if (direction.x>0)
            {
                scale.y = 1;
            }
            transform.localScale = scale;

            if (transform.eulerAngles.z>0 && transform.eulerAngles.z<180)
            {
                weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
            }
            else
            {
                weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
            }
        }
    }
}
