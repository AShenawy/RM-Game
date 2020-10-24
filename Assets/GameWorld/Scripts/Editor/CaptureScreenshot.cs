using UnityEngine;
using UnityEditor;

namespace Methodyca.Core
{
    // this script allows capturing screenshots double the size of the game view window straight from the editor
    // it creates a new menu tab in the toolbar called Screenshot
    //  ##Important: Project folder MUST contain a folder called Screenshots for this to work. If it doesn't, make one
    public class CaptureScreenshot
    {
        static int counter = 0;

        [MenuItem("Screenshot/Capture")]
        static void TakeScreenshot()
        {
            ScreenCapture.CaptureScreenshot("Screenshots/screen_" + counter + ".png", 2);
            counter++;
        }
    }
}