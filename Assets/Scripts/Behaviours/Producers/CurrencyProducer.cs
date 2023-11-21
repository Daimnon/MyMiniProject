using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyProducer : Producer
{
    [SerializeField] private ProducerType _type;
    public override ProducerType Type => _type;

    [SerializeField] private GameObject _productPrefab;
    public override GameObject ProductPrefab => _productPrefab;

    [SerializeField] private Transform[] _productsTr;
    public override Transform[] ProductsTr => _productsTr;

    [SerializeField] private List<Resource> _products;
    public override List<Resource> Products { get => _products; set => _products = value; }

    private PlayerInventory _playerInventory;
    public override PlayerInventory PlayerInventory { get => _playerInventory; set => _playerInventory = value; }

    private int[] _prices = new int[System.Enum.GetValues(typeof(ResourceType)).Length];

    public override void ChargePrice() // need to figure out
    {
        EventManager.InvokePayFirstResource(_playerInventory);
        ProduceProduct();
    }
    public override void StopCharging()
    {
    }
    public override void ProduceProduct()
    {
    }
}
