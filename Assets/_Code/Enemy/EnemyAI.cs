using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using _Code;
using TMPro;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform target;
    private int currentHealth;
    [SerializeField] private Collider2D _splashHitbox;
    [SerializeField] private ProjectileDeath _death;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float speed;

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public void SetHealthToMax()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        target = GameManager.Instance.Player?.transform;
        if (target == null)
        {
            Destroy(this.gameObject);
        }
        SetHealthToMax();
    }

    // Update is called once per frame
    void Update()
    {
        SeekPlayer();
    }

    private void SeekPlayer()
    {
        if(target == null)
            Destroy(this.gameObject);

        Vector3 direction = (target.position - this.transform.position).normalized;

        Vector3 velocity = direction * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);
        
        this.transform.position += velocity * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.DealDamage();
            _death.EnableParticleDrift();
            Destroy(this.gameObject);
            return;
        }
        
        currentHealth -= (int) GameManager.Instance.playerDamage;
        if (currentHealth <= 0)
        {
            if (other.CompareTag("Projectile"))
            {
                TriggerSplash();
            }
            
            ScoreTracker.instance.EnemyKilled();
            _death.EnableParticleDrift();
            Destroy(this.gameObject);
        }
    }

    private void TriggerSplash()
    {
        _splashHitbox.gameObject.SetActive(true);   
    }
}
