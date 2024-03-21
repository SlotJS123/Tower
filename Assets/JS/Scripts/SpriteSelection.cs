using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpriteSelection : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 마우스 왼쪽 버튼을 클릭하면 레이캐스트 발사
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 레이캐스트로 오브젝트 충돌 확인
            if (Physics.Raycast(ray, out hit))
            {
                // 충돌한 오브젝트가 스프라이트인지 확인
                Tile spriteRenderer = hit.collider.GetComponent<Tile>();

                if (EventSystem.current.IsPointerOverGameObject() == true)
                {
                    return;
                }

                if (spriteRenderer.isWall == false && GameManager.Instance.mapManager.towerSetState == true)
                {
                    return;
                }

                if (spriteRenderer != null)
                {
                    // 스프라이트 오브젝트를 클릭한 경우
                    Debug.Log("Sprite Object Selected: " + hit.collider.gameObject.name);

                    spriteRenderer.TouchTile();
                }
                else
                {
                    // 스프라이트가 아닌 다른 오브젝트를 클릭한 경우
                    Debug.Log("Other Object Selected: " + hit.collider.gameObject.name);
                }
            }
        }
    }
}