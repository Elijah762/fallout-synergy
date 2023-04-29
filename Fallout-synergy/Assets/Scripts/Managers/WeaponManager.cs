using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Camera _mainCam;

    private Vector3 _mousePos;

    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float _timer;
    public float timeBetweenFiring;

    private void Start()
    {
        _mainCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        _mousePos = _mainCam.ScreenToWorldPoint((Input.mousePosition));

        Vector3 rotation = _mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            _timer += Time.deltaTime;
            if (_timer > timeBetweenFiring)
            {
                canFire = true;
                _timer = 0;
            }
        }
        
        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
    }
}
