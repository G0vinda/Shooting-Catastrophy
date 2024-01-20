using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
public class Obstacle : MonoBehaviour, ITarget
{
    [SerializeField] protected GameObject[] transformations;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    protected static T GetRandom<T>(T[] array)
    {
        int index = UnityEngine.Random.Range(0, array.Length);
        return array[index];
    }

    private void Randomize()
    {
        GameObject prefab = GetRandom(transformations);
        GameObject instance = Instantiate(prefab);
        instance.transform.position = transform.position;

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody2D instanceRigidbody = instance.GetComponent<Rigidbody2D>();

        instanceRigidbody.velocity = rigidbody.velocity;
        instanceRigidbody.angularVelocity = rigidbody.angularVelocity;
        
        Destroy(gameObject);
    }

    public virtual void OnHit()
    {
        Randomize();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if (projectile != null && projectile.IsArmed)
        {
            projectile.IsArmed = false;
            OnHit();
        }
    }
}
