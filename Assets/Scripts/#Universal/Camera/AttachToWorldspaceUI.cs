using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToWorldspaceUI : MonoBehaviour
{
    public bool maintainWorldspacePosition = false;
    public bool permanentAddition = false;

    private void Start()
    {
        Statics.WorldspaceUI.AttachToUI(gameObject, maintainWorldspacePosition, permanentAddition);

        Destroy(this);
    }
}
