using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Utils;

[RequireComponent(typeof(PathFindingManager))]
public class PlayerController : MonoBehaviour {

    public float speed;
	
    [Tooltip("Enable / Disable the manual deplacement of the character by clicking on the place to move")]
	public bool manualMode = true;
	
	private Rigidbody2D rb2d;
    Vector2 playerMove = Vector2.zero;
	
	// Pathfing related attributes
	PathFindingManager pathFindingManager;
	bool isFollowingPath;
	Queue<Vector2> path; 
	Vector2 nextPathPosition;
    public GameMode gameMode;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        pathFindingManager = GetComponent<PathFindingManager>(); 
    }

	// Called every frame
    private void Update() 
    {
		playerMove = Vector2.zero; // reset each time
		
		if (Input.GetMouseButtonDown(0) && manualMode) // moving to the mouse position
		{ 
			Vector3 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 targetPos = new Vector2(mousePositon.x, mousePositon.y);
			MoveTo(targetPos);
		}
		
		if (isFollowingPath)
		{
			FollowPath();
		}
    }


    public void OnGameModeChanged()
    {
        manualMode = gameMode.GetValue() == GameModeType.PlayerControl;
    }

    

	// Called every "physics" frame
    void FixedUpdate()
    {
        rb2d.velocity = playerMove.normalized * speed; // move the player
    }
	
	// Go to the given position using an A* pathfinding algorithm
	public void MoveTo (Vector2 newPosition)
	{
		Vector2 startPos = new Vector2(transform.position.x, transform.position.y);
        Queue<Node> nodePath = pathFindingManager.GetPathWithAStarAlgo(startPos, newPosition);
        StartFollowingPath (PathFindingManager.ConvertPathToWorldCoord(nodePath));
	}

    // Move to the given object
    // Check all the paths leading to tile neibourgh from this object
    // Choose the shorten path
    // if no path exist, return false
    public bool MoveToObject(GameObject targetObj)
    {
        pathFindingManager.grid.ScanObstacles(); // important to check where the colliders are
        List<Node> potentialNodes = pathFindingManager.grid.GetFreeNeighbours(targetObj);
        string str = "";
        foreach (Node node in potentialNodes)
        {
            str += " " + node.ToString();
        }
        Debug.Log("Free nodes at:" + str);
        Queue<Node> pathToFollow = GetShortestPathToTargets (potentialNodes);
        if (pathToFollow != null && pathToFollow.Count > 0)
        {
            StartFollowingPath (PathFindingManager.ConvertPathToWorldCoord(pathToFollow));
            return true;
        }
        else
        {
            return false;
        }
    }

    private Queue<Node> GetShortestPathToTargets(List<Node> targets)
    {
        if (targets == null) return null;
        Queue<Node> shortestPath = null;
        foreach (Node node in targets)
        {
            Queue<Node> path = pathFindingManager.GetPathWithAStarAlgo(transform.position, node.worldPos);
            if (shortestPath == null || shortestPath.Count > path.Count)
            {
                shortestPath = path;
            }
        }
        return shortestPath;
    }

    // Set the new path to follow
    private void StartFollowingPath(Queue<Vector2> path)
	{
		this.path = path;
		isFollowingPath = true;
		if (path != null && path.Count > 0)
		{
			nextPathPosition = path.Dequeue();
		}
		else 
		{
			StopFollowingPath();
		}
	}

    // Stop following the current path
    private void StopFollowingPath()
	{
		isFollowingPath = false;
		path = null;
        nextPathPosition = new Vector2(transform.position.x, transform.position.y);
	}

    // Follow the current path by updating the playerMove vector
    private void FollowPath()
	{
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 movement = nextPathPosition - currentPosition;
		if (GetNorm2(movement) <  2 * speed * Time.deltaTime)
		{
			if (path != null && path.Count != 0)
			{
				nextPathPosition = path.Dequeue();
				movement = nextPathPosition - currentPosition;
			}
			else
			{
				movement = Vector2.zero;
				StopFollowingPath();
			}
		}
		playerMove = movement;
	}

    
}
