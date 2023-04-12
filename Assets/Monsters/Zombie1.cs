using System.Collections.Generic;
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
            
        }

        protected override void Start()
        {
            ZombiePathFinding pathFinder = GetComponent<ZombiePathFinding>();
            Node[,] nodes = pathFinder.nodes;
            GameObject player = pathFinder.Target;
            TileDefinition[,] map = pathFinder.staticmap;
            Node startNode = nodes[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)];
            Node endNode = nodes[Mathf.RoundToInt(Playertarget.transform.position.x), Mathf.RoundToInt(Playertarget.transform.position.y)];
            path = pathFinder.FindPath(startNode, endNode);
        }
       

        protected override void ZombieMovement(Vector2 direction)
        {
            rb.MovePosition((Vector2)transform.position + direction * (speed * Time.deltaTime));
        }
        
        protected override void Update()
        {
            if (path == null || path.Count == 0)
            {
                return;
            }

            Vector3 targetPosition = new Vector3(path[currentWaypointIndex].X, path[currentWaypointIndex].Y, 0);
            if (Vector3.Distance(transform.position, targetPosition) < minDistance)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= path.Count)
                {
                    return;
                }
                targetPosition = new Vector3(path[currentWaypointIndex].X, path[currentWaypointIndex].Y, 0);
            }

           // transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
           Vector3 direction = targetPosition.normalized - transform.position;
           // direction.Normalize();
           // Movement = direction;
            animator.SetFloat("X", Movement.x);
            animator.SetFloat("Y", Movement.y);
        }
        protected override void FixedUpdate()
        {
           ZombieMovement(Movement);
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
