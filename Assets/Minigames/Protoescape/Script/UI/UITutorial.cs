using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    public GameObject tutorialPrefab;
    public Button spawnButton;
    public Transform tutorialPanel;
    private GameObject spawnedObject;

    void Start()
    {
        spawnButton.onClick.AddListener(SpawnObject);
    }

    void SpawnObject()
    {
        if (spawnedObject == null)
        {
            spawnedObject = Instantiate(tutorialPrefab,tutorialPanel);
        }
    }
}

