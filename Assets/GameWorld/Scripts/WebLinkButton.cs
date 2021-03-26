using UnityEngine;

namespace Methodyca.Core
{
    public class WebLinkButton : MonoBehaviour
    {
        public string url = "http://dlg.tlu.ee/methodyca/";

        public void OpenURL()
        {
            Application.OpenURL(url);
        }
    }
}