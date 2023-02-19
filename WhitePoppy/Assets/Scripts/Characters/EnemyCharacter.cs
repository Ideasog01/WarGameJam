using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCharacter : BaseCharacter
{
    [SerializeField]
    private GameObject rifleObj;

    private NavMeshAgent _navMeshAgent;

    private SoldierCharacter _soldierCharacter;

    private bool _isInAttackRange;

    private void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _soldierCharacter = GameManager.playerController;
        _navMeshAgent.updateRotation = false;
    }

    public void Attack()
    {
        if(!FireDisabled && Ammo > 0)
        {
            SpawnManagerRef.SpawnProjectile(ProjectilePrefab, SpawnPos.position, SpawnPos.eulerAngles, ProjectileMovementSpeed, true, ProjectileDuration, ProjectileDamage);
            CharacterAnimator.SetTrigger("fire");
            Ammo--;
        }
    }

    private bool IsPlayerNear()
    {
        float distanceToPlayer = Vector2.Distance(this.transform.position, _soldierCharacter.transform.position);

        _isInAttackRange = distanceToPlayer <= 10 && !CharacterAnimator.GetBool("isWalking");

        return distanceToPlayer < 15;
    }

    public void Chase()
    {
        _navMeshAgent.SetDestination(_soldierCharacter.transform.position);
    }

    private void LookAtPlayer()
    {
        Debug.Log("Looking at player!");
        this.transform.LookAt(_soldierCharacter.transform.position);
        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
    }

    private void Update()
    {
<<<<<<< HEAD
        if(Health > 0 && _soldierCharacter.Health > 0 && GameManager.gameInProgress)
=======
        if(Health > 0 && _soldierCharacter.Health > 0)
>>>>>>> parent of 9db7fa1 (Last update)
        {
            if (IsPlayerNear())
            {
                Chase();
            }

            if (_isInAttackRange && !FireDisabled && !IsReloading && !CharacterAnimator.GetBool("isWalking"))
            {
                if (Ammo > 0)
                {
                    Attack();
                    StartCoroutine(FireCooldown());
                }
                else
                {
                    StartCoroutine(Reload());
                }
            }

            LookAtPlayer();

            if (!_navMeshAgent.pathPending)
            {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        CharacterAnimator.SetBool("isWalking", false);
                    }
                }
            }
            else
            {
                CharacterAnimator.SetBool("isWalking", true);
            }
        }
    }

    private IEnumerator FireCooldown()
    {
        FireDisabled = true;
        yield return new WaitForSeconds(FireCooldownRef);
        FireDisabled = false;
    }

    private IEnumerator Reload()
    {
        IsReloading = true;
        CharacterAnimator.SetTrigger("reload");
        Ammo = 0;
        yield return new WaitForSeconds(ReloadTime);
        Ammo = MaxAmmo;
        IsReloading = false;
    }

    public void OnDeath()
    {
        CharacterAnimator.SetTrigger("dead");
        _navMeshAgent.enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        rifleObj.SetActive(false);
<<<<<<< HEAD

        GameManager.objectiveManager.UpdateObjective(1, Objective.ObjectiveType.DefeatEnemy);
=======
>>>>>>> parent of 9db7fa1 (Last update)
    }
}
