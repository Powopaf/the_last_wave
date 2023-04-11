using System.Collections.Generic;
using Pathfinding;
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
            seeker = GetComponent<Seeker>();
        }

        protected override void Start()
        {
            InvokeRepeating("UpdatePath", 0f, 0.5f);
        }
        private void UpdatePath()
        {
            if (nodes != null && endNode != null)
            {
                // Get the start node based on the current position of the zombie
                Vector3 zombiePosition = transform.position;
                int zombieX = Mathf.RoundToInt(zombiePosition.x);
                int zombieY = Mathf.RoundToInt(zombiePosition.y);
                Node startNode = nodes[zombieX, zombieY];

                // Run the pathfinding algorithm to find the shortest path
                List<Node> shortestPath = FindShortestPath(startNode, endNode);

                // Convert the list of nodes to a list of Vector2 positions
                List<Vector2> pathPositions = new List<Vector2>();
                foreach (Node node in shortestPath)
                {
                    pathPositions.Add(new Vector2(node.X, node.Y));
                }

                // Update the zombie's path and reset the current waypoint index
                path = new Path(pathPositions.ToArray());
                currentWaypoint = 0;
            }
        }

        private List<Node> FindShortestPath(Node startNode, Node endNode)
        {
            ZombiePathFinding pathFinding = new ZombiePathFinding(new Node[50, 80], GameObject.FindWithTag("Player"),
                new TileDefinition[50, 80]);
            return pathFinding.FindPath(pathFinding.nodes[0, 0], pathFinding.nodes[5, 5]);
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
        }

        protected override void ZombieMovement(Vector2 direction)
        {
            rb.MovePosition((Vector2)transform.position + direction * (speed * Time.deltaTime));
        }
        
        protected override void Update()
        { 
            if (path == null)
            {
                return;
            }

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }

            // Calculate the direction to the current waypoint
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

            // Move the zombie towards the current waypoint
            ZombieMovement(direction);

            // Check if the zombie has reached the current waypoint
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            //Vector3 direction = Playertarget.position - transform.position;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //rb.rotation = angle;
            //direction.Normalize();
            //Movement = direction;
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
