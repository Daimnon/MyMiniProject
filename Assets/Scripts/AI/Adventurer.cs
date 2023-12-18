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
    [SerializeField] private Transform _itemPos;
    [SerializeField] private float _lookAtOffset = 2.0f;

    private Transform _tradingTr = null;
    public Transform TradingTr { get => _tradingTr; set => _tradingTr = value; }

    private Transform _exitTr = null;
    public Transform ExitTr { get => _exitTr; set => _exitTr = value; }

    private bool _isAlive = true;
    public bool IsAlive { get => _isAlive; set => _isAlive = value; }

    private bool _hasBoughtItem = false;
    public bool HasBoughtItem => _hasBoughtItem;

    private AIObjectPool _aiPool = null;

    private ResourceObjectPool _resourcePool = null;
    private Resource _resource = null;

    private WeaponObjectPool _weaponPool = null;
    private Weapon _weapon = null;

    private const string _resourceCustomerTag = "ResourceCustomer", _weaponCustomerTag = "WeaponCustomer";

    private void OnEnable()
    {
        EventManager.OnHoldResource += OnHoldResource;
    }
    private void Start()
    {
        _agent.SetDestination(_tradingTr.position);
        _aiPool = GameManager.Instance.AiPool;
        _resourcePool = GameManager.Instance.ResourcePool;
        _weaponPool = GameManager.Instance.WeaponPool;

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
        if (_isAlive && !_agent.pathPending && _agent.remainingDistance < 0.1f)
        {
            if (!_hasBoughtItem)
                return;

            Vector2 correctDestination = new(_agent.destination.x, _agent.destination.z);
            Vector2 correctTarget = new(_tradingTr.position.x, _tradingTr.position.z);

            if (correctDestination == correctTarget)
            {
                _agent.SetDestination(_exitTr.position);
                _agent.transform.LookAt(_exitTr.position, Vector3.up);
            }
            else if (gameObject.CompareTag(_resourceCustomerTag))
            {
                _isAlive = false;
                _resourcePool.ReturnResourceToPool(_resource);
                _resource = null;
                EventManager.InvokeHoldResource(this, false);
                _aiPool.RespawnAdventurer(this);
            }
            else if (gameObject.CompareTag(_weaponCustomerTag))
            {
                _isAlive = false;
                _weaponPool.ReturnWeaponToPool(_weapon);
                _weapon = null;
                EventManager.InvokeHoldWeapon(this, false);
                _aiPool.RespawnAdventurer(this);
            }
        }
    }
    private void OnDisable()
    {
        EventManager.OnHoldResource -= OnHoldResource;
    }

    private void OnHoldResource(Character chara, bool isHoldingResource) // nned to tweak event
    {
        if (chara is Adventurer && chara as Adventurer == this)
            _aiAnimator.SetBool("Is Holding", isHoldingResource);
    }
    public void BuyItem(PlayerInventory inventory)
    {
        _hasBoughtItem = true;

        if (gameObject.CompareTag(_resourceCustomerTag))
        {
            EventManager.InvokeHoldResource(this, true);
            _resource = inventory.PayFirstResource();
            _resource.transform.parent = _itemPos;
            _resource.transform.localPosition = Vector3.zero;
            _resource.transform.localRotation = Quaternion.identity;
        }
        else if (gameObject.CompareTag(_weaponCustomerTag))
        {
            EventManager.InvokeHoldWeapon(this, true);
            _weapon = inventory.GiveWeapon();
            _weapon.transform.parent = _itemPos;
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.transform.localRotation = Quaternion.identity;
        }
    }
}
