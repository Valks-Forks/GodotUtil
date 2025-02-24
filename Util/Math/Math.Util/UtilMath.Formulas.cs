﻿using System.Runtime.CompilerServices;

namespace Util;
public static partial class UtilMath
{

    #region Percent

    [MethodImpl(INLINE)] public static float ValuePercent(float value, float max) => max != 0f ? value / max : 0f;
    [MethodImpl(INLINE)] public static float ValuePercent(float value, float max, float min) => max != min ? (value - min) / (max - min) : 0f;

    #endregion

    #region Interpolation & Remapping

    [MethodImpl(INLINE)] public static float Lerp(float from, float to, float weight) => from + (to - from) * weight;
    [MethodImpl(INLINE)] public static float LerpClamped(float from, float to, float weight) => Math.Clamp(from + (to - from) * weight, 0f, 1f);

    [MethodImpl(INLINE)] public static float InverseLerp(float from, float to, float weight) => (weight - from) / (to - from);
    [MethodImpl(INLINE)] public static float InverseLerpClamped(float from, float to, float value) => Math.Clamp((value - from) / (to - from), 0f, 1f);
    [MethodImpl(INLINE)] public static float Remap(float fromMin, float fromMax, float toMin, float toMax, float value) => Lerp(toMin, toMax, InverseLerp(fromMin, fromMax, value));
    [MethodImpl(INLINE)] public static float RemapClamped(float fromMin, float fromMax, float toMin, float toMax, float value) => Lerp(toMin, toMax, InverseLerpClamped(fromMin, fromMax, value));

    public static float Smoothstep(float min, float max, float value)
    {
        if (value < min)
            return 0f;
        if (value >= max)
            return 1f;
        var t = (value - min) / (max - min);
        return t * t * (3f - 2f * t);
    }
    public static float Smootherstep(float min, float max, float value)
    {
        var x = Math.Clamp((value - min) / (max - min), 0f, 1f);
        return x * x * x * (x * (x * 6f - 15f) + 10f);
    }

    #endregion

    #region Average
    // https://math.stackexchange.com/questions/22348/how-to-add-and-subtract-values-from-an-average
    // size = number of elements that already exist

    [MethodImpl(INLINE)] public static float AddToAverage(float average, float newValue, int newSize) => 
        average + (newValue - average) / newSize;
    [MethodImpl(INLINE)] public static float SubtractFromAverage(float average, float newValue, int newSize) => 
        average + (-newValue - average) / newSize;
    [MethodImpl(INLINE)] public static float ReplaceInAverage(float average, int size, float oldValue, float newValue) => 
        (size * average - oldValue + newValue) / size;
    [MethodImpl(INLINE)] public static float MergeAverages(float average1, int size1, float average2, int size2) => 
        (size1 * average1 + size2 * average2) / (size1 + size2);

    #endregion

}