using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationsEvents : MonoBehaviour
{
    public Action OnAnimationEnd;

    public void AnimationEnd()
    {
        OnAnimationEnd?.Invoke();
    }
}