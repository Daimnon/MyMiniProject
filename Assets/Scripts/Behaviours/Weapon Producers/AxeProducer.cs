using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class AxeProducer : WeaponProducer
{
    [Header("Product Source")]
    [SerializeField] private WeaponObjectPool _weaponPool;
    public override WeaponObjectPool WeaponPool { get => _weaponPool; set => _weaponPool = value; }

    [SerializeField] private ResourceObjectPool _resourcePool;
    [SerializeField] private WeaponsRack _weaponRack;

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

    [SerializeField] private int[] _requiredIron;
    [SerializeField] private int _currentIronCount = 0;

    [Header("Product Placements")]
    [SerializeField] private Transform[] _smallProductsTr;
    public override Transform[] SmallProductsTr => _smallProductsTr;

    [SerializeField] private Transform[] _mediumProductsTr;
    public override Transform[] MediumProductsTr => _mediumProductsTr;

    [SerializeField] private Transform[] _largeProductsTr;
    public override Transform[] LargeProductsTr => _largeProductsTr;

    private List<Weapon> _smallProducts;
    public override List<Weapon> SmallProducts => _smallProducts;

    private List<Weapon> _mediumProducts;
    public override List<Weapon> MediumProducts => _mediumProducts;

    private List<Weapon> _largeProducts;
    public override List<Weapon> LargeProducts => _largeProducts;

    private const string _playerTag = "Player";
    private PlayerInventory _playerInventory;
    private IEnumerator _produceWeapons;

    private void Awake()
    {
        _smallProducts = new List<Weapon>();
        _mediumProducts = new List<Weapon>();
        _largeProducts = new List<Weapon>();
    }
    private void Start()
    {
        Initialize();

        if (_requiredIron == null)
            _requiredIron = new int[_weaponPool.UniqueWeaponsCount];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            _playerInventory = other.GetComponent<PlayerInventory>();

            if (_isBusy)
                return;

            StartProducingWeapons();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_playerTag))
            _playerInventory = null;
    }

    public override void Produce() // need to modify for fuel usage
    {
        List<Weapon> currentProducts = null;
        Transform[] productsTr = null;
        switch (_weaponSize)
        {
            case WeaponSize.Small:
                currentProducts = _smallProducts;
                productsTr = _smallProductsTr;
                break;
            case WeaponSize.Medium:
                currentProducts = _mediumProducts;
                productsTr = _mediumProductsTr;
                break;
            case WeaponSize.Large:
                currentProducts = _largeProducts;
                productsTr = _largeProductsTr;
                break;
            default:
                currentProducts = _smallProducts;
                productsTr = _smallProductsTr;
                break;
        }

        _weaponRack.Placements ??= productsTr; // if null get ref to productsTr

        int productCount = _smallProducts.Count + _mediumProducts.Count + _largeProducts.Count;
        if (productCount < _maxProducts && productsTr[productCount].childCount < 1)
        {
            Weapon newWeapon = _weaponPool.GetWeaponFromPool(_weaponType, _weaponRarity, _weaponSize);
            newWeapon.transform.parent = productsTr[productCount];
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;
            currentProducts.Add(newWeapon);
            _weaponRack.Weapons = currentProducts;
            _isBusy = false;

            if (productCount == _maxProducts)
                _isFull = true;
        }
    }

    private IEnumerator ProduceWeapon()
    {
        int sizeRank = (int)_weaponSize;
        ResourceType iron = ResourceType.Iron;
        int price = _requiredIron[sizeRank];

        for (int i = 0; i < price; i++)
        {
            Resource paidIron = _playerInventory.PayResource(iron); // need to check why axe is not producing
            if (!paidIron)
                yield break;

            _resourcePool.ReturnResourceToPool(paidIron);
            _currentIronCount++;

            if (_currentIronCount < price)
            {
                _produceWeapons = null;
                StartProducingWeapons();
                yield break;
            }
            else if (_currentIronCount == price)
                break;
        }
        
        _isBusy = true;
        yield return new WaitForSeconds(_productionTime);

        Produce();
        _produceWeapons = null;
        _currentIronCount -= price;
    }
    private void StartProducingWeapons()
    {
        _produceWeapons = ProduceWeapon();
        StartCoroutine(_produceWeapons);
    }
    private void StopProducingWeapons()
    {
        StopCoroutine(_produceWeapons);
        _produceWeapons = null;
    }
}
