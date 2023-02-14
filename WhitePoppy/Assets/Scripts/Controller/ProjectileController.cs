using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float _movementSpeed;

    private bool _isEnemy;

    private float _duration;

    private void OnCollisionEnter(Collision collision)
    {
        this.gameObject.SetActive(false);
    }

    public void ResetProjectile(float speed, bool isEnemy, float duration)
    {
        _movementSpeed = speed;
        _isEnemy = isEnemy;
        _duration = duration;
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
