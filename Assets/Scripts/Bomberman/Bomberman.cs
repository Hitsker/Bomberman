using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomberman : MonoBehaviour
{
    [SerializeField] private LayerMask raycastMask;
    [SerializeField] private Bomb bombPrefab;
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

        if (Input.GetKeyDown(KeyCode.Space) && !isInMovement)
        {
            DropBomb();
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

    private void DropBomb()
    {
        Instantiate(bombPrefab, transform.position, Quaternion.identity);
    }
}
