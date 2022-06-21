using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private float _speed;

    private Transform _currentWayPoint;
    private int _currentWayPointPosition;

    private void Start()
    {
        if (_wayPoints != null)
        {
            _currentWayPointPosition = 0;
            _currentWayPoint = _wayPoints[_currentWayPointPosition];
        }
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _currentWayPoint.transform.position, _speed * Time.deltaTime);

        if (transform.position.x != _currentWayPoint.position.x)
            return;

        if (_currentWayPointPosition + 1 < _wayPoints.Count)
            _currentWayPointPosition++;
        else if (_currentWayPointPosition + 1 == _wayPoints.Count)
            _currentWayPointPosition = 0;

        _currentWayPoint = _wayPoints[_currentWayPointPosition];
    }
}