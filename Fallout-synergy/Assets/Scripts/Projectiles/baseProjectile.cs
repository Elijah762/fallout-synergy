using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class baseProjectile : MonoBehaviour
{
    public Tile OccupiedTile;

    private Vector3 _mousePos;
    
    private Camera _mainCam;

    private Rigidbody2D _rb;

    public float force;

    [SerializeField] public float distanceLimit;
    private float _timeInAir;
    void Start()
    {
        _mainCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _rb = GetComponent<Rigidbody2D>();
        _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = _mousePos - transform.position;
        Vector3 rotation = transform.position - _mousePos;
        _rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    private void Update()
    {
        _timeInAir += Time.deltaTime;
        
        if (_timeInAir >= distanceLimit)
        {
            Destroy(this.gameObject);
        }
        
    }
}
