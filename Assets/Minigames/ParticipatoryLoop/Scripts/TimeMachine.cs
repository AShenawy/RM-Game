using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeMachine : MonoBehaviour
{
    public GameManager manager;
    public Text turnCounter;

    private void OnEnable()
    {
        turnCounter.text = manager.currentTurn.ToString("D2");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
