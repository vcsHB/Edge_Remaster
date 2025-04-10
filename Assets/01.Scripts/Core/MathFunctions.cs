using UnityEngine;

public static class MathFunctions
{

    public static float EaseOutExpo(float x)
    {

        return x >= 1f ? 1f : 1f - Mathf.Pow(2f, -10f * x);

    }

    public static float EaseInCubic(float x)
    {
        return Mathf.Clamp01(x * x * x);
    }

    public static float EaseInSine(float x)
    {
        return 1 - Mathf.Cos((x * Mathf.PI) / 2);
    }
}