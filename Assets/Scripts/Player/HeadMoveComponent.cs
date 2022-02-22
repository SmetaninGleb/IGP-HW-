using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandlerComponent))]
public class HeadMoveComponent : MonoBehaviour
{
    [SerializeField] private MovingConfigSO _movingConfig;
    
    private float _speed;
    private List<Vector3> _directionList;
    private int _currentDirectionIndex = 0;
    private bool _isMoving = false;

    public void StartMoving()
    {
        _isMoving = true;
    }

    private void Awake()
    {
        _speed = _movingConfig.PlayerSpeed;
        _directionList = _movingConfig.DirectionList;
        GetComponent<InputHandlerComponent>().OnInteracted += OnInteractedMethod;
    }

    private void Update()
    {
        if (_isMoving) Move();
    }

    private void OnInteractedMethod()
    {
        if (!_isMoving) return;
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        _currentDirectionIndex++;
        _currentDirectionIndex %= _directionList.Count;
    }

    private void Move()
    {
        Vector3 currentDirection = _directionList[_currentDirectionIndex];
        transform.position += currentDirection.normalized * _speed * Time.deltaTime;
    }
}
