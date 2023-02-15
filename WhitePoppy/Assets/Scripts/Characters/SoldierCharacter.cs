using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierCharacter : BaseCharacter
{
    public static InteractItem interactItem;

    public void Fire()
    {
        if(Ammo > 0 && !FireDisabled)
        {
            SpawnManagerRef.SpawnProjectile(ProjectilePrefab, SpawnPos.position, ProjectileMovementSpeed, false, ProjectileDuration, ProjectileDamage);
            Ammo--;
            StartCoroutine(FireCooldown());
            CharacterAnimator.SetTrigger("fire");
        }
    }

    public void Reload()
    {
        if(!IsReloading && !FireDisabled && Ammo < MaxAmmo)
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
}
