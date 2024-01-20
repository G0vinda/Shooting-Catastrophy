using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public enum PlayerState
{
    Cat = 0,
    Whale,
    Food
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Sprite cat1;
    [SerializeField] private Sprite cat2;
    [SerializeField] private Sprite catWhale1;
    [SerializeField] private Sprite catWhale2;
    [SerializeField] private Sprite catFood1;
    [SerializeField] private Sprite catFood2;
    [SerializeField] private AudioClip shootSound1;
    [SerializeField] private AudioClip shootSound2;
    [SerializeField] private AudioClip toWhaleSound1;
    [SerializeField] private AudioClip toWhaleSound2;
    [SerializeField] private AudioClip toFoodSound1;
    [SerializeField] private AudioClip toFoodSound2;
    [SerializeField] private AudioClip eatSound;
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
    private bool _canShoot = true;
    private Coroutine _reloading;

    public PlayerState State { get; private set; } = PlayerState.Cat;

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
    }

    private void ShootProjectile()
    {
        if(!_canShoot) return;

        _canShoot = false;
        var projectile = Instantiate(projectilePrefab, shooterTransform.position, shooterHandle.rotation);
        _audioSource.Play();
        _reloading = StartCoroutine(nameof(CoReload));
    }

    private IEnumerator CoReload()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        _canShoot = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if (projectile != null && projectile.IsArmed)
        {
            switch (State)
            {
                case PlayerState.Cat:
                    TransformToWhale();
                    break;
                case PlayerState.Whale:
                    TransformToFood();
                    break;
                default:
                    return;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController enemyPlayerController = other.gameObject.GetComponent<PlayerController>();
        if (enemyPlayerController != null && enemyPlayerController.State == PlayerState.Food)
        {
            _audioSource.clip = eatSound;
            _audioSource.Play();
            Destroy(enemyPlayerController.gameObject);
            
            _canShoot = false;
            StopCoroutine(nameof(CoReload));
        }
    }

    private void TransformToWhale()
    {
        State = PlayerState.Whale;
        _currentSpeed *= 0.5f;
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
    
    private void TransformToFood()
    {
        StopCoroutine(_reloading);
        _canShoot = false;
        
        State = PlayerState.Food;
        _currentSpeed = 0.0f;
        if (_appearance == 1)
        {
            _spriteRenderer.sprite = catFood1;
            _audioSource.clip = toFoodSound1;
        }
        else
        {
            _spriteRenderer.sprite = catFood2;
            _audioSource.clip = toFoodSound2;
        }
        _audioSource.Play();
    }
}
