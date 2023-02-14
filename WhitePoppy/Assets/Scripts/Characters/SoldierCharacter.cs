using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierCharacter : PlayerCharacter
{
    [SerializeField]
    private Transform projectilePrefab;

    [SerializeField]
    private Transform spawnPos;

    [SerializeField]
    private float projectileMovementSpeed;

    [SerializeField]
    private float projectileDuration;

    [SerializeField]
    private int maxAmmo;

    [SerializeField]
    private float reloadTime;

    [SerializeField]
    private float fireCooldown;

    private bool _fireDisabled;

    private int _ammo;

    private SpawnManager _spawnManager;

    private bool _isReloading;

    private void Awake()
    {
        _spawnManager = GameObject.Find("GameManager").GetComponent<SpawnManager>();
        _ammo = maxAmmo;
    }

    public void Fire()
    {
        if(_ammo > 0 && !_fireDisabled)
        {
            _spawnManager.SpawnProjectile(projectilePrefab, spawnPos.position, spawnPos.eulerAngles, projectileMovementSpeed, false, projectileDuration);
            _ammo--;
            StartCoroutine(FireCooldown());
            Debug.Log("Ammo: " + _ammo);
        }
    }

    public void Reload()
    {
        if(!_isReloading && !_fireDisabled)
        {
            _ammo = 0;
            _isReloading = true;
            StartCoroutine(ReloadDelay());
        }
    }

    private IEnumerator FireCooldown()
    {
        _fireDisabled = true;
        yield return new WaitForSeconds(fireCooldown);
        _fireDisabled = false;
    }

    private IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(reloadTime);
        _ammo = maxAmmo;
        _isReloading = false;
    }
}
