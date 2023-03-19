using UnityEngine;
using UnityEngine.InputSystem;

namespace Players.PlayerFolder
{
    public class Playersight : MonoBehaviour
    {
        private GameObject LaunchOffsetPlayer;
        private Rigidbody2D RblaunchOffsetPLayer;
   
        private InputAction _sightmove;
        public Vector2 PointerPosition { get; set; }
    
        void Awake()
        {
            LaunchOffsetPlayer = GameObject.FindWithTag("PlayerLaunchOffset");
            RblaunchOffsetPLayer = LaunchOffsetPlayer.GetComponent<Rigidbody2D>();
        }
    
        void Update()
        {
        var transform1 = transform;
        transform1.right = (PointerPosition - (Vector2)transform1.position).normalized;
        }
    }
}
