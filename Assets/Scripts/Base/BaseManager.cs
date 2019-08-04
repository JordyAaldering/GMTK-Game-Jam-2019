﻿#pragma warning disable 0649
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Base
{
    public class BaseManager : MonoBehaviour
    {
        [SerializeField] private float _x = 8f;
        [SerializeField] private float _y = 4f;

        [SerializeField] private Text _dropsText;
        
        public int waterDrops
        {
            get => PlayerPrefs.GetInt("WaterDrops");
            set
            {
                PlayerPrefs.SetInt("WaterDrops", value);
                _dropsText.text = value.ToString();
            }
        }
        
        private Transform _player;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            Vector2 pos = _player.position;
            if (pos.x < -_x || pos.x > _x || pos.y < -_y || pos.y > _y)
            {
                FindObjectOfType<PlantManager>().CheckDeath();
                SceneManager.LoadScene(1);
            }
        }
    }
}
