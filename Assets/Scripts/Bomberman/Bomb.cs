using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float boomTime = 3f;
    [SerializeField] private float explosionRadius = 1f;
    [SerializeField] private LayerMask explosionMask;
    [Space] 
    [SerializeField] private GameObject _animationGroup1;
    [SerializeField] private GameObject _animationGroup2;
    [SerializeField] private GameObject _animationGroup3;

    private void Start()
    {
        StartCoroutine(DelayedExplode());
    }

    private IEnumerator DelayedExplode()
    {
        yield return new WaitForSeconds(boomTime);
        StartCoroutine(ExplosionAnimation());
    }

    private IEnumerator ExplosionAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        _animationGroup1.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _animationGroup2.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _animationGroup3.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Explode();
        Destroy(this.gameObject);
    }

    private void Explode()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, explosionMask);
        foreach (var colldr in colliders)
        {
            if (colldr.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(0);
            }
            Destroy(colldr.gameObject);
        }
    }
}
