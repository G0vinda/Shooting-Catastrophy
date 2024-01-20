using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 Velocity { get; set; }
    public bool IsArmed { get; set; } = true;


    // Update is called once per frame
    void Update()
    {
        Transform t = transform;
        Vector3 position = t.localPosition;
        Vector3 newPosition = new Vector3(
            position.x + (Velocity.x * Time.deltaTime),
            position.y + (Velocity.y * Time.deltaTime),
            position.z
        );
        t.localPosition = newPosition;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
