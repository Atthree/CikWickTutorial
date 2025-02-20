using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class CatController : MonoBehaviour
{
    public event Action OnCatCatched;

    [Header("References")]
    [SerializeField] private PlayerController _playerContoller;
    [SerializeField] private Transform _playerTransform;

    [Header("Settings")]
    [SerializeField] private float _defaulSpeed = 5f;
    [SerializeField] private float _chaseSpeed = 7f;

    [Header("Navigation Settings")]
    [SerializeField] private float _patrolRadius = 10f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private int _maxDestinationAttemps = 10;
    [SerializeField] private float _chaseDistanceThreshold = 1.5f;
    [SerializeField] private float _chaseDistance = 2f;
    private NavMeshAgent _catAgent;
    private CatStateController _catStateController;
    private float _timer;
    private bool _isChasing;
    private bool _isWaiting;
    private Vector3 _initialPosition;

    private void Awake()
    {
        _catAgent = GetComponent<NavMeshAgent>();
        _catStateController = GetComponent<CatStateController>();
    }
    private void Start()
    {
        _initialPosition = transform.position;
        SetRandomDestination();
    }
    private void Update()
    {
        if (_playerContoller.CanCatChase())
        {
            SetChaseMovement();
        }
        else
        {
            SetPatrolMovement();
        }
    }
    private void SetChaseMovement()
    {
        _isChasing = true;

        Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;
        Vector3 offsetPosition = _playerTransform.position - directionToPlayer * _chaseDistanceThreshold;
        _catAgent.SetDestination(offsetPosition);
        _catAgent.speed = _chaseSpeed;
        _catStateController.ChangeState(CatState.Running);

        if(Vector3.Distance(transform.position,_playerTransform.position) <= _chaseDistance && _isChasing)
        {
            OnCatCatched?.Invoke();
            _catStateController.ChangeState(CatState.Attacking);
            _isChasing = false;
        }
    }
    private void SetPatrolMovement()
    {
        _catAgent.speed = _defaulSpeed;
        if (!_catAgent.pathPending && _catAgent.remainingDistance <= _catAgent.stoppingDistance)
        {
            if (!_isWaiting)
            {
                _isWaiting = true;
                _timer = _waitTime;
                _catStateController.ChangeState(CatState.Idle);
            }
        }
        if (_isWaiting)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _isWaiting = false;
                SetRandomDestination();
                _catStateController.ChangeState(CatState.Walking);
            }
        }
    }

    private void SetRandomDestination()
    {
        int attempts = 0;
        bool destinationSet = false;

        while (attempts < _maxDestinationAttemps && !destinationSet)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * _patrolRadius;

            randomDirection += _initialPosition;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _patrolRadius, NavMesh.AllAreas))
            {
                Vector3 finalPoisiton = hit.position;

                if (!IsPositionBlocked(finalPoisiton))
                {
                    _catAgent.SetDestination(finalPoisiton);
                    destinationSet = true;
                }
                else
                {
                    attempts++;
                }
            }
            else
            {
                attempts++;
            }
        }
        if (!destinationSet)
        {
            Debug.LogWarning("Failed to find a valid destination");
            _isWaiting = true;
            _timer = _waitTime * 2;
        }
    }

    private bool IsPositionBlocked(Vector3 position)
    {
        if (NavMesh.Raycast(transform.position, position, out NavMeshHit hit, NavMesh.AllAreas))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = (_initialPosition != Vector3.zero) ? _initialPosition : transform.position;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos, _patrolRadius);
    }
}
