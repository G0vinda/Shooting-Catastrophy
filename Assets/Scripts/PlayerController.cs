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
    [SerializeField] private AudioClip toWhaleSound1;
    [SerializeField] private AudioClip toWhaleSound2;
    [SerializeField] private Vector3 spawnPoint1;
    [SerializeField] private Vector3 spawnPoint2;
    [SerializeField] private Transform shooterHandle;
    [SerializeField] private Transform shooterTransform;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float startSpeed;

    private static int _playerCount = 0;
    private int _appearance;
    private float _currentSpeed;
    
    private Vector2 _currentMoveDirection;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _currentSpeed = startSpeed;
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
        transform.position += (Vector3)_currentMoveDirection * (Time.deltaTime * _currentSpeed);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _currentMoveDirection = ctx.ReadValue<Vector2>();
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
        var newAngle = Vector2.Angle(Vector2.up, _currentMoveDirection);
        if (_currentMoveDirection.x > 0)
            newAngle *= -1;
        shooterHandle.rotation = Quaternion.Euler(0, 0, newAngle);
        Debug.Log(newAngle);
    }

    private void ShootProjectile()
    {
        var projectile = Instantiate(projectilePrefab, shooterTransform.position, shooterHandle.rotation);
        _audioSource.Play();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if (projectile != null && projectile.IsArmed)
        {
            TransformToWhale();
        }
    }

    private void TransformToWhale()
    {
        _currentSpeed *= 0.3f;
        if (_appearance == 1)
        {
            _spriteRenderer.sprite = catWhale1;
            _audioSource.clip = toWhaleSound1;
        }
        else
        {
            _spriteRenderer.sprite = catWhale2;
            _audioSource.clip = toWhaleSound2;
        }
        _audioSource.Play();
    }
}
