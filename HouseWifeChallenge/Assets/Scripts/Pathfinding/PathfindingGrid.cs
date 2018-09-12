using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Utils;

[CreateAssetMenu (menuName = "PathFinding/Grid", fileName = "Grid")]
public class PathfindingGrid : ScriptableObject {

    public TileMapVariable worldMap; // tileMap defining the word size and grid size
    public int Width => WorldMap.size.x;
    public int Height => WorldMap.size.y;
    public float CellSize => WorldMap.cellSize.x;
    private Tilemap WorldMap => worldMap.Value;
	Node[,] nodes;

    private void OnEnable()
    {
        Init(worldMap);
    }

    public void Init(TileMapVariable worldMap)
	{
        this.worldMap = worldMap;
        this.WorldMap.CompressBounds();
        Debug.Log(string.Format("Grid Initialized. Properties: width:{0}, height:{1}, cellsize: {2}", Width, Height, CellSize));
        InitNodes();
	}
	
	// Init the nodes[,] containing all the node of the grid
    public void InitNodes()
    {
        nodes = new Node[Width, Height];
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                int gridX = WorldMap.cellBounds.xMin + x;
                int gridY = WorldMap.cellBounds.yMin + y;
                Vector3Int localPlace = new Vector3Int(gridX, gridY, (int)WorldMap.transform.position.z);
                if (WorldMap.HasTile(localPlace))
                {
                    Vector3 worldPos = WorldMap.GetCellCenterWorld(localPlace);
                    nodes[x, y] = new Node(x, y, new Vector2(worldPos.x, worldPos.y));
                }
            }
        }
    }

	// Put all the parent of the grid nodes to null
	// Always use at the begining of A* algo
    public void ResetNodesParent()
    {
        foreach (Node node in nodes)
        {
            if (node != null)
            {
                node.parent = null;
            }
        }
    }

	// Check for all nodes if they contain a collider
    public void ScanObstacles()
    {
        foreach (Node node in nodes)
        {
            if (node != null)
            {
                node.isWall = HasCollider(node.worldPos);
            }
        }
    }

	// Check if the given position contains a collider
    public bool HasCollider(Vector2 pos)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, CellSize / 8);
        if (colliders.Length > 0)
        {
            Debug.Log("Found Colliders");
        }
        return colliders.Length > 0;
    }


    // Return the free neighbours node of one defined node
	// If a wall exist on the bottom, its neihbours nodes right and left are considered are not walkable.
	// Same for left / right / top neighbour wall.
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        if (node != null)
        {
            int xMin = Mathf.Max(node.gridX - 1, 0);
            int xMax = Mathf.Min(node.gridX + 1, Width - 1);
            int yMin = Mathf.Max(node.gridY - 1, 0);
            int yMax = Mathf.Min(node.gridY + 1, Height - 1);

            bool isTopWall = !CanWalkPosition(node.gridX, node.gridY + 1);
            bool isBottomWall = !CanWalkPosition(node.gridX, node.gridY - 1);
            bool isLeftWall = !CanWalkPosition(node.gridX - 1, node.gridY);
            bool isRightWall = !CanWalkPosition(node.gridX + 1, node.gridY);

            for (int x = xMin; x <= xMax; x++)
            {
                for (int y = yMin; y <= yMax; y++)
                {
                    if ((x == node.gridX && y == node.gridY)
                        || !CanWalkPosition(x,y))
                    {
                        // ne rien faire
                    }
                    else
                    {
                        if ((isTopWall && y == node.gridY + 1)
                            || (isBottomWall && y == node.gridY - 1)
                            || (isLeftWall && x == node.gridX - 1)
                            || (isRightWall && x == node.gridX + 1))
                        {
                            // ne rien faire
                        }
                        else
                        {
                            neighbours.Add(nodes[x, y]);
                        }                
                    }                    
                }
            }
        }
        return neighbours;
    }

	// Return true if the given position is free (not wall, not empty and not outside the borders)
    public bool CanWalkPosition(int x, int y)
    {
        if (x < 0 || y < 0 || x > Width - 1 || y > Height - 1)
        {
            return false;
        }
        else
        {
            return nodes[x, y] != null && !nodes[x, y].isWall;
        }
    }

	// Return the Node corresponding the guven world position
    public Node GetNodeFromWorldPosition(Vector2 position)
    {
        Vector2Int localPosition = GetNodePositionFromWorldPosition(position);
		if (localPosition.x < 0 || localPosition.x > Width - 1 
				|| localPosition.y < 0 || localPosition.y > Height - 1)
		{
            return null;
		}
		else
		{
			return nodes[localPosition.x, localPosition.y];
		}
    }

	// Get the node position in the nodes array (x, y) from the world position
    public Vector2Int GetNodePositionFromWorldPosition(Vector2 position)
    {
        Vector2 nodeZeroPosition = nodes[0, 0].worldPos;
        float dX = position.x - nodeZeroPosition.x;
        float dY = position.y - nodeZeroPosition.y;
        int x = (int) Mathf.Round(dX / CellSize);
        int y = (int) Mathf.Round(dY / CellSize);
        return new Vector2Int(x, y);
    }

    public Node[,] GetNodes()
    {
        return nodes;
    }
}
