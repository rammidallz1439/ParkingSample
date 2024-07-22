using UnityEngine;

public class MoveToParkingSlotCommand : ICommand
{
    private CarController _car;
    private Vector3 _originalPosition;
    private Vector3 _targetPosition;

    public MoveToParkingSlotCommand(CarController car, Vector3 targetPosition)
    {
        _car = car;
        _originalPosition = car.transform.position;
        _targetPosition = targetPosition;
    }

    public void Execute()
    {
        _car.MoveTo(_targetPosition);
    }

    public void Undo()
    {
        _car.MoveTo(_originalPosition);
    }
}
