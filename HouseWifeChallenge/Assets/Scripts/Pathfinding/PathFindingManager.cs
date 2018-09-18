using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Utils;

// 
public class PathFindingManager : MonoBehaviour {

    public static int limitStepAlgo = 1000;
    private Queue<Node> currentPath;
    public PathfindingGrid grid;

	// Get the path to follow using a A* start algorithm
    public Queue<Node> GetPathWithAStarAlgo(Vector2 startPos, Vector2 targetPos)
    {
        List<Node> openList = new List<Node>();
        List<Node> closeList = new List<Node>();
        
        Node startNode = grid.GetNodeFromWorldPosition(startPos);
        Node targetNode = grid.GetNodeFromWorldPosition(targetPos);

        // First step
        // Update H value of all the nodes in the grid
        grid.ResetNodesParent();
        grid.ScanObstacles();
        if (targetNode == null || startNode == null  || targetNode.isWall)
        {
            return null;
        }

        foreach (Node node in grid.GetNodes())
        {
            if (node != null)
            {
                node.UpdateHCost(targetNode);
            }
        }
        Node currentNode = startNode;
        int cpt = 0;
        while (currentNode != targetNode)
        {
            openList.Remove(currentNode);
			closeList.AddUnique(currentNode);
         
            // Add neighbour nodes to the open list if they are not in the closeList
            List<Node> neighbours = grid.GetFreeNeighbours(currentNode);
            foreach (Node node in neighbours)
            {   
                if (!closeList.Contains(node))
                {
                    // We change the parent node if it is a shorter way to access
					if (node.parent == null || node.GCost > node.ComputeGCostFrom(currentNode))
					{
						node.parent = currentNode;
					}
					openList.AddUnique(node);
                }
            }
            currentNode = GetBestCandidate (openList, targetNode);
            cpt++;
            if (cpt > limitStepAlgo)
            {
                Debug.Log("Reached iteration limit during pathFinding algorithm");
                return null;
            }
        }
        //Debug.Log("number of iterations: " + cpt);
        currentPath = GetPathFromNode(currentNode);
        return currentPath;
    }

	
	// Get the node in the given list with the lowest F value
	// If the node is the target node, return it
	private static Node GetBestCandidate(List<Node> openList, Node targetNode)
	{
		Node candidate = null;
		float minCost = float.MaxValue;
		foreach (Node node in openList)
		{
			if (node == targetNode)
			{
                candidate = node;
				break;
			}
			else if (node.FCost < minCost)
			{
                candidate = node;
				minCost = node.FCost;
			}
		}
		return candidate;
	}

	// Get the path leading to the final node using the node parent chain
	// Stop when the node is null (starting position)
	private static Queue<Node> GetPathFromNode(Node node)
	{
		Queue<Node> pathFromEnd = new Queue<Node>();
		while (node != null)
        {
            pathFromEnd.Enqueue(node);
            node = node.parent;
        }
		return new Queue<Node>(pathFromEnd.Reverse());
	}

	// Conver a Queue<Node> into a Queue<Vector2> containing the nodes world position
    public static Queue<Vector2> ConvertPathToWorldCoord(Queue<Node> nodes)
    {
        if (nodes == null) return null;
        Queue<Vector2> worldPositions = new Queue<Vector2>();
        foreach (Node node in nodes)
        {
            worldPositions.Enqueue(node.worldPos);
        }
        return worldPositions;
    }
	
	public Queue<Node> getCurrentPath()
	{
		return currentPath;
	}
}
