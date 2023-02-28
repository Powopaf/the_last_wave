using UnityEngine;

namespace Players
{
    public class Survivor : Player
    {
        public Survivor() : base(125, 25, 30) { }


        protected override void FixedUpdate()
        {
            float horizontalMovement = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
        }

        protected override void MovePlayer(float h)
        {
            Vector3 targetvelocityH = new Vector2(h, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity,
                targetvelocityH, ref velocity, .05f);
        }
    }
}
