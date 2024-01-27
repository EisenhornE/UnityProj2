using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region For Movement Variables
    private Vector2 _moveInput;
    private Vector2 _mousePos;
    private Rigidbody2D _rb2d;
    [SerializeField] float playerSpeed;
    #endregion

    #region For Shooting Mechanic Variables
    [SerializeField] float bulletSpeed;
    public Camera cam;
    public GameObject bulletPrefab;
    public Transform barrel;
    private bool canShoot;
    #endregion

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        canShoot = true;
    }

    
    void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");
        _moveInput.Normalize();


        if(Input.GetMouseButton(0) && canShoot)
        {
            Shoot();
            StartCoroutine(ShotCooldown(0.5f));
        }
    }

    IEnumerator ShotCooldown(float cooldownTime)
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
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
