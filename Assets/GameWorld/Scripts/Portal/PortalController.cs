﻿using UnityEngine;
using UnityEngine.Video;
using System.IO;


namespace Methodyca.Core
{
    [RequireComponent(typeof(PortalInteraction))]
    public class PortalController : MonoBehaviour
    {
        [Header("Crystal Parameters")]
        [SerializeField] private GameObject crystalQLSlot;
        [SerializeField] private GameObject crystalQNSlot;

        [Header("Swirl Effect")]
        //[SerializeField] private Sprite spriteQLOpaque;       //TODO remove redundant code
        //[SerializeField] private Sprite spriteQLTransparent;
        //[SerializeField] private Sprite spriteQNOpaque;
        //[SerializeField] private Sprite spriteQNTransparent;
        //[SerializeField] private SpriteRenderer swirlDisplayBot;
        //[SerializeField] private SpriteRenderer swirlDisplayTop;
        [SerializeField] private Animator animBot;
        [SerializeField] private Animator animTop;
        public Sound swirlSFX;

        [Header("Act 2 Scenes")]
        [SerializeField] private string Act2Scene;
        [SerializeField] private string Act2QLRoomName, Act2QNRoomName;
        [SerializeField] private bool skipTransitionVideo;


        private enum CrystalType { Quantitaive, Qualitative, None}
        private CrystalType lastPlacedCrystal;
        private bool isBothSlotsEmpty = true;
        private VideoPlayer videoPlayer;


        private void Start()
        {
            if (!skipTransitionVideo)
                PrepareVideo();
        }

        void PrepareVideo()
        {
            videoPlayer = GetComponent<VideoPlayer>();
            videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "portal-animate_lower-30fps_2MBPS_aud.mp4");
            // prepares the video file for quick playback when called
            videoPlayer.Prepare();
        }

        public void OnItemPlacement(Item crystal)
        {
            PlaceCrystalInSlot(crystal);
            lastPlacedCrystal = CheckCrystalType(crystal);
            DisplaySwirlFX();
            SoundManager.instance.PlaySFX(swirlSFX);
        }

        private CrystalType CheckCrystalType(Item crystal)
        {
            if (crystal.itemName == "Bright Purple Crystal")
                return CrystalType.Qualitative;
            else if (crystal.itemName == "Bright Blue Crystal")
                return CrystalType.Quantitaive;
            else
                return CrystalType.None;
        }

        private void PlaceCrystalInSlot(Item crystal)
        {
            CrystalType type = CheckCrystalType(crystal);

            switch (type)
            {
                case CrystalType.Qualitative:
                    GameObject crystalQL = Instantiate(crystal.prefabObject, crystalQLSlot.transform);
                    Destroy(crystalQL.GetComponent<ObjectInteraction>());
                    break;

                case CrystalType.Quantitaive:
                    GameObject crystalQN = Instantiate(crystal.prefabObject, crystalQNSlot.transform);
                    Destroy(crystalQN.GetComponent<ObjectInteraction>());
                    break;

                case CrystalType.None:
                    Debug.LogWarning("Placed item was neither \"Bright Purple Crystal\" nor \"Bright Blue Crystal\". Check names of crystal items used.");
                    break;

                default:
                    Debug.LogError("No case satisfied in PlaceCrystalInSlot(Item crystal) method. Check names of crystal items used.");
                    break;
            }
        }

        private void DisplaySwirlFX()
        {
            switch (lastPlacedCrystal)
            {
                case CrystalType.Qualitative:
                    if (isBothSlotsEmpty)
                    {
                        //swirlDisplayBot.sprite = spriteQLOpaque;
                        animBot.SetTrigger("OnPurpleCrystal");
                        isBothSlotsEmpty = false;
                    }
                    else
                    {
                        //swirlDisplayTop.sprite = spriteQLTransparent;
                        animTop.SetTrigger("OnPurpleCrystal");
                    }

                    break;

                case CrystalType.Quantitaive:
                    if (isBothSlotsEmpty)
                    {
                        //swirlDisplayBot.sprite = spriteQNOpaque;
                        animBot.SetTrigger("OnBlueCrystal");
                        isBothSlotsEmpty = false;
                    }
                    else
                    {
                        //swirlDisplayTop.sprite = spriteQNTransparent;
                        animTop.SetTrigger("OnBlueCrystal");
                    }

                    break;

                case CrystalType.None:
                    Debug.LogWarning("Could not display swirl sprite. Check names of crystal items used.");
                    break;

                default:
                    Debug.LogError("No case satisfied in DisplaySwirlFX() method. Check names of crystal items used.");
                    break;
            }
        }

        public void PlayTransition()
        {
            SoundManager.instance.StopSFX("swirl");
            SoundManager.instance.StopBGM();

            if (skipTransitionVideo)
                GoToNextLevel(null);
            else
            {
                videoPlayer.Play();
                videoPlayer.loopPointReached += GoToNextLevel;
            }
            
        }

        void GoToNextLevel(VideoPlayer player)
        {
            if (player != null)
                player.loopPointReached -= GoToNextLevel;

            switch (lastPlacedCrystal)
            {
                case CrystalType.Qualitative:
                    // can remove interaction states since going to a new scene
                    SceneManagerScript.instance.GoToLevel(Act2Scene, Act2QLRoomName, false);
                    break;

                case CrystalType.Quantitaive:
                    //videoPlayer.Play();   //TODO remove this line
                    // can remove interaction states since going to a new scene
                    SceneManagerScript.instance.GoToLevel(Act2Scene, Act2QNRoomName, false);
                    break;

                case CrystalType.None:
                    Debug.LogWarning("Could not load level. Check names of crystal items used.");
                    break;

                default:
                    Debug.LogError("No case satisfied in GoToNextLevel() method. Check names of crystal items used.");
                    break;
            }
        }
    }
}