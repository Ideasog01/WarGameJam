using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float _movementSpeed;

    private bool _isEnemy;

    private float _duration;

    private int _damage;

    private void OnCollisionEnter(Collision collision)
    {
        if(!_isEnemy && collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<EnemyCharacter>().TakeDamage(_damage);
        }
        else if(_isEnemy && collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<SoldierCharacter>().TakeDamage(_damage);
        }

        this.gameObject.SetActive(false);
    }

    public void ResetProjectile(float speed, bool isEnemy, float duration, int damage)
    {
        _movementSpeed = speed;
        _isEnemy = isEnemy;
        _duration = duration;
        _damage = damage;
        StartCoroutine(DelayInactive());
    }

    private void Update()
    {
        this.transform.position += this.transform.forward * Time.deltaTime * _movementSpeed;
    }

    private IEnumerator DelayInactive()
    {
        yield return new WaitForSeconds(_duration);
        this.gameObject.SetActive(false);
    }
}
