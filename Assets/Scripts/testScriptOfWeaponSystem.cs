using System;
using Player.Weapons;
using UnityEngine;

public class testScriptOfWeaponSystem : MonoBehaviour
{
    public Weapon weapon;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
        WeaponManager.Instance.AddWeapon(weapon);
        Destroy(this.gameObject);
    }
}
