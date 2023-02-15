using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform aimTarget;

    private List<ProjectileController> _projectileList = new List<ProjectileController>();

    public void SpawnProjectile(Transform prefab, Vector3 position, Vector3 rotation, float projectileSpeed, bool isEnemy, float duration, int damage)
    {
        bool projectileFound = false;

        foreach(ProjectileController obj in _projectileList)
        {
            if(!obj.gameObject.activeSelf)
            {
                obj.gameObject.SetActive(true);
                obj.transform.position = position;

                if(!isEnemy)
                {
                    obj.transform.LookAt(aimTarget);
                }
                else
                {
                    obj.transform.eulerAngles = rotation;
                }
                
                obj.ResetProjectile(projectileSpeed, isEnemy, duration, damage);
                projectileFound = true;
                break;
            }
        }

        if(!projectileFound)
        {
            ProjectileController projectile = Instantiate(prefab.GetComponent<ProjectileController>(), position, Quaternion.identity);

            if(!isEnemy)
            {
                projectile.transform.LookAt(aimTarget);
            }
            else
            {
                projectile.transform.eulerAngles = rotation;
            }

            projectile.ResetProjectile(projectileSpeed, isEnemy, duration, damage);
            _projectileList.Add(projectile);
        }

        Debug.Log("Projectile Fired");
        
    }
}
