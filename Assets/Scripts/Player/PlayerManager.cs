#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Text _dropsText;
        [SerializeField] private GameObject _gameOverPanel;
        
        public bool IsDead { get; private set; }

        public int waterDrops
        {
            get => PlayerPrefs.GetInt("WaterDrops");
            set
            {
                PlayerPrefs.SetInt("WaterDrops", value);
                _dropsText.text = value.ToString();
            }
        }

        private void Awake()
        {
            _dropsText.text = waterDrops.ToString();
        }

        public void Die()
        {
            IsDead = true;
            _gameOverPanel.SetActive(true);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerCombat>().enabled = false;
        }
    }
}
