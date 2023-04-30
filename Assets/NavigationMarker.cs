using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationMarker : MonoBehaviour
{
    public Player player;

    private void Update()
    {
        transform.position = player.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, 360f - Statics.Maths.GetAngleFromVectorDirection((player.currentGoal.transform.position - player.transform.position).normalized));
    }
}