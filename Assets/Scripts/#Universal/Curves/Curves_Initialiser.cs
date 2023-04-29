using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curves_Initialiser : MonoBehaviour
{
    public AnimationCurve slowStartFastEnd;
    public AnimationCurve fastStartSlowEnd;
    public AnimationCurve mound;
    public AnimationCurve slideAndBounce;

    private void Awake()
    {
        // Set up animation curves and add to curve dictionary.
        Curves.linear = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        Curves.dict_Curve__AnimationCurve.Add(Curves.Curve.Linear, Curves.linear);


        Curves.smooth = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });
        Curves.dict_Curve__AnimationCurve.Add(Curves.Curve.Smooth, Curves.smooth);


        Curves.slowStartFastEnd = slowStartFastEnd;
        Curves.dict_Curve__AnimationCurve.Add(Curves.Curve.SlowStartFastEnd, Curves.slowStartFastEnd);

        Curves.fastStartSlowEnd = fastStartSlowEnd;
        Curves.dict_Curve__AnimationCurve.Add(Curves.Curve.FastStartSlowEnd, Curves.fastStartSlowEnd);


        Curves.overshoot_Large  = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(0.8f, 1.3f), new Keyframe(1f, 1f) });
        Curves.dict_Curve__AnimationCurve.Add(Curves.Curve.Overshoot_Large, Curves.overshoot_Large);

        Curves.overshoot_Medium = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(0.8f, 1.2f), new Keyframe(1f, 1f) });
        Curves.dict_Curve__AnimationCurve.Add(Curves.Curve.Overshoot_Medium, Curves.overshoot_Medium);

        Curves.overshoot_Small  = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(0.8f, 1.1f), new Keyframe(1f, 1f) });
        Curves.dict_Curve__AnimationCurve.Add(Curves.Curve.Overshoot_Small, Curves.overshoot_Small);


        Curves.undershoot_Large = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(0.4f, -0.3f), new Keyframe(1f, 1f) });
        Curves.dict_Curve__AnimationCurve.Add(Curves.Curve.Undershoot_Large, Curves.undershoot_Large);

        Curves.undershoot_Medium = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(0.4f, -0.2f), new Keyframe(1f, 1f) });
        Curves.dict_Curve__AnimationCurve.Add(Curves.Curve.Undershoot_Medium, Curves.undershoot_Medium);

        Curves.undershoot_Small = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(0.4f, -0.1f), new Keyframe(1f, 1f) });
        Curves.dict_Curve__AnimationCurve.Add(Curves.Curve.Undershoot_Small, Curves.undershoot_Small);

        Curves.mound = mound;
        Curves.dict_Curve__AnimationCurve.Add(Curves.Curve.Mound, Curves.mound);

        Curves.slideAndBounce = slideAndBounce;
        Curves.dict_Curve__AnimationCurve.Add(Curves.Curve.SlideAndBounce, Curves.slideAndBounce);
    }
}