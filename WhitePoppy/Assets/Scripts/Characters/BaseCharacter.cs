using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private Transform projectilePrefab;

    [SerializeField]
    private Transform spawnPos;

    [SerializeField]
    private Animator characterAnimator;

    [SerializeField]
    private float projectileMovementSpeed;

    [SerializeField]
    private float projectileDuration;

    [SerializeField]
    private int projectileDamage;

    [SerializeField]
    private int maxAmmo;

    [SerializeField]
    private float reloadTime;

    [SerializeField]
    private float fireCooldown;

    [SerializeField]
    private UnityEvent onDeathEvent;

    [SerializeField]
    private UnityEvent onTakeDamageEvent;

    private bool _fireDisabled;

    private int _ammo;

    private int _health;

    private SpawnManager _spawnManager;

    private SoundSystem soundSystem;

    private bool _isReloading;

    public SpawnManager SpawnManagerRef
    {
        get { return _spawnManager; }
    }

    public bool IsReloading
    {
        get { return _isReloading; }
        set { _isReloading = value; }
    }

    public bool FireDisabled
    {
        get { return _fireDisabled; }
        set { _fireDisabled = value; }
    }

    public int Ammo
    {
        get { return _ammo; }
        set { _ammo = value; }
    }

    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public int MaxAmmo
    {
        get { return maxAmmo; }
    }

    public float ReloadTime
    {
        get { return reloadTime; }
    }

    public float FireCooldownRef
    {
        get { return fireCooldown; }
    }

    public Transform ProjectilePrefab
    {
        get { return projectilePrefab; }
    }

    public float ProjectileDuration
    {
        get { return projectileDuration; }
    }

    public float ProjectileMovementSpeed
    {
        get { return projectileMovementSpeed; }
    }

    public int ProjectileDamage
    {
        get { return projectileDamage; }
    }

    public Transform SpawnPos
    {
        get { return spawnPos; }
    }

    public Animator CharacterAnimator
    {
        get { return characterAnimator; }
    }

    private void Awake()
    {
        _spawnManager = GameObject.Find("GameManager").GetComponent<SpawnManager>();
        _ammo = maxAmmo;
        _health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;

        onTakeDamageEvent.Invoke();
        soundSystem.PlaySoundEffect(2);
        if (_health <= 0)
        {
            onDeathEvent.Invoke();
        }

        Debug.Log(gameObject.name + " Health: " + _health);
    }
}
