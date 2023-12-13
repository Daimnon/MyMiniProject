using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsRack : MonoBehaviour
{
    private Transform[] _placements = null;
    public Transform[] Placements { get => _placements; set => _placements = value; }

    private List<Weapon> _weapons = null;
    public List<Weapon> Weapons { get => _weapons; set => _weapons = value; }

    private const string _playerTag = "Player";
    private PlayerInventory _playerInventory;

    private void OnTriggerEnter(Collider other)
    {
        if (_weapons != null && other.CompareTag(_playerTag))
        {
            _playerInventory = other.GetComponent<PlayerInventory>();
            Weapon weaponToGive = _weapons[^1];
            _playerInventory.TakeWeapon(weaponToGive);
            _weapons.Remove(weaponToGive);
        }
    }
}