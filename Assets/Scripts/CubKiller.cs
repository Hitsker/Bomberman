using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubKiller : MonoBehaviour
{
    [SerializeField] private Camera camera;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        var ray = camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit))
        {
            return;
        }

        var rig = hit.collider.attachedRigidbody;
        if (rig!=null)
        {
            var dir = (hit.point - transform.position).normalized * 50f;
            rig.AddForceAtPosition(dir, hit.point, ForceMode.Impulse);

            var tnt = rig.gameObject.GetComponent<TNT>();
            if (tnt != null) 
            {
                tnt.Badabumsss();
            }
        }
    }
}
