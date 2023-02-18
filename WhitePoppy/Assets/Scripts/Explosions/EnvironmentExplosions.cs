using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentExplosions : MonoBehaviour
{
    private List<GameObject> explositionPosition = new List<GameObject>();

    [SerializeField]
    private GameObject explosionPrefab;


    void Start()
    {
        // add all children from this parent to expositionPosition
        foreach (Transform child in transform)
        {
            explositionPosition.Add(child.gameObject);
        }
        
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(1f);

        //Random number from the length of the explositionPosition
        int randomExplosion = Random.Range(0, explositionPosition.Count);
        Instantiate(explosionPrefab, explositionPosition[randomExplosion].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(4f);
        StartCoroutine(Explosion());
    }
}
