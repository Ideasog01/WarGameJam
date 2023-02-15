using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform aimTarget;

    private List<ProjectileController> _projectileList = new List<ProjectileController>();

    public void SpawnProjectile(Transform prefab, Vector3 position, float projectileSpeed, bool isEnemy, float duration, int damage)
    {
        bool projectileFound = false;

        foreach(ProjectileController obj in _projectileList)
        {
            if(!obj.gameObject.activeSelf)
            {
                obj.gameObject.SetActive(true);
                obj.transform.position = position;
                obj.transform.eulerAngles = Vector3.zero;
                obj.transform.LookAt(aimTarget);
                obj.ResetProjectile(projectileSpeed, isEnemy, duration, damage);
                projectileFound = true;
                break;
            }
        }

        if(!projectileFound)
        {
            ProjectileController projectile = Instantiate(prefab.GetComponent<ProjectileController>(), position, Quaternion.identity);
            projectile.transform.LookAt(aimTarget);
            projectile.ResetProjectile(projectileSpeed, isEnemy, duration, damage);
            _projectileList.Add(projectile);
        }

        Debug.Log("Projectile Fired");
        
    }
}
