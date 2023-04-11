

/*
public class Node 
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
public class ZombiePathFinding:MonoBehaviour
{
    public Node[,] nodes;
    private Node endNode;
    private GameObject Target;
    private TileDefinition[,] staticmap;


    public ZombiePathFinding(Node[,] nodes, GameObject target, TileDefinition[,] map )
    {
        this.nodes = nodes;
        this.endNode = nodes[5,5]; //Player coordinates 
        staticmap = map;
    }

    private void Awake()
    {
        Target=GameObject.FindWithTag("Player");
    }

    void Start()
    {
        nodes = InitializeMap(staticmap);
        // Set the initial endNode to the target's current position
        Vector3 targetPosition = Target.transform.position;
        int targetX = Mathf.RoundToInt(targetPosition.x);
        int targetY = Mathf.RoundToInt(targetPosition.y);
        endNode = nodes[targetX, targetY];
    }

    public Node[,] InitializeMap(TileDefinition[,] map) 
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);
        Node[,] initializeMap = new Node[width, height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                bool isWalkable = true; //We need to acces to tile iswalkable
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
                    if ((y < height - 1 && x<width-1) && initializeMap[x+1, y + 1].IsWalkable) 
                    {
                        initializeMap[x, y].neighbors.Add(initializeMap[x+1, y + 1]);
                    }
                    if ((y >0 - 1 &&x>0) && initializeMap[x-1, y -1].IsWalkable) 
                    {
                        initializeMap[x, y].neighbors.Add(initializeMap[x-1, y -1]);
                    }
                    if ((y < height - 1 && x>0) && initializeMap[x-1, y + 1].IsWalkable) 
                    {
                        initializeMap[x, y].neighbors.Add(initializeMap[x-1, y + 1]);
                    }
                    if ((y >0 &&x<width-1) && initializeMap[x+1, y -1].IsWalkable) 
                    {
                        initializeMap[x, y].neighbors.Add(initializeMap[x+1, y -1]);
                    }
                }
            }
        }

        return initializeMap;
    }
    
    public float HeuristicCost(Node a, Node b) {
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

            if (currentNode == endNodeParameter) {
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

                    if (!openList.Contains(neighbor)) {
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
   
}
*/
namespace Monsters
{
    public class ZombiePathFinding
    {
        public ZombiePathFinding(int width, int height)
        {
            
        }
    }
}