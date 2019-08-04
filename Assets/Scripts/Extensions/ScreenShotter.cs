#if (UNITY_EDITOR)

using System.IO;
using UnityEngine;

public class ScreenShotter : MonoBehaviour
{
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            DirectoryInfo di = new DirectoryInfo("Screenshots");
            
            int index = di.GetFiles().Length + 1;
            string path = $"Screenshots/screen_{index}.png";
            
            ScreenCapture.CaptureScreenshot(path);
            
            Debug.Log($"Screenshot {index} saved to \"{path}\".");
        }
    }
}

#endif
