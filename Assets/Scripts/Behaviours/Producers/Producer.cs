using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Producer : MonoBehaviour
{
    public abstract ResourceObjectPool ResourcePool { get; set; }
    public abstract ProducerType Type { get; }
    public abstract int MaxProducts { get; set; }
    public abstract float ProductionTime { get; }
    public abstract bool IsFull { get; set; }
    public abstract Transform[] ProductsTr { get; }
    public abstract List<Resource> Products { get; }

    public abstract void Produce();
    protected void Initialize()
    {
        if (!ResourcePool && GameManager.Instance.ResourcePool)
            ResourcePool = GameManager.Instance.ResourcePool;
    }
    private void GiveProduct(PlayerInventory inventory)
    {
        Resource lastResource = Products[^1];
        inventory.TakeResource(lastResource);
        Products.Remove(lastResource);
    }
}
