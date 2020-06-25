using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    private GameObject NPCimage;
    private Sprite activeSprite;
    private AudioClip npcAudioClip;
    private int currentComfortValue;
    public AudioSource npcAudioSource;
    private void SetNPCImage()
    {
        activeSprite = Resources.Load<Sprite>("Images/NPCs/Judy/judy" + NPCData.ReturnComfortValueAsString());
        NPCimage = GameObject.Find("NPCImg");
        NPCimage.GetComponent<UnityEngine.UI.Image>().sprite = activeSprite;
    }

    void Start()
    {
        SetNPCImage();
        currentComfortValue = NPCData.GetComfortValue();
    }

    void Update()
    {
        if(currentComfortValue != NPCData.GetComfortValue())
        {
            SetNPCImage();
            if (!Sound.muted)
            {
                if (NPCData.isComfortValPositive)
                {
                    System.Random r = new System.Random();
                    int randomInt = r.Next(1, 3); // range 1 - 2
                    npcAudioClip = Resources.Load<AudioClip>("Sounds/approving" + randomInt);
                }
                else
                {
                    npcAudioClip = Resources.Load<AudioClip>("Sounds/disapproving2");
                }
                npcAudioSource.clip = npcAudioClip;
                npcAudioSource.Play();
            }
        }
        currentComfortValue = NPCData.GetComfortValue();
    }
}
