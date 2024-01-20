using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Sprite cat1;
    [SerializeField] private Sprite cat2;
    [SerializeField] private Sprite catWhale1;
    [SerializeField] private Sprite catWhale2;
    [SerializeField] private AudioClip shootSound1;
    [SerializeField] private AudioClip shootSound2;
    [SerializeField] private Vector3 spawnPoint1;
    [SerializeField] private Vector3 spawnPoint2;
    [SerializeField] private Transform shooterHandle;
    [SerializeField] private Transform shooterTransform;
    [SerializeField] private Projectile projectilePrefab;

    private static int _playerCount = 0;
    private int _appearance;
    
    private Vector2 _currentMoveSpeed;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        if (_playerCount == 0)
        {
            _playerCount = 1;
            _spriteRenderer.sprite = cat1;
            transform.position = spawnPoint1;
            _appearance = 1;
            _audioSource.clip = shootSound1;
        }
        else
        {
            _spriteRenderer.sprite = cat2;
            transform.position = spawnPoint2;
            _appearance = 2;
            _audioSource.clip = shootSound2;
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
        _audioSource.Play();
    }

    private void TransformToWhale()
    {
        if (_appearance == 1)
        {
            _spriteRenderer.sprite = catWhale1;
        }
        else
        {
            _spriteRenderer.sprite = catWhale2;
        }
    }
}
