using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoldierCharacter : BaseCharacter
{
    public static InteractItem interactItem;

    public static bool disableCombatMechanics;

    [SerializeField]
    private GameObject characterMesh;

    [Header("Sounds")]

    [SerializeField]
    private Sound fireSound;

    [SerializeField]
    private Sound reloadSound;

    [SerializeField]
    private Sound damageSound;

    [SerializeField]
    private Sound deathSound;

    [SerializeField]
    private Sound toggleRifleSound;

    private PlayerInterface _playerInterface;

    private void Start()
    {
        _playerInterface = GameObject.Find("GameManager").GetComponent<PlayerInterface>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        _playerInterface.UpdateAmmo(Ammo, MaxAmmo);
        _playerInterface.UpdateHealth(Health, MaxHealth);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            disableCombatMechanics = true;
        }
    }

    public void Fire()
    {
        if(Ammo > 0 && !FireDisabled && !disableCombatMechanics && characterMesh.activeSelf)
        {
            SpawnManagerRef.SpawnProjectile(ProjectilePrefab, SpawnPos.position, Vector3.zero, ProjectileMovementSpeed, false, ProjectileDuration, ProjectileDamage);
            Ammo--;
            StartCoroutine(FireCooldown());
            CharacterAnimator.SetTrigger("fire");
            _playerInterface.UpdateAmmo(Ammo, MaxAmmo);
            GameManager.soundSystem.PlaySound(fireSound);
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
            _playerInterface.UpdateAmmo(Ammo, MaxAmmo);
            GameManager.soundSystem.PlaySound(reloadSound);
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
        _playerInterface.UpdateAmmo(Ammo, MaxAmmo);
    }

    public void OnTakeDamage()
    {
        _playerInterface.UpdateHealth(Health, MaxHealth);
        _playerInterface.UpdateDamageScreen(Health);
        GameManager.soundSystem.PlaySound(damageSound);
    }

    public void OnPlayerDeath()
    {
        _playerInterface.DisplayGameOverScreen();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.EnableCamera(false);
        GameManager.soundSystem.PlaySound(deathSound);
    }

    public void ToggleRifle()
    {
        if(!disableCombatMechanics)
            characterMesh.SetActive(!characterMesh.activeSelf);
        GameManager.soundSystem.PlaySound(toggleRifleSound);
    }
}
