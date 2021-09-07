using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace _Code
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    instance = go.AddComponent<GameManager>();
                }

                return instance;
            }
        }

        [Header("References")] 
        [SerializeField] private PlayerController player;
        [SerializeField] private EnemySpawner spawner;
        [SerializeField] private UnityEvent _gameOver;

        [Header("Player Stats")] 
        public float playerDamage;
        public float playerStartingHealth;

        [Header("Enemy Start Stats")] 
        public int enemyStartingMaxHP;
        public float enemyStartingSpeed;
        public Vector3 enemyStartingSize;
        public Vector3 enemyMinSize;
         
        [HideInInspector] public float playerCurrentHealth;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            playerCurrentHealth = playerStartingHealth;

            spawner.enemyMaxHP = enemyStartingMaxHP;
            spawner.enemySpeed = enemyStartingSpeed;
            spawner.enemySize = enemyStartingSize;
        }

        public void ChangeGameStats()
        {
            float enemyStatBoost = Random.Range(1.01f, 1.03f);

            spawner.enemyMaxHP = (int) (spawner.enemyMaxHP * enemyStatBoost);
            spawner.enemySpeed = Mathf.Lerp(spawner.enemySpeed,player.Speed, 0.02f);
            spawner.enemySize = Vector3.Lerp(enemyStartingSize,enemyMinSize,0.03f);
        }

        public void DealDamage()
        {
            playerCurrentHealth -= 1;

            if (playerCurrentHealth <= 0)
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            spawner.gameObject.SetActive(false);
            Destroy(player.gameObject);
            _gameOver.Invoke();
        }

        public PlayerController Player => player;
    }
}