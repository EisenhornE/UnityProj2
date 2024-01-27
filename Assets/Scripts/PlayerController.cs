using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 _moveInput;
    private Vector2 _mousePos;
    [SerializeField] float playerSpeed;
    [SerializeField] float bulletSpeed;
    private Rigidbody2D _rb2d;
    public Camera cam;
    public GameObject bulletPrefab;
    public Transform barrel;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");
        _moveInput.Normalize();

        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        _rb2d.MovePosition(_rb2d.position + _moveInput * playerSpeed * Time.fixedDeltaTime);

        MouseFollow();
    }

    void MouseFollow()
    {
        _mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDirection = _mousePos - _rb2d.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rb2d.rotation = angle;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
        Rigidbody2D bullet_rB = bullet.GetComponent<Rigidbody2D>();
        bullet_rB.AddForce(barrel.up * bulletSpeed, ForceMode2D.Impulse);
    }
}
