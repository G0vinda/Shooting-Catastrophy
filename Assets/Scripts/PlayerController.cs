using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;
    [SerializeField] private Vector3 spawnPoint1;
    [SerializeField] private Vector3 spawnPoint2;
    [SerializeField] private Transform shooterHandle;
    [SerializeField] private Transform shooterTransform;
    [SerializeField] private Projectile projectilePrefab;

    private static int _playerCount = 0;
    
    private Vector2 _currentMoveSpeed;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_playerCount == 0)
        {
            _playerCount = 1;
            _spriteRenderer.color = color1;
            transform.position = spawnPoint1;
        }
        else
        {
            _spriteRenderer.color = color2;
            transform.position = spawnPoint2;
        }
    }

    private void Update()
    {
        transform.position += (Vector3)_currentMoveSpeed * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _currentMoveSpeed = ctx.ReadValue<Vector2>();
        TurnShooterToCurrentDirection();
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if(!ctx.ReadValueAsButton())
            return;
        
        ShootProjectile();
    }

    private void TurnShooterToCurrentDirection()
    {
        var newAngle = Vector2.Angle(Vector2.up, _currentMoveSpeed);
        if (_currentMoveSpeed.x > 0)
            newAngle *= -1;
        shooterHandle.rotation = Quaternion.Euler(0, 0, newAngle);
        Debug.Log(newAngle);
    }

    private void ShootProjectile()
    {
        var projectile = Instantiate(projectilePrefab, shooterTransform.position, shooterHandle.rotation);
    }
}
