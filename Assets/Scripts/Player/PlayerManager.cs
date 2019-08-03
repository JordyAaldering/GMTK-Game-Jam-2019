#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Text _dropsText;
        [SerializeField] private GameObject _gameOverPanel;

        private int _waterDrops;
        public int waterDrops
        {
            get => _waterDrops;
            set
            {
                _waterDrops = value;
                _dropsText.text = _waterDrops.ToString();
            }
        }

        public void Die()
        {
            _gameOverPanel.SetActive(true);

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerCombat>().enabled = false;
        }
    }
}
