using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSibling : MonoBehaviour
{
    public enum Rank {First, Last}

    public Rank setRank;

    public void SetRank()
    {
        switch(setRank)
        {
            case Rank.First:
                GetComponent<RectTransform>().SetAsFirstSibling();
                break;

            case Rank.Last:
                GetComponent<RectTransform>().SetAsLastSibling();
                break;
        }
    }
}
