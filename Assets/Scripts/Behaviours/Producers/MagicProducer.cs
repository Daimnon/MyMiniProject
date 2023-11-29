using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProducer : Producer
{
    [Header("Product Source")]
    [SerializeField] private ResourceObjectPool _resourcePool;
    public override ResourceObjectPool ResourcePool { get => _resourcePool; set => _resourcePool = value; }

    [Header("Production Details")]
    [SerializeField] private ProducerType _type;
    public override ProducerType Type => _type;

    [SerializeField] private int _maxProducts = 3;
    public override int MaxProducts { get => _maxProducts; set => _maxProducts = value; }

    [SerializeField] private float _productionTime = 0.1f;
    public override float ProductionTime => _productionTime;

    [SerializeField] private bool _isFull;
    public override bool IsFull { get => _isFull; set => _isFull = value; }

    [Header("Product placements")]
    [SerializeField] private Transform[] _productsTr;
    public override Transform[] ProductsTr => _productsTr;

    private List<Resource> _products;
    public override List<Resource> Products => _products;

    private void Awake()
    {
        Initialize();
    }

    public override void Produce()
    {
        if (_isFull)
            return;

        for (int i = 0; i < _productsTr.Length && _productsTr[i].childCount < 1; i++)
        {
            int resourceIndex = (int)_type;
            Resource newResource = _resourcePool.GetResourceFromPool(resourceIndex);
            newResource.transform.position = _productsTr[i].position;
            _products.Add(newResource);

            if (i == _maxProducts)
                _isFull = true;

            break;
        }

        if (!_isFull)
            Invoke(nameof(Produce), _productionTime);
    }
}