using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public GridManager gridManager;
    public LayerMask obstacleMask;
    public bool useDirectLine = true;
    private List<GridManager.Node> lastPath;

    public List<GridManager.Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        if (gridManager == null)
        {
            Debug.LogError("GridManager non assigné !");
            return null;
        }

        // 🔹 Raycast direct pour la ligne de vue
        if (useDirectLine)
        {
            Vector2 dir = targetPos - startPos;
            float distance = dir.magnitude;
            RaycastHit2D hit = Physics2D.Raycast(startPos, dir.normalized, distance, obstacleMask);
            if (hit.collider == null)
            {
                lastPath = new List<GridManager.Node>
            {
                gridManager.NodeFromWorldPoint(targetPos)
            };
                return lastPath;
            }
        }

        // 🔹 A* classique
        GridManager.Node startNode = gridManager.NodeFromWorldPoint(startPos);
        GridManager.Node endNode = gridManager.NodeFromWorldPoint(targetPos);

        if (startNode == null || endNode == null) return null;
        if (!startNode.walkable) return null;

        List<GridManager.Node> openSet = new List<GridManager.Node> { startNode };
        HashSet<GridManager.Node> closedSet = new HashSet<GridManager.Node>();

        while (openSet.Count > 0)
        {
            GridManager.Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost ||
                    (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == endNode)
            {
                lastPath = RetracePath(startNode, endNode);
                return lastPath;
            }

            foreach (GridManager.Node neighbor in gridManager.GetNeighbors(currentNode))
            {
                // Ignore les nodes déjà visités ou non marchables
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                    continue;

                // 🔹 Buffer dynamique autour des obstacles
                int buffer = 2;
                if (gridManager.IsNearObstacle(neighbor, buffer))
                {
                    // Si aucun chemin possible avec buffer 2, on peut essayer buffer 1
                    if (!gridManager.IsNearObstacle(neighbor, 1))
                    {
                        // autoriser ce node
                    }
                    else
                    {
                        continue; // sinon on ignore ce node
                    }
                }

                int newCost = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newCost < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newCost;
                    neighbor.hCost = GetDistance(neighbor, endNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }

        }

        lastPath = null;
        return null;
    }

    // 🔹 Vérifie si node est trop proche d’un obstacle (buffer cases)
    private bool IsTooCloseToObstacle(GridManager.Node node, int buffer)
    {
        Queue<GridManager.Node> toCheck = new Queue<GridManager.Node>();
        HashSet<GridManager.Node> visited = new HashSet<GridManager.Node>();
        toCheck.Enqueue(node);
        visited.Add(node);
        int level = 0;

        while (toCheck.Count > 0 && level < buffer)
        {
            int count = toCheck.Count;
            for (int i = 0; i < count; i++)
            {
                GridManager.Node current = toCheck.Dequeue();
                if (!current.walkable) return true;

                foreach (var neighbor in gridManager.GetNeighbors(current))
                {
                    if (!visited.Contains(neighbor))
                    {
                        toCheck.Enqueue(neighbor);
                        visited.Add(neighbor);
                    }
                }
            }
            level++;
        }

        return false;
    }

    // 🔹 Permet de vérifier si un voisin plus éloigné existe pour retomber au buffer 1
    private bool HasFartherOption(GridManager.Node current, GridManager.Node neighbor)
    {
        foreach (var n in gridManager.GetNeighbors(neighbor))
        {
            if (!IsTooCloseToObstacle(n, 2)) return true;
        }
        return false;
    }

    private bool IsNextToObstacle(GridManager.Node node)
    {
        foreach (var n in gridManager.GetNeighbors(node))
        {
            if (!n.walkable) return true; // obstacle adjacent
        }

        // Optionnel : vérifier une case plus loin pour un buffer plus grand
        foreach (var n in gridManager.GetNeighbors(node))
        {
            foreach (var nn in gridManager.GetNeighbors(n))
            {
                if (!nn.walkable) return true; // obstacle à 1 case d'écart
            }
        }

        return false;
    }


    private List<GridManager.Node> RetracePath(GridManager.Node startNode, GridManager.Node endNode)
    {
        List<GridManager.Node> path = new List<GridManager.Node>();
        GridManager.Node currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    private int GetDistance(GridManager.Node a, GridManager.Node b)
    {
        int dstX = Mathf.Abs(a.gridX - b.gridX);
        int dstY = Mathf.Abs(a.gridY - b.gridY);
        return 14 * Mathf.Min(dstX, dstY) + 10 * Mathf.Abs(dstX - dstY);
    }

    private bool IsTooCloseToObstacle(GridManager.Node node)
    {
        int blocked = 0;
        foreach (var n in gridManager.GetNeighbors(node))
            if (!n.walkable) blocked++;

        // Si entouré de 3 ou 4 cases non marchables → bloquer
        return blocked >= 3;
    }

}
