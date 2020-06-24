using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public VisitorType Type;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI CountText;

    public byte Count { get; private set; }

    void Start() => Game.instance.OnRecorded += Game_OnRecorded;

    void Game_OnRecorded(byte day)
    {
        TitleText.text = string.Format($"{day}. Day Visitor List");
        CountText.text = "0";
        Count = 0;
    }

    public void Increase()
    {
        if (Count >= 15)
            return;

        Count++;
        CountText.text = Count.ToString();
    }

    public void Decrease()
    {
        if (Count < 1)
            return;

        Count--;
        CountText.text = Count.ToString();
    }

    public void GetTotalNumber() => CountText.text = Game.instance.Visitors[Type].ToString();
}