using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private List<ProjectileController> _projectileList = new List<ProjectileController>();

    public void SpawnProjectile(Transform prefab, Vector3 position, Vector3 rotation, float projectileSpeed, bool isEnemy, float duration)
    {
        bool projectileFound = false;

        foreach(ProjectileController obj in _projectileList)
        {
            if(!obj.gameObject.activeSelf)
            {
                obj.gameObject.SetActive(true);
                obj.transform.position = position;
                obj.transform.eulerAngles = rotation;
                obj.ResetProjectile(projectileSpeed, isEnemy, duration);
                projectileFound = true;
                break;
            }
        }

        if(!projectileFound)
        {
            ProjectileController projectile = Instantiate(prefab.GetComponent<ProjectileController>(), position, Quaternion.Euler(rotation));
            projectile.ResetProjectile(projectileSpeed, isEnemy, duration);
            _projectileList.Add(projectile);
        }

        Debug.Log("Projectile Fired");
        
    }
}
