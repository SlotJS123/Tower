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
        // 마우스 왼쪽 버튼을 클릭하면 레이캐스트 발사
        if (Input.GetMouseButtonDown(0))
        {

            // 카메라의 위치와 방향 설정
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 targetPosition = new Vector3(0f, 0f, 0f); // 원하는 기준점으로 수정할 것
            Vector3 cameraDirection = targetPosition - cameraPosition;

            // 마우스 클릭 지점에서의 스크린 좌표
            Vector3 mousePosition = Input.mousePosition;

            // 마우스 클릭 지점에서의 스크린 좌표를 월드 좌표로 변환
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

            // 카메라에서 마우스 클릭 지점을 향하는 레이 생성
            Ray ray = new Ray(cameraPosition, mousePositionInWorld - cameraPosition);
            RaycastHit hit;
            //Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

            // 레이캐스트로 오브젝트 충돌 확인
            if (Physics.Raycast(ray, out hit))
            {
                // 충돌한 오브젝트가 스프라이트인지 확인
                Tile spriteRenderer = hit.collider.GetComponent<Tile>();

                if(spriteRenderer == null)
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