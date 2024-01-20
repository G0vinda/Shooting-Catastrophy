using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject[] targets;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in targets)
        {
            Debug.Log(obj.transform.position);
            GameObject projectileInstance = Instantiate(projectilePrefab, transform);
            Vector3 normalized3 = (obj.transform.position - projectileInstance.transform.position).normalized;
            Vector2 normalized2 = new Vector2(
                normalized3.x,
                normalized3.y
            );
        }
    }
}
