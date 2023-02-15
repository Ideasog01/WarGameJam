using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierCharacter : BaseCharacter
{
    public static InteractItem interactItem;

    public static bool disableCombatMechanics;

    private PlayerInterface _playerInterface;

    private void Start()
    {
        _playerInterface = GameObject.Find("GameManager").GetComponent<PlayerInterface>();
    }

    public void Fire()
    {
        if(Ammo > 0 && !FireDisabled && !disableCombatMechanics)
        {
            SpawnManagerRef.SpawnProjectile(ProjectilePrefab, SpawnPos.position, Vector3.zero, ProjectileMovementSpeed, false, ProjectileDuration, ProjectileDamage);
            Ammo--;
            StartCoroutine(FireCooldown());
            CharacterAnimator.SetTrigger("fire");
        }
    }

    public void Reload()
    {
        if(!IsReloading && !FireDisabled && Ammo < MaxAmmo && !disableCombatMechanics)
        {
            CharacterAnimator.SetTrigger("reload");
            Ammo = 0;
            IsReloading = true;
            StartCoroutine(ReloadDelay());
        }
    }

    private IEnumerator FireCooldown()
    {
        FireDisabled = true;
        yield return new WaitForSeconds(FireCooldownRef);
        FireDisabled = false;
    }

    private IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(ReloadTime);
        Ammo = MaxAmmo;
        IsReloading = false;
    }

    public void OnTakeDamage()
    {
        _playerInterface.UpdateDamageScreen(Health);
    }
}
