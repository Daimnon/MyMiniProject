using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;

public class Adventurer : Character
{
    [Header("Animation")]
    [SerializeField] private Animator _aiAnimator;
    [SerializeField] private float _idleGestureTime = 7.5f;

    private float _idleTime = 0.0f;
    private bool _isGesturing = false;

    [Header("AI Data")]
    [SerializeField] private AdventurerType _adventurerType;
    public AdventurerType AdventurerType => _adventurerType;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _lookAtOffset = 2.0f;

    private Transform _tradingTr = null;
    public Transform TradingTr { get => _tradingTr; set => _tradingTr = value; }

    private Transform _exitTr = null;
    public Transform ExitTr { get => _exitTr; set => _exitTr = value; }

    private void OnEnable()
    {
        EventManager.OnHoldResource += OnHoldResource;
    }
    private void Start()
    {
        _agent.SetDestination(_tradingTr.position);
        Vector3 lookAtPos = _tradingTr.position;
        lookAtPos.x += _lookAtOffset;
        _agent.transform.LookAt(lookAtPos, Vector3.up);
    }
    private void Update()
    {
        Vector3 scaledMovement = _agent.speed * Time.deltaTime * _agent.velocity.normalized;
        _aiAnimator.SetFloat("Move Speed", scaledMovement.normalized.magnitude);

        if (scaledMovement == Vector3.zero)
        {
            _idleTime += Time.deltaTime;

            if (_idleTime >= _idleGestureTime && !_isGesturing)
            {
                _isGesturing = true;
                _aiAnimator.SetBool("Is Gesturing", _isGesturing);
            }
        }
        else if (_idleTime != 0)
        {
            _idleTime = 0;

            _isGesturing = false;
            _aiAnimator.SetBool("Is Gesturing", _isGesturing);
        }

        // Check if the agent has reached the current target
        if (!_agent.pathPending && _agent.remainingDistance < 0.1f)
        {
            // Perform your action here

            // Switch to the next target position
            if (_agent.destination == _tradingTr.position)
            {
                _agent.SetDestination(_exitTr.position);
                _agent.transform.LookAt(_exitTr.position, Vector3.up);
            }
            else
            {
                // Destroy the GameObject when it reaches the second target position
                Destroy(gameObject);
            }
        }
    }
    private void OnDisable()
    {
        EventManager.OnHoldResource -= OnHoldResource;
    }

    private void OnHoldResource(bool isHoldingResource) // nned to tweak event
    {
        _aiAnimator.SetBool("Is Holding", isHoldingResource);
    }
}
