using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private GameObject[] childs;
    private int childCount;

    public void ShowUI()
    {
        //gameObject.SetActive(true);

        for(int i = 0; i < childCount; ++i)
        {
            childs[i].SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        //gameObject.SetActive(false);
        //GameManager.Instance.OnGameOver.AddListener(ShowUI);
        childCount = transform.childCount;
        childs = new GameObject[childCount];

        for(int i = 0; i < childCount; ++i)
        {
            childs[i] = transform.GetChild(i).gameObject;
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameOver.AddListener(ShowUI);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver.RemoveListener(ShowUI);
    }
}
