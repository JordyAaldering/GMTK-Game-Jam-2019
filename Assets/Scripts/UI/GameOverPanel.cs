#pragma warning disable 0649
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _heart;
        [SerializeField] private GameObject _heartDestroyEffect;

        private void Awake()
        {
            _heart.SetActive(false);
            Vector2 pos = Camera.main.ScreenToWorldPoint(_heart.transform.position);
            Instantiate(_heartDestroyEffect, pos, Quaternion.identity);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
