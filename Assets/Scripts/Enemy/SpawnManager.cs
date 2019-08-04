#pragma warning disable 0649
using System;
using System.Collections;
using Extensions;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float _x = 20f;
    [SerializeField] private float _y = 20f;

    [SerializeField] private GameObject _waterPrefab;
    [SerializeField] private GameObject[] _enemies;

    [SerializeField] private float _spawnTimer;
    [SerializeField] private int _maxSpawned;
    private int _maxSpawnedTotal;
    
    [HideInInspector] public int _spawned;

    private void Awake()
    {
        _maxSpawnedTotal = _maxSpawned + PlayerPrefs.GetInt("Day", 1);
        
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        PlayerManager player = FindObjectOfType<PlayerManager>();
        
        while (!player.IsDead)
        {
            if (_spawned < _maxSpawnedTotal)
            {
                SpawnEnemy();
            }

            if (Random.value > 0.75f)
            {
                SpawnWater();
            }
            
            yield return new WaitForSeconds(_spawnTimer);
        }
    }

    private void SpawnEnemy()
    {
        _spawned++;

        float x = Random.Range(-_x, _x);
        float y = Random.Range(-_y, _y);
        Instantiate(_enemies.GetRandom(), new Vector2(x, y), Quaternion.identity);
    }

    private void SpawnWater()
    {
        float x = Random.Range(-_x, _x);
        float y = Random.Range(-_y, _y);
        Instantiate(_waterPrefab, new Vector2(x, y), Quaternion.identity);
    }
}
