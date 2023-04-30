using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statics
{
    public static SFXController SFX;

    public static VFXController VFX;

    public static WorldspaceUI WorldspaceUI;

    public static CameraController CameraController;


    public static Collider2D screenBounds = null;

    public static float screenWidthAdjustment;


    public static bool isPaused = false;


    public static bool tutorialMode = true;


    private static List<MonoBehaviour> pauseRequests = new List<MonoBehaviour>();
    public static void Pause(MonoBehaviour script)
    {
        // Remove dead pause requests.
        ValidatePauseRequests();

        // Log pause request (one per MonoBehaviour).
        if (!pauseRequests.Contains(script)) pauseRequests.Add(script);

        // Check whether to pause.
        if (pauseRequests.Count > 0) isPaused = true;
    }
    public static void Unpause(MonoBehaviour script)
    {
        // Remove dead pause requests.
        ValidatePauseRequests();

        // Remove previously-logged pause request.
        if (pauseRequests.Contains(script)) pauseRequests.Remove(script);

        // Check whether to unpause.
        if (pauseRequests.Count <= 0) isPaused = false;
    }

    private static void ValidatePauseRequests()
    {
        // Remove dead pause requests.
        for (int i = 0; i < pauseRequests.Count; i++)
        {
            if (pauseRequests[i] == null)
            {
                pauseRequests.RemoveAt(i);
                i--;
            }
        }
    }


    public static class Maths
    {
        public static float GetAngleFromVectorDirection(Vector2 direction)
        {
            direction = direction.normalized;

            float angle = Vector3.Angle(new Vector3(0.0f, 1.0f, 0.0f), new Vector3(direction.x, direction.y, 0.0f));
            if (direction.x < 0.0f) angle = -angle + 360;

            return angle;
        }

    }
}