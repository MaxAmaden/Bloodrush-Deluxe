using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Curves
{
    public static AnimationCurve linear;

    public static AnimationCurve smooth;

    public static AnimationCurve slowStartFastEnd;
    public static AnimationCurve fastStartSlowEnd;

    public static AnimationCurve overshoot_Large;
    public static AnimationCurve overshoot_Medium;
    public static AnimationCurve overshoot_Small;

    public static AnimationCurve undershoot_Small;
    public static AnimationCurve undershoot_Medium;
    public static AnimationCurve undershoot_Large;

    public static AnimationCurve mound;

    public static AnimationCurve slideAndBounce;



    public enum Curve { Linear, Smooth, SlowStartFastEnd, FastStartSlowEnd, Overshoot_Small, Overshoot_Medium, Overshoot_Large, Undershoot_Small, Undershoot_Medium, Undershoot_Large, Mound, SlideAndBounce, None }

    public static Dictionary<Curve, AnimationCurve> dict_Curve__AnimationCurve = new Dictionary<Curve, AnimationCurve>();
    public static AnimationCurve GetCurve(Curve curve)
    {
        if (!dict_Curve__AnimationCurve.ContainsKey(curve))
        {
            Debug.LogError("[!] No dictionary definition for Curve '" + curve.ToString() + "'!");
            return linear;
        }
        else return dict_Curve__AnimationCurve[curve];
    }
}