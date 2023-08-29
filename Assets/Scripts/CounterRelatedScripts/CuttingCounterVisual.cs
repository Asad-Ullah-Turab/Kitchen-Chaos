using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    Animator animator;
    [SerializeField] CuttingCounter cuttingCounter;

    private const string CUT = "Cut";

    private void Start()
    {
        animator = GetComponent<Animator>();
        cuttingCounter.OnCut += PlayAnimation;
    }

    public void PlayAnimation(object obj, EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
