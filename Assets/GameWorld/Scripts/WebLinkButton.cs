using UnityEngine;
using System.Runtime.InteropServices;

namespace Methodyca.Core
{
    public class WebLinkButton : MonoBehaviour
    {
        public string url = "http://dlg.tlu.ee/methodyca/";

        // JS interaction with browser to open URL in new window
        [DllImport("__Internal")]
        private static extern void OpenURLNewWindow(string url);

        // button click action
        public void OpenURL()
        {
            OpenURLNewWindow(url);

            // This method opens the url in same game window, effectively ending the game session
            //Application.OpenURL(url);
        }
    }
}