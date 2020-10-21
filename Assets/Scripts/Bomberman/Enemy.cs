using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float movementDelay =1f;
    [SerializeField] private LayerMask raycastMask;
    private Vector2 _currentDir;

    private void Start()
    {
        switch (enemyType)
        {
            case EnemyType.Horizontal:
                _currentDir=Vector2.right;
                break;
            case EnemyType.Vertical:
                _currentDir=Vector2.up;
                break;
            default:
                break;
        }

        StartCoroutine(LoopedMove());
    }

    private IEnumerator LoopedMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(movementDelay);
            
            Move(_currentDir);
        }
    }

    private void Move(Vector2 dir)
    {
        if (Raycast(dir))
        {
            _currentDir = dir * -1;
            return;
        }

        var pos = (Vector2) transform.position + dir;
        transform.DOJump(pos, 0.3f, 1, 0.2f);
    }
    
    private bool Raycast(Vector2 dir)
    {
        var hit = Physics2D.Raycast(transform.position, dir, 1f, raycastMask);
        return hit.collider != null;
    }

}

public enum EnemyType
{
    Horizontal,
    Vertical
}
