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
        Debug.Log(_currentMoveSpeed);
    }
}
