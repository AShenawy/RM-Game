using UnityEngine;
using Methodyca.Core;

public class SoundTester : MonoBehaviour
{
    public Sound soundData;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SoundManager.instance.PlaySFXOneShot(soundData);
    }
}
