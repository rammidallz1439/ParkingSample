using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CarController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private CommandInvoker _invoker;
    [SerializeField] private CarSlot carSlot;
    [SerializeField] private float _rayDistance = 10f;
    [SerializeField] private LayerMask _obstacleLayer;

    private Vector3 _originalPosition;
    private bool _isAtParkingSlot = false;

    private void Start()
    {
        if (_agent == null)
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        if (_invoker == null)
        {
            _invoker = FindObjectOfType<CommandInvoker>();
        }

        _originalPosition = transform.position;

        // Set stopping distance to a small value
        _agent.stoppingDistance = 0.5f; // Adjust this value based on your needs
        _agent.autoBraking = false;     // Disable auto braking to have more control
    }

    private void OnMouseDown()
    {
        Vector3 targetPosition = _isAtParkingSlot ? _originalPosition : carSlot.ParkingSlot.transform.position;

        if (IsPathClear(targetPosition))
        {
            ICommand command = new MoveToParkingSlotCommand(this, targetPosition);
            _invoker.ExecuteCommand(command);
            _isAtParkingSlot = !_isAtParkingSlot;
        }
        else
        {
            Debug.Log("No valid path to the target parking slot.");
        }
    }

    public void MoveTo(Vector3 position)
    {
        if (_agent != null)
        {
            _agent.SetDestination(position);
            StartCoroutine(WaitUntilReached(position));
        }
    }

    private IEnumerator WaitUntilReached(Vector3 targetPosition)
    {
        // Continuously check if the agent has reached the target position
        while (Vector3.Distance(transform.position, targetPosition) > _agent.stoppingDistance)
        {
            yield return null;
        }

        // Fine-tune the position to ensure it's exactly at the target
        transform.position = targetPosition;
        _agent.velocity = Vector3.zero; // Stop any residual movement
    }

    private bool IsPathClear(Vector3 targetPosition)
    {
        if (!IsObstacleInDirection(transform.forward))
        {
            return true;
        }

        Vector3[] directions = {
            transform.right,
            -transform.right,
            -transform.forward
        };

        foreach (Vector3 direction in directions)
        {
            if (!IsObstacleInDirection(direction))
            {
                if (TryMoveInDirection(direction, targetPosition))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool IsObstacleInDirection(Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, _rayDistance, _obstacleLayer);
    }

    private bool TryMoveInDirection(Vector3 direction, Vector3 targetPosition)
    {
        Vector3 newPosition = transform.position + direction * _rayDistance;
        if (NavMesh.SamplePosition(newPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            NavMeshPath path = new NavMeshPath();
            _agent.CalculatePath(targetPosition, path);
            return path.status == NavMeshPathStatus.PathComplete && path.corners.Length > 0;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Vector3[] directions = {
            transform.forward,
            -transform.forward,
            transform.right,
            -transform.right
        };

        Gizmos.color = Color.red;

        foreach (Vector3 direction in directions)
        {
            Gizmos.DrawRay(transform.position, direction * _rayDistance);
        }
    }
}
