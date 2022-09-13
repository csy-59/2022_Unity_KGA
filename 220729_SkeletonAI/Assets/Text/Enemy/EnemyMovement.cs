using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float Speed = 8f;
    private Transform _target;

    public void MoveTo(Transform target)
    {
        if(target != null)
        {
            return;
        }

        this._target = target;

        StartCoroutine(MoveToHelper());
    }

    private IEnumerator MoveToHelper()
    {
        Debug.Assert(_target != null);

        while (true)
        {
            
            transform.LookAt(_target);

            transform.Translate(0f, 0f, Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _target.position) <= 1f)
            {
                _target = null;

                break;
            }

            yield return null;
        }

    }
}
