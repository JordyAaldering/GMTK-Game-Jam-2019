#pragma warning disable 0649
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Base
{
    public class PlantManager : MonoBehaviour
    {
        [SerializeField] private int[] _requiredCost;
        [SerializeField] private int[] _upgradeCost;
        [SerializeField] private Sprite[] _plantSprites;

        [SerializeField] private GameObject _tutorialPanel;
        [SerializeField] private Text _requiredText;
        [SerializeField] private Text _upgradeText;

        [SerializeField] private Image _requiredImage;
        [SerializeField] private Image _upgradeImage;
        [SerializeField] private GameObject _spaceImage;

        private int _sceneStartLevel;
        private int _currentRequired;
        private int _currentUpgrade;

        private static int CurrentLevel
        {
            get => PlayerPrefs.GetInt("PlantLevel");
            set => PlayerPrefs.SetInt("PlantLevel", value);
        }

        private Transform _player;
        private BaseManager _base;
        private SpriteRenderer _sr;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _base = FindObjectOfType<BaseManager>();
            _sr = GetComponent<SpriteRenderer>();

            Initialize();

            if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
            {
                StartCoroutine(ShowTutorial());
                
                PlayerPrefs.SetInt("Tutorial", 1);
            }
        }

        private void Initialize()
        {
            _sceneStartLevel = CurrentLevel;
            _sr.sprite = _plantSprites[_sceneStartLevel];

            if (CurrentLevel >= 11)
            {
                _upgradeImage.enabled = false;
                _upgradeText.enabled = false;
            }
            else
            {
                _requiredText.text = $"{0}/{_requiredCost[_sceneStartLevel]}";
                _upgradeText.text = $"{0}/{_upgradeCost[_sceneStartLevel]}";
            }
        }

        private IEnumerator ShowTutorial()
        {
            _tutorialPanel.SetActive(true);

            yield return new WaitForSeconds(5f);

            _tutorialPanel.SetActive(false);
        }

        private void Update()
        {
            if (_base.waterDrops > 0 && Vector2.Distance(transform.position, _player.position) < 2f)
            {
                _spaceImage.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    AddWater();
                }
            }
            else
            {
                _spaceImage.SetActive(false);
            }
        }

        private void AddWater()
        {
            if (_currentRequired < _requiredCost[_sceneStartLevel])
            {
                _currentRequired++;
                _base.waterDrops--;

                _requiredImage.fillAmount = (float) _currentRequired / _requiredCost[_sceneStartLevel];
                _requiredText.text = $"{_currentRequired}/{_requiredCost[_sceneStartLevel]}";
            }
            else if (CurrentLevel < 11)
            {
                _currentUpgrade++;
                _base.waterDrops--;

                if (_currentUpgrade >= _upgradeCost[CurrentLevel])
                {
                    _currentUpgrade = 0;
                    CurrentLevel++;

                    _sr.sprite = _plantSprites[CurrentLevel];
                }

                if (CurrentLevel >= 11)
                {
                    _upgradeImage.enabled = false;
                    _upgradeText.enabled = false;
                }
                else
                {
                    _upgradeImage.fillAmount = (float) _currentUpgrade / _upgradeCost[CurrentLevel];
                    _upgradeText.text = $"{_currentUpgrade}/{_upgradeCost[CurrentLevel]}";
                }
            }
        }

        public void CheckDeath()
        {
            if (_currentRequired < _requiredCost[_sceneStartLevel] && CurrentLevel > 0)
            {
                CurrentLevel--;
            }
        }
    }
}
