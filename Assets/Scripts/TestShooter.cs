using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(CoShoot));
    }
    

    private IEnumerator CoShoot()
    {
        while (true)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform);
            Vector3 normalized3 = (target.transform.position - projectileInstance.transform.position).normalized;
            Vector2 normalized2 = new Vector2(
                normalized3.x,
                normalized3.y
            );

            projectileInstance.GetComponent<Projectile>().Velocity = normalized2;
                
            yield return new WaitForSecondsRealtime(1.0f);
        }
    }
}
