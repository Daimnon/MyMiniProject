using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponProducer : MonoBehaviour
{
    public abstract WeaponObjectPool WeaponPool { get; set; }
    public abstract WeaponType Type { get; }
    public abstract WeaponRarity Rarity { get; }
    public abstract WeaponSize Size { get; }
    public abstract int CapacityUpgradeFactor { get; }
    public abstract int MaxProducts { get; set; }
    public abstract float ProductionTime { get; }
    public abstract bool IsBusy { get; set; }
    public abstract bool IsFull { get; set; }
    public abstract Transform[] SmallProductsTr { get; }
    public abstract Transform[] MediumProductsTr { get; }
    public abstract Transform[] LargeProductsTr { get; }
    public abstract List<Weapon> SmallProducts { get; }
    public abstract List<Weapon> MediumProducts { get; }
    public abstract List<Weapon> LargeProducts { get; }

    public abstract void Produce();
    protected void Initialize()
    {
        if (!WeaponPool && GameManager.Instance.WeaponPool)
            WeaponPool = GameManager.Instance.WeaponPool;
    }
    public void UpgradeCapacity()
    {
        MaxProducts += CapacityUpgradeFactor;
    }
    public virtual void Upgrade(WeaponSize weaponSizeRank)
    {
        if ((int)weaponSizeRank >= System.Enum.GetValues(typeof(WeaponRarity)).Length - 1)
            return;

        int currentRank = (int)weaponSizeRank;
        weaponSizeRank = (WeaponSize)currentRank+1;
    }
}
