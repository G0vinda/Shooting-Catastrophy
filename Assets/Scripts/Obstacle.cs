using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject[] Transformations;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static T GetRandom<T>(T[] array)
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }

    private void Randomize()
    {
        GameObject prefab = ();
        GameObject instance = Instantiate(prefab, transform);
        instance.transform.SetParent(transform.root);

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody2D instanceRigidbody = instance.GetComponent<Rigidbody2D>();

        instanceRigidbody.velocity = rigidbody.velocity;
        instanceRigidbody.angularVelocity = rigidbody.angularVelocity;

        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Projectile>() != null)
        {
            
        }
    }
}
