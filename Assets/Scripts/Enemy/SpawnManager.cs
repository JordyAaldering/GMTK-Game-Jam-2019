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

    [SerializeField] private GameObject[] _enemies;

    [SerializeField] private float _spawnTimer;
    [SerializeField] private int _maxSpawned;
    [HideInInspector] public int _spawned;

    private void Awake()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        PlayerManager player = FindObjectOfType<PlayerManager>();
        
        while (!player.IsDead)
        {
            if (_spawned < _maxSpawned)
            {
                Spawn();
            }
            
            yield return new WaitForSeconds(_spawnTimer);
        }
    }

    private void Spawn()
    {
        _spawned++;

        float x = Random.Range(-_x, _x);
        float y = Random.Range(-_y, _y);
        Instantiate(_enemies.GetRandom(), new Vector2(x, y), Quaternion.identity);
    }
}
