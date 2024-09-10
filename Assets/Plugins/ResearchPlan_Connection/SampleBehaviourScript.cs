
using System.Runtime.InteropServices;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SavePlanTitle(string str);

    // Start is called before the first frame update
    void Start()
    {
        SavePlanTitle("My new title for research");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
