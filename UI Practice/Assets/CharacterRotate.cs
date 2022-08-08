using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotate : MonoBehaviour
{
    private bool IsSelected;

    private void Start()
    {
        
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

        while(true)
        {
            // 결과 값이 false면 대기
            yield return new WaitUntil(() => IsSelected == true);
            // 결과 값이 true면 대기
            yield return new WaitWhile(() => IsSelected == false);

            deltaScale = Mathf.Clamp(deltaScale * Time.deltaTime * 2f, originalScale, originalScale + 1f);
            transform.localScale = new Vector3(deltaScale, deltaScale, deltaScale);

        }
    }
}
