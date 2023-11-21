using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Producer : MonoBehaviour
{
    public abstract ProducerType Type { get; }
    public abstract GameObject ProductPrefab { get; }
    public abstract Transform[] ProductsTr { get; }

    public abstract List<Resource> Products { get; set; }
    public abstract PlayerInventory PlayerInventory { get; set; }

    protected const string _playerTag = "Player";
    protected const string _resourceTag = "Resource";

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_playerTag))
            PlayerInventory = other.GetComponent<PlayerInventory>();

        ChargePrice();
    }
    private void OnTriggerExit(Collider other)
    {
        StopCharging();
        PlayerInventory = null;
    }

    public abstract void ChargePrice();
    public abstract void StopCharging();
    public abstract void ProduceProduct();
}
