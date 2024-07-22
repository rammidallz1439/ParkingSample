using UnityEngine;
using UnityEngine.AI;
using Vault;

public class CarController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    private bool _isAtParkingSlot = false;
    [SerializeField] private CommandInvoker _invoker;

    private Vector3 _originalPosition;
    [SerializeField] private CarSlot _carSlot;


    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _invoker = FindObjectOfType<CommandInvoker>();
        _originalPosition = transform.position;
    }

    void OnMouseDown()
    {


        Vector3 targetPosition = _isAtParkingSlot ? _originalPosition : _carSlot.ParkingSlot.transform.position;

        if (!IsPathBlocked(targetPosition))
        {
            ICommand command = new MoveToParkingSlotCommand(this, targetPosition);
            _invoker.ExecuteCommand(command);
            _isAtParkingSlot = !_isAtParkingSlot;
        }
    }

    public void MoveTo(Vector3 position)
    {
        _agent.SetDestination(position);
    }

    private bool IsPathBlocked(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        _agent.CalculatePath(targetPosition, path);
        return path.status != NavMeshPathStatus.PathComplete;
    }
}
