using UnityEngine;

namespace Scenes.ATH
{
    public class CameraFollow : MonoBehaviour
    {
        private const float CamFollowSpeed = 2100f;
        public Transform _target;

    
        // Update is called once per frame

        private void Start()
        {
            
        }

        void Update()
        {
            var position = _target.position;
            Vector3 newPos = new Vector3(position.x, position.y, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, CamFollowSpeed * Time.deltaTime);
        }
    }
}
