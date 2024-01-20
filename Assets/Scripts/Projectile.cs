using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool IsArmed { get; set; } = true;


    // Update is called once per frame
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
}
