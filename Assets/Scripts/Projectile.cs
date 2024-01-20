using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 Velocity { get; set; }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
}
