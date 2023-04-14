using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using World;

namespace Monsters
{
    public class Zombie1 : Zombie
    {
        public Zombie1() : 
            base("Zombie1", new []{"Player", "Core"}, 
                100, 20, 30) {}

        
        protected override void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            Playertarget = GameObject.FindWithTag("Player").transform;
            animator = GetComponent<Animator>();
            X =(int) transform.position.x;
            Y = (int) transform.position.y;

        }

        protected override void Start()
        {
          //  pathFinder = gameObject.GetComponent<ZombiePathFinding>();
        }
       

        protected override void ZombieMovement(Vector2 direction)
        {
           rb.MovePosition((Vector2)transform.position + direction * (speed * Time.deltaTime));
        }
        
        protected override void Update()
        {
           /*
           var position = transform.position;
           pathFinder.FindPath
           (new int2(X, Y), new int2((int)Playertarget.transform.position.x, (int)Playertarget.transform.position.y));
           Vector2 playermoved = Playertarget.transform.position;
           int i = 0;
           while ((Vector2)Playertarget.transform.position==playermoved)
           {
               if (pathFinder.NodePath.Count==0) break;
               Vector2 direction=new Vector2(pathFinder.NodePath[i].x-position.x,pathFinder.NodePath[i].y-position.y);
               direction.Normalize();
               Movement = direction;
               i += 1;

           }
           animator.SetFloat("X", Movement.x);
           animator.SetFloat("Y", Movement.y);
            X = (int) position.x;
            Y = (int)position.y;*/
        }
        protected override void FixedUpdate()
        {
           //ZombieMovement(Movement);
        }
        protected override void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.CompareTag("Player"))   //Need to change  the tag
            {
                Players.Survivor survivor = Playertarget.transform.GetComponent<Players.Survivor>(); //Zombie Attack
                survivor.ZombieDamageOnPlayer(Damage); // Zombie Attack
            }
        
        }
    
    }
}
