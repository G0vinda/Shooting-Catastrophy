using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
public class Obstacle : MonoBehaviour, ITarget
{
    [SerializeField] protected GameObject[] transformations;
    
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if (projectile != null && projectile.IsArmed)
        {
            projectile.IsArmed = false;
            OnHit();
        }
    }
}
