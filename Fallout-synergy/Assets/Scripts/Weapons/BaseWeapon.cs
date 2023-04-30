using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaseWeapon : MonoBehaviour
{
    private Camera _mainCam;

    private Vector3 _mousePos;

    public GameObject bullet;
    [SerializeField] public int bulletNum;
    public Transform bulletTransform;
    public bool canFire;
    private float _timer;
    public float timeBetweenFiring;

    [SerializeField] private ParticleSystem particleSystem;

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
                particleSystem.Stop();
            }
        }
        
        if (Input.GetMouseButton(0) && canFire)
        {
            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
        for (int i = 0; i < bulletNum; i++)
        {
            Vector3 bulletPosition = new Vector3(Random.Range(bulletTransform.position.x - .5f, bulletTransform.position.x + .5f), 
                Random.Range(bulletTransform.position.y - .5f, bulletTransform.position.y + .5f), bulletTransform.position.z);
            Instantiate(bullet, bulletPosition, Quaternion.identity);
        }

        canFire = false;
        particleSystem.Play();
    }
}
