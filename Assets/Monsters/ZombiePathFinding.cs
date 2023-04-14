


using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using World;

namespace Monsters
{


   /* public class Node
    {
        public int X;
        public int Y;
        public bool IsWalkable;
        public List<Node> neighbors;
        public float gScore;
        public float fScore;
        public Node parent;

        public Node(int x, int y, bool isWalkable)
        {
            X = x;
            Y = y;
            IsWalkable = isWalkable;
            neighbors = new List<Node>();
            gScore = float.MaxValue;
            fScore = float.MaxValue;
            parent = null;
        }
    }

    public class ZombiePathFinding : MonoBehaviour
    {
        public Node[,] nodes;
        public Node endNode;
        public GameObject Target;
        public TileDefinition[,] staticmap;




        private void Awake()
        {
            Target = GameObject.FindWithTag("Player");

        }

        void Start()
        {
            staticmap = GameObject.FindWithTag("MapGenerator").GetComponent<World.Map>()._mapDefinition.Map;
            nodes = InitializeMap(staticmap);
            // Set the initial endNode to the target's current position
            Vector3 targetPosition = Target.transform.position;
            UpdateTargetPosition(targetPosition);
            

        }

        public Node[,] InitializeMap(TileDefinition[,] map)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            Node[,] initializeMap = new Node[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bool isWalkable = staticmap[x, y].IsWall; //We need to acces to tile iswalkable
                    initializeMap[x, y] = new Node(x, y, isWalkable);
                }
            }

            // Connect neighboring nodes
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (initializeMap[x, y].IsWalkable)
                    {
                        if (x > 0 && initializeMap[x - 1, y].IsWalkable)
                        {
                            initializeMap[x, y].neighbors.Add(initializeMap[x - 1, y]);
                        }

                        if (x < width - 1 && initializeMap[x + 1, y].IsWalkable)
                        {
                            initializeMap[x, y].neighbors.Add(initializeMap[x + 1, y]);
                        }

                        if (y > 0 && initializeMap[x, y - 1].IsWalkable)
                        {
                            initializeMap[x, y].neighbors.Add(initializeMap[x, y - 1]);
                        }

                        if (y < height - 1 && initializeMap[x, y + 1].IsWalkable)
                        {
                            initializeMap[x, y].neighbors.Add(initializeMap[x, y + 1]);
                        }

                        if (y < height - 1 && x < width - 1 && initializeMap[x + 1, y + 1].IsWalkable)
                        {
                            initializeMap[x, y].neighbors.Add(initializeMap[x + 1, y + 1]);
                        }

                        if (y > 0 - 1 && x > 0 && initializeMap[x - 1, y - 1].IsWalkable)
                        {
                            initializeMap[x, y].neighbors.Add(initializeMap[x - 1, y - 1]);
                        }

                        if (y < height - 1 && x > 0 && initializeMap[x - 1, y + 1].IsWalkable)
                        {
                            initializeMap[x, y].neighbors.Add(initializeMap[x - 1, y + 1]);
                        }

                        if (y > 0 && x < width - 1 && initializeMap[x + 1, y - 1].IsWalkable)
                        {
                            initializeMap[x, y].neighbors.Add(initializeMap[x + 1, y - 1]);
                        }
                    }
                }
            }

            return initializeMap;
        }

        public float HeuristicCost(Node a, Node b)
        {
            float dx = Mathf.Abs(a.X - b.X);
            float dy = Mathf.Abs(a.Y - b.Y);
            return Mathf.Sqrt(dx * dx + dy * dy); // Euclidean distance
        }

        public List<Node> FindPath(Node startNode, Node endNodeParameter)
        {
            List<Node> openList = new List<Node>();
            HashSet<Node> closedList = new HashSet<Node>();

            startNode.gScore = 0;
            startNode.fScore = HeuristicCost(startNode, endNodeParameter);
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                Node currentNode = openList[0];
                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].fScore < currentNode.fScore)
                    {
                        currentNode = openList[i];
                    }
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode == endNodeParameter)
                {
                    return ConstructPath(endNodeParameter);
                }

                foreach (Node neighbor in currentNode.neighbors)
                {
                    if (closedList.Contains(neighbor))
                    {
                        continue;
                    }

                    float tentativeGScore = currentNode.gScore + HeuristicCost(currentNode, neighbor);
                    if (!openList.Contains(neighbor) || tentativeGScore < neighbor.gScore)
                    {
                        neighbor.parent = currentNode;
                        neighbor.gScore = tentativeGScore;
                        neighbor.fScore = neighbor.gScore + HeuristicCost(neighbor, endNodeParameter);

                        if (!openList.Contains(neighbor))
                        {
                            openList.Add(neighbor);
                        }
                    }
                }
            }

            return null; // path not found
        }

        private List<Node> ConstructPath(Node end)
        {
            List<Node> path = new List<Node>();
            Node currentNode = end;
            while (currentNode != null)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.Reverse();
            return path;
        }

        public void UpdateTargetPosition(Vector3 newPosition)
        {
            // Convert the target position to a Node object
            int targetX = Mathf.RoundToInt(newPosition.x);
            int targetY = Mathf.RoundToInt(newPosition.y);
            Node newEndNode = nodes[targetX, targetY];

            // Update the endNode variable
            endNode = newEndNode;
        }

    }*/
  /* public class ZombiePathFinding:MonoBehaviour
   {
       private const int MOVE_STRAIGHT_COST = 10;
       private const int MOVE_DIAGONAL_COST = 14;
       private int2 gridsize;
       private MapDefinition map;
       public List<int2> NodePath;



       private void Start()
       { 
           map = GameObject.FindWithTag("MapGenerator").GetComponent<Map>()._mapDefinition;
           gridsize = new int2(map.Width, map.Height);
           NodePath = new List<int2>();
       }

       public void FindPath(int2 startPosition, int2 endPosition)
       {

           NativeArray<PathNode> pathNodeArray = new NativeArray<PathNode>(gridsize.x * gridsize.y, Allocator.Temp);

           for (int i = 0; i < gridsize.x; i++)
           {
               for (int j = 0; j < gridsize.y; j++)
               {
                   PathNode pathNode = new PathNode();
                   pathNode.x = i;
                   pathNode.y = j;
                   pathNode.index = CalculateIndex(i, j, gridsize.x);

                   pathNode.gcost = int.MaxValue;
                   pathNode.hcost = CalculateDistanceCost(new int2(i, j), endPosition);
                   pathNode.CalculateFCost();

                   pathNode.isWalkable = true;//we need to change
                   pathNode.cameFromNodeIndex = -1;

                   pathNodeArray[pathNode.index] = pathNode;
               }
           }

           NativeArray<int2> neighbourOffsetArray = new NativeArray<int2>(new int2[]
           {
               new int2(-1, 0), //left
               new int2(1,0), //right
               new int2(0,1), // up
               new int2(0,-1), //down
               new int2(-1,-1), //left down
               new int2(-1,1), //left up
               new int2(1,-1), // right down
               new int2(1,1), //right up
           }, Allocator.Temp);
           int endNodeIndex = CalculateIndex(endPosition.x, endPosition.y,gridsize.x);
           
           PathNode startNode = pathNodeArray[CalculateIndex(startPosition.x, startPosition.y, gridsize.x)];
           startNode.gcost = 0;
           startNode.CalculateFCost();
           pathNodeArray[startNode.index] = startNode;

           NativeList<int> openlist = new NativeList<int>(Allocator.Temp);
           NativeList<int> closedlist = new NativeList<int>(Allocator.Temp);
           
           openlist.Add(startNode.index);

           while (openlist.Length>0)
           {
               int currentNodeIndex = GetLowestCostNodeIndex(openlist, pathNodeArray);
               PathNode currentNode = pathNodeArray[currentNodeIndex];

               if (currentNodeIndex == endNodeIndex)
               {
                   //reached our destination
                   break;
               }
               
               //remove current node from OpenList
               for (int i = 0; i < openlist.Length; i++)
               {
                   if (openlist[i] == currentNodeIndex)
                   {
                       openlist.RemoveAtSwapBack(i);
                       break;
                   }
               }
               closedlist.Add(currentNodeIndex);

               for (int i = 0; i < neighbourOffsetArray.Length; i++)
               {
                   int2 neighbourOffset = neighbourOffsetArray[i];
                   int2 neighbourPosition=new int2(currentNode.x+neighbourOffset.x,currentNode.y + neighbourOffset.y);

                   if (!IsPositionInsideGrid(neighbourPosition,gridsize))
                   {
                       continue;
                   }

                   int neighbourNodeIndex = CalculateIndex(neighbourPosition.x, neighbourPosition.y, gridsize.x);

                   if (closedlist.Contains(neighbourNodeIndex))
                   {
                       continue;
                   }

                   PathNode neighbourNode = pathNodeArray[neighbourNodeIndex];
                   if (!neighbourNode.isWalkable)
                   {
                       continue;
                   }

                   int2 currentNodePosition = new int2(currentNode.x, currentNode.y);

                   int tentativeCost =
                       currentNode.gcost + CalculateDistanceCost(currentNodePosition, neighbourPosition);
                   if (tentativeCost<neighbourNode.gcost)
                   {
                       neighbourNode.cameFromNodeIndex = currentNodeIndex;
                       neighbourNode.gcost = tentativeCost;
                       neighbourNode.CalculateFCost();
                       pathNodeArray[neighbourNodeIndex] = neighbourNode;

                       if (!openlist.Contains(neighbourNode.index))
                       {
                           openlist.Add(neighbourNode.index);
                       }
                   }
               }
               
           }

           PathNode endNode = pathNodeArray[endNodeIndex];
           if (endNode.cameFromNodeIndex == -1)
           {
           }
           else
           {
               //j'essaye des trucs 
               NativeList<int2> path =CalculatePath(pathNodeArray, endNode);
               for (int i = 0; i < path.Length; i++)
               {
                   NodePath.Add(path[i]);
               }
               path.Dispose();
           }
           openlist.Dispose();
           closedlist.Dispose();
           pathNodeArray.Dispose();
           pathNodeArray.Dispose();
       }

       private NativeList<int2> CalculatePath(NativeArray<PathNode> pathNodeArray, PathNode endNode)
       {
           if (endNode.cameFromNodeIndex==-1)
           {
               return new NativeList<int2>(Allocator.Temp);
           }
           else
           {
               NativeList<int2> path = new NativeList<int2>(Allocator.Temp);
               path.Add(new int2(endNode.x,endNode.y));

               PathNode currentNode = endNode;
               while (currentNode.cameFromNodeIndex != -1)
               {
                   PathNode cameFromNode = pathNodeArray[currentNode.cameFromNodeIndex];
                   path.Add(new int2(cameFromNode.x,cameFromNode.y));
                   currentNode = cameFromNode;
               }

               return path;
           }
       }
       private bool IsPositionInsideGrid(int2 gridPosition,int2 gridsizeParameter)
       {
           return gridPosition.x >= 0 &&
                  gridPosition.y >= 0 &&
                  gridPosition.x < gridsizeParameter.x &&
                  gridPosition.y < gridsizeParameter.y;
       }
       private int CalculateIndex(int x, int y,int gridwidth)
       {
           return x + y * gridwidth;
       }

       private int CalculateDistanceCost(int2 aPosition, int2 bPosition)
       {
           int xDistance = math.abs(aPosition.x - bPosition.x);
           int yDistance = math.abs(aPosition.y - bPosition.y);
           int remaining = math.abs(xDistance - yDistance);
           return MOVE_DIAGONAL_COST * math.min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
       }

       private int GetLowestCostNodeIndex(NativeList<int> openlist, NativeArray<PathNode> pathNodeArray)
       {
           PathNode lowestCostPathNode = pathNodeArray[openlist[0]];
           for (int i = 0; i < openlist.Length; i++)
           {
               PathNode tesstPathNode = pathNodeArray[openlist[i]];
               if (tesstPathNode.fcost < lowestCostPathNode.fcost)
               {
                   lowestCostPathNode = tesstPathNode;
               }
           }

           return lowestCostPathNode.index;
       }
       private struct PathNode
       {
           public int x;
           public int y;
           
           public int index;

           public int gcost;
           public int hcost;
           public int fcost;

           public bool isWalkable;

           public int cameFromNodeIndex;

           public void CalculateFCost()
           {
               fcost = gcost + hcost;
           }
           
       }

       
   }*/
}
  