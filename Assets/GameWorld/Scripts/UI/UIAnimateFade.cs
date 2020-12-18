using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimateFade : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void AnimateIn()
    {
        anim.Play("Base Layer.Active & Fade In");
    }

    public void AnimateOut()
    {
        anim.Play("Base Layer.InActive & Fade Out");
    }
}
