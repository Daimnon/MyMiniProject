using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeProducer : WeaponProducer
{
    [Header("Product Source")]
    [SerializeField] private WeaponObjectPool _weaponPool;
    public override WeaponObjectPool WeaponPool { get => _weaponPool; set => _weaponPool = value; }

    /*[SerializeField] private FuelStove _engine;
    public FuelStove Engine => _engine;*/

    [Header("Production Details")]
    [SerializeField] private WeaponType _weaponType = WeaponType.Axe;
    public override WeaponType Type => _weaponType;

    [SerializeField] private WeaponRarity _weaponRarity = WeaponRarity.Common;
    public override WeaponRarity Rarity => _weaponRarity;

    [SerializeField] private WeaponSize  _weaponSize = WeaponSize.Small;
    public override WeaponSize Size => _weaponSize;

    [SerializeField] private int _capacityUpgradeFactor = 3;
    public override int CapacityUpgradeFactor { get => _capacityUpgradeFactor; }

    [SerializeField] private int _maxProducts = 3;
    public override int MaxProducts { get => _maxProducts; set => _maxProducts = value; }

    [SerializeField] private float _productionTime = 10.0f;
    public override float ProductionTime => _productionTime;

    [SerializeField] private bool _isBusy = false;
    public override bool IsBusy { get => _isBusy; set => _isBusy = value; }
    
    [SerializeField] private bool _isFull = false;
    public override bool IsFull { get => _isFull; set => _isFull = value; }

    [Header("Product Placements")]
    [SerializeField] private Transform[] _productsTr;
    public override Transform[] ProductsTr => _productsTr;

    private List<Weapon> _products;
    public override List<Weapon> Products => _products;

    private const string _playerTag = "Player";
    private PlayerInventory _playerInventory;

    private void Awake()
    {
        _products = new List<Weapon>();
    }
    private void Start()
    {
        Initialize();
        Produce();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            _playerInventory = other.GetComponent<PlayerInventory>();

            if (_isBusy)
                return;

            _playerInventory.PayResource(ResourceType.Iron);
            StartCoroutine(ProduceWeapon());
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (!_playerInventory || _isBusy)
            return;

        _playerInventory.PayResource(ResourceType.Iron);
        StartCoroutine(ProduceWeapon());
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_playerTag))
            _playerInventory = null;
    }

    public override void Produce() // need to modify for fuel usage
    {
        int productCount = _products.Count;
        if (productCount < _maxProducts && _productsTr[productCount].childCount < 1)
        {
            Weapon newResource = _weaponPool.GetWeaponFromPool(_weaponType, _weaponRarity, _weaponSize);
            newResource.transform.position = _productsTr[productCount].position;
            _products.Add(newResource);
            _isBusy = false;

            if (productCount == _maxProducts)
                _isFull = true;
        }
    }

    private IEnumerator ProduceWeapon()
    {
        _isBusy = true;

        yield return new WaitForSeconds(_productionTime);
        Produce();
    }
}
