﻿using UnityEngine;
using UnityEngine.SceneManagement;


namespace Methodyca.Core
{
    [RequireComponent(typeof(PortalInteraction))]
    public class PortalController : MonoBehaviour
    {
        [Header("Crystal Parameters")]
        [SerializeField] private GameObject crystalQLSlot;
        [SerializeField] private GameObject crystalQNSlot;

        [Header("Swirl Effect")]
        [SerializeField] private Sprite spriteQLOpaque;
        [SerializeField] private Sprite spriteQLTransparent;
        [SerializeField] private Sprite spriteQNOpaque;
        [SerializeField] private Sprite spriteQNTransparent;
        [SerializeField] private SpriteRenderer swirlDisplayBot;
        [SerializeField] private SpriteRenderer swirlDisplayTop;

        [Header("Act 2 Scenes")]
        [SerializeField] private string Act2Scene;
        [SerializeField] private string Act2QLRoomTag, Act2QNRoomTag;


        private enum CrystalType { Quantitaive, Qualitative, None}
        private CrystalType lastPlacedCrystal;
        private bool isBothSlotsEmpty = true;


        public void OnItemPlacement(Item crystal)
        {
            PlaceCrystalInSlot(crystal);
            lastPlacedCrystal = CheckCrystalType(crystal);
            DisplaySwirlFX();
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
                        swirlDisplayBot.sprite = spriteQLOpaque;
                        isBothSlotsEmpty = false;
                    }
                    else
                        swirlDisplayTop.sprite = spriteQLTransparent;
                    break;

                case CrystalType.Quantitaive:
                    if (isBothSlotsEmpty)
                    {
                        swirlDisplayBot.sprite = spriteQNOpaque;
                        isBothSlotsEmpty = false;
                    }
                    else
                        swirlDisplayTop.sprite = spriteQNTransparent;
                    break;

                case CrystalType.None:
                    Debug.LogWarning("Could not display swirl sprite. Check names of crystal items used.");
                    break;

                default:
                    Debug.LogError("No case satisfied in DisplaySwirlFX() method. Check names of crystal items used.");
                    break;
            }
        }

        public void GoToNextLevel()
        {
            switch (lastPlacedCrystal)
            {
                case CrystalType.Qualitative:
                    SceneManagerScript.instance.GoToLevel(Act2Scene, Act2QLRoomTag);
                    break;

                case CrystalType.Quantitaive:
                    SceneManagerScript.instance.GoToLevel(Act2Scene, Act2QNRoomTag);
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