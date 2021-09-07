using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public EnemyAI _prefab;
    public float _arenaX;
    public float _arenaY;
    [HideInInspector] public int enemyMaxHP;
    [HideInInspector] public float enemySpeed;
    [HideInInspector] public Vector3 enemySize = Vector3.one*3;
    private int lastVertical = -1;

    public void SpawnEnemyAtRandomPositionOutsideArena()
    {
        float randomStrechX = Random.Range(-1.0f, +1.0f) * _arenaX;
        float randomStrechY = Random.Range(1.1f, 1.3f) * _arenaY;

        Vector3 spawnPosition = new Vector3(randomStrechX, lastVertical*randomStrechY);
        lastVertical *= -1;
        
        EnemyAI spawnedEnemy = Instantiate(_prefab, spawnPosition, quaternion.identity);
        
        EnemySetup(spawnedEnemy);
    }

    private void EnemySetup(EnemyAI enemy)
    {
        float multiplier = Random.Range(0.7f, 1.2f);
        
        enemy.MaxHealth = enemyMaxHP;
        enemy.SetHealthToMax();
        
        enemy.Speed = enemySpeed * multiplier;

        enemy.transform.localScale = enemySize*multiplier;
    }
    
}
