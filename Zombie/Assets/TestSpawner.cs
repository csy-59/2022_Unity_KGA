using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        // 1. 마우 Primary 버튼을 누르면
        if(Input.GetMouseButtonDown(0))
        {
            // 2. 그 지점을 얻어내서
            Ray mouseRay = mainCam.ScreenPointToRay(Input.mousePosition);

            // 2-1. 레이어 마스크 얻기
            LayerMask targetLayer = LayerMask.NameToLayer("Ground");
            RaycastHit hit;
            if(Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit, 100f))
            {
                if(hit.collider.gameObject.layer != targetLayer.value)
                {
                    return;
                }

                // 3. 땅 위에 아이템을 소화
                Vector3 spawnPosition = hit.point;
                spawnPosition.y = 0.5f;
                GameObject item = Instantiate(ItemPrefab, spawnPosition, Quaternion.identity);
                Destroy(item, 5f);
            }
        }
    }
}
