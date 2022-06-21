using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health = 10;
    [SerializeField] private float _dyingTime = 0.3f;

    private Animator _animator;
    private Coroutine _dying;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        StopCoroutine(_dying);
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        _animator.SetTrigger(AnimatorPlayerController.Params.Hurt);

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _dying = StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        var waitForSeconds = new WaitForSeconds(_dyingTime);

        _animator.SetTrigger(AnimatorPlayerController.Params.Death);
        yield return waitForSeconds;
        Destroy(gameObject);
    }
}
