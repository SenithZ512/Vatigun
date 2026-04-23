using UnityEngine;

public static class AnimationExtend 
{
    public static void SafeSetTrigger(this Animator anim, string name)
    {
        if (anim != null) anim.SetTrigger(name);
    }

    public static void SafeSetFloat(this Animator anim, string name, float value)
    {
        if (anim != null) anim.SetFloat(name, value);
    }
    public static void SafeSetBool(this Animator anim, string name, bool value)
    {
        if (anim != null) anim.SetBool(name, value);
    }
}
