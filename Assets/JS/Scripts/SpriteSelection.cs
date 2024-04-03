using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpriteSelection : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // ���콺 ���� ��ư�� Ŭ���ϸ� ����ĳ��Ʈ �߻�
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ����ĳ��Ʈ�� ������Ʈ �浹 Ȯ��
            if (Physics.Raycast(ray, out hit))
            {
                // �浹�� ������Ʈ�� ��������Ʈ���� Ȯ��
                Tile spriteRenderer = hit.collider.GetComponent<Tile>();

                if(spriteRenderer.isWall == false && GameManager.Instance.mapManager.towerSetState == true)
                {
                    return;
                }

                if (spriteRenderer != null)
                {
                    // ��������Ʈ ������Ʈ�� Ŭ���� ���
                    Debug.Log("Sprite Object Selected: " + hit.collider.gameObject.name);

                    spriteRenderer.TouchTile();
                }
                else
                {
                    // ��������Ʈ�� �ƴ� �ٸ� ������Ʈ�� Ŭ���� ���
                    Debug.Log("Other Object Selected: " + hit.collider.gameObject.name);
                }
            }
        }
    }
}