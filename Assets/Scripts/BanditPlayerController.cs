using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BanditPlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask raycastMask;
    [SerializeField] private LayerMask explosionMask;
    private bool isInMovement;

    private void Update()
    {
        if (isInMovement)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovePlayer(Vector2.left);
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovePlayer(Vector2.right);
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MovePlayer(Vector2.up);
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MovePlayer(Vector2.down);
        }

        if (Input.GetMouseButtonDown(0))
        {
            var obj = RaycastFromCamera();
            if (obj!= null && obj.CompareTag("Explosive"))
            {
                Destroy(obj);

                var colliders = Physics2D.OverlapCircleAll(obj.transform.position, 1f, explosionMask);
                foreach (var colldr in colliders)
                {
                    Destroy(colldr.gameObject);
                }
            }
            
            
        }
    }

    private void MovePlayer(Vector2 dir)
    {
        if (Raycast(dir))
        {
            return;
        }

        isInMovement = true;
        var pos = (Vector2) transform.position + dir;
        transform.DOJump(pos, 0.3f, 1, 0.2f).OnComplete(() =>
        {
            isInMovement = false;
        });
    }

    private bool Raycast(Vector2 dir)
    {
        var hit = Physics2D.Raycast(transform.position, dir, 1f, raycastMask);
        return hit.collider != null;
    }

    private GameObject RaycastFromCamera()
    {
        var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1f);

        return hit.collider ? hit.collider.gameObject : null;
    }
}
