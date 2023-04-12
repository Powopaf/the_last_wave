using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World;

public class PathNode : MonoBehaviour
{
    private TileDefinition[,] Map;
    private int x;
    private int y;
    public int gCost;
    public int hCost;
    public int fCost;
    public PathNode cameFromNode;
    public PathNode(TileDefinition[,] map, int x, int y)
    {
        this.x = x;
        this.y = y;
        Map = map;
    }

    public override string ToString()
    {
        return x + "," + y;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
