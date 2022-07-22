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
        // 1. ���� Primary ��ư�� ������
        if(Input.GetMouseButtonDown(0))
        {
            // 2. �� ������ ����
            Ray mouseRay = mainCam.ScreenPointToRay(Input.mousePosition);

            // 2-1. ���̾� ����ũ ���
            LayerMask targetLayer = LayerMask.NameToLayer("Ground");
            RaycastHit hit;
            if(Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit, 100f))
            {
                if(hit.collider.gameObject.layer != targetLayer.value)
                {
                    return;
                }

                // 3. �� ���� �������� ��ȭ
                Vector3 spawnPosition = hit.point;
                spawnPosition.y = 0.5f;
                GameObject item = Instantiate(ItemPrefab, spawnPosition, Quaternion.identity);
                Destroy(item, 5f);
            }
        }
    }
}
