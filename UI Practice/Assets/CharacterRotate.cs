using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterRotate : MonoBehaviour
{
    public bool IsSelected;

    private void Start()
    {
        StartCoroutine(CoroutineSelectedEffect());
    }

    void Update()
    {
        if(IsSelected == false)
        {
            transform.Rotate(0f, 30f * Time.deltaTime, 0f);
        }
    }

    private IEnumerator CoroutineSelectedEffect()
    {
        float delay = 0f;
        float originalScale = transform.localScale.x;
        float deltaScale = originalScale;
        Debug.Log(deltaScale);

        while(true)
        {
            // ��� ���� false�� ���
            //yield return new WaitUntil(() => IsSelected == true);
            // ��� ���� true�� ���
            yield return new WaitWhile(() => IsSelected == false);

            deltaScale = Mathf.Clamp(deltaScale + Time.deltaTime * 2f, originalScale, originalScale + 1f);
            transform.localScale = new Vector3(deltaScale, deltaScale, deltaScale);
            delay += Time.deltaTime;

            if (transform.localScale.x == originalScale + 1f && delay > 2.5f)
            {
                break;
            }
        }

        SceneManager.LoadScene(0);
    }
}
