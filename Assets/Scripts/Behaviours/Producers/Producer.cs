using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Producer : MonoBehaviour
{
    public abstract ProducerType Type { get; }
    public abstract GameObject ProductPrefab { get; }
    public abstract Transform[] ProductsTr { get; }
    public abstract float TimeToProduce { get; }

    public abstract List<Resource> Products { get; set; }

    protected const string _playerTag = "Player";
    protected const string _resourceTag = "Resource";

    private void OnTriggerEnter(Collider other)
    {
        ChargePrice();
    }
    private void OnTriggerExit(Collider other)
    {
        StopCharging();
    }

    public abstract void ChargePrice();
    public abstract void StopCharging();
    public abstract void ProduceProduct();
}
