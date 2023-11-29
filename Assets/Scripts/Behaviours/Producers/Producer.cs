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

    private void GiveProduct(PlayerInventory inventory)
    {
        Resource lastResource = Products[^1];
        inventory.TakeResource(lastResource);
        Products.Remove(lastResource);
    }
    protected void Initialize()
    {
        if (!ResourcePool)
            ResourcePool = GameManager.Instance.ResourcePool;
    }
    public void Produce()
    {
        if (IsFull)
            return;

        for (int i = 0; i < ProductsTr.Length && ProductsTr[i].childCount < 1; i++)
        {
            int resourceIndex = (int)Type;
            Resource newResource = ResourcePool.GetResourceFromPool(resourceIndex);
            newResource.transform.position = ProductsTr[i].position;
            Products.Add(newResource);

            if (i == MaxProducts)
                IsFull = true;

            break;
        }

        if (!IsFull)
            Invoke(nameof(Produce), ProductionTime);
    }
}
