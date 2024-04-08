using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class SupplyDropZone : MonoBehaviour
{
    public GameObject SupplyPrefab;
    public float SupplySpawnCooldown = 10f;

    void Start()
    {
        StartCoroutine(CheckForSupply());
    }

    void SpawnSupply()
    {
        Instantiate(SupplyPrefab, transform.position, Quaternion.identity, transform);
    }
    
    IEnumerator CheckForSupply()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); 
            
            if (transform.childCount == 0)
            {
                yield return new WaitForSeconds(SupplySpawnCooldown); 
                SpawnSupply();
            }
        }
    }
}
