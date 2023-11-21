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

    [SerializeField] private float _timeToProduce;
    public override float TimeToProduce => _timeToProduce;

    [SerializeField] private List<Resource> _products;
    public override List<Resource> Products { get => _products; set => _products = value; }

    public override void ChargePrice()
    {
    }
    public override void StopCharging()
    {
    }
    public override void ProduceProduct()
    {
    }
}
