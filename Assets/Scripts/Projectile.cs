using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeToLive;
    public bool IsArmed { get; set; } = true;

    private void Start()
    {
        Invoke("DestroySelf", timeToLive);
    }

    void Update()
    {
        Transform t = transform;
        Vector3 position = t.position;
        var newPosition = position + t.up * (Time.deltaTime * speed);
        t.position = newPosition;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if (projectile != null) return;
        Destroy(gameObject);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
