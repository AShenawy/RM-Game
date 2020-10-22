using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour
{
    public ActivityType activity;
    public int cost;
    public bool isHappening;


}

public enum ActivityType { Ideation, TargetAudience, StoryWorkshop, ArtWorkshop, SoundWorkshop}