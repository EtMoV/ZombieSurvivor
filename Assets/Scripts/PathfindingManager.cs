using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    public static PathfindingManager Instance;

    private Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    private bool isProcessingPath = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RequestPath(Vector3 startPos, Vector3 targetPos, System.Action<List<GridManager.Node>> callback)
    {
        PathRequest newRequest = new PathRequest(startPos, targetPos, callback);
        pathRequestQueue.Enqueue(newRequest);
        if (!isProcessingPath)
        {
            StartCoroutine(ProcessQueue());
        }
    }

    private IEnumerator ProcessQueue()
    {
        isProcessingPath = true;

        while (pathRequestQueue.Count > 0)
        {
            PathRequest request = pathRequestQueue.Dequeue();

            // ✅ Vérification que l'objet n'est pas détruit
            if (request.callback == null)
                continue;

            List<GridManager.Node> path = null;

            // ✅ On essaye de récupérer un Pathfinding dans la scène
            Pathfinding pathfinding = FindFirstObjectByType<Pathfinding>();
            if (pathfinding != null)
            {
                path = pathfinding.FindPath(request.startPos, request.targetPos);
            }

            // ✅ On renvoie le path seulement si le callback existe encore
            request.callback?.Invoke(path);

            // ⚡ on peut yield 0 pour répartir la charge sur plusieurs frames
            yield return null;
        }

        isProcessingPath = false;
    }

    private struct PathRequest
    {
        public Vector3 startPos;
        public Vector3 targetPos;
        public System.Action<List<GridManager.Node>> callback;

        public PathRequest(Vector3 _start, Vector3 _target, System.Action<List<GridManager.Node>> _callback)
        {
            startPos = _start;
            targetPos = _target;
            callback = _callback;
        }
    }
}
