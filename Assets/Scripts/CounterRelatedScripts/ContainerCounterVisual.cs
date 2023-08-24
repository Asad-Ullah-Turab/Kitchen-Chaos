using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    Animator animator;
    [SerializeField] ContainerCounter containerCounter;

    private const string OPEN_CLOSE = "OpenClose";

    private void Start()
    {
        animator = GetComponent<Animator>();
        containerCounter.ObjectSpawnedEvent += PlayAnimation;
    }

    public void PlayAnimation(object obj, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }

}
