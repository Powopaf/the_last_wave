using UnityEngine;

namespace Players
{
    public class Survivor : Player
    {
        public Survivor() : base(125, 25, 30, 1 , "", 125) { }
        
        protected override void FixedUpdate()
        {
            dir.x = Input.GetAxis("Horizontal");
            dir.y = Input.GetAxis("Vertical");
            MovePlayer();
        }

        protected override void MovePlayer()
        {
            rb.MovePosition(rb.position + dir * (_speed * Time.deltaTime));
        }
    }
}
