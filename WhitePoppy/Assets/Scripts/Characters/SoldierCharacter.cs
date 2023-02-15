using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierCharacter : BaseCharacter
{
    public static InteractItem interactItem;

    public static bool disableCombatMechanics;

    [SerializeField]
    private GameObject characterMesh;

    private PlayerInterface _playerInterface;

    private void Start()
    {
        _playerInterface = GameObject.Find("GameManager").GetComponent<PlayerInterface>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void Fire()
    {
        if(Ammo > 0 && !FireDisabled && !disableCombatMechanics && characterMesh.activeSelf)
        {
            SpawnManagerRef.SpawnProjectile(ProjectilePrefab, SpawnPos.position, Vector3.zero, ProjectileMovementSpeed, false, ProjectileDuration, ProjectileDamage);
            Ammo--;
            StartCoroutine(FireCooldown());
            CharacterAnimator.SetTrigger("fire");
        }
    }

    public void Reload()
    {
        if(!IsReloading && !FireDisabled && Ammo < MaxAmmo && !disableCombatMechanics && characterMesh.activeSelf)
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

    public void OnPlayerDeath()
    {
        _playerInterface.DisplayGameOverScreen();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.EnableCamera(false);
    }

    public void ToggleRifle()
    {
        characterMesh.SetActive(!characterMesh.activeSelf);
    }
}
