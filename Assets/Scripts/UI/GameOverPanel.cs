using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameOverPanel : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
