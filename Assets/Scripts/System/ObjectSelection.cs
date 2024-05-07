using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelection
{
    private Camera mainCamera;
    private int pointerID; // PC와 Android 환경에서 터치와 클릭을 체크하기 위한 변수
    public float rayLength = 100f; // 레이케스트 길이
    public void Init()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        pointerID = -1;
// 차후 Android SDK 적용 후 아래 주석 해제할것
// #elif UNITY_ANDROID
//      pointerID = 0;
#endif
        mainCamera = Camera.main;
    }

    public void UpdateSelection()
    {
        // 마우스 왼쪽 버튼을 클릭하면 레이캐스트 발사
        if (Input.GetMouseButtonDown(0))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(pointerID))
            {
                return;
            }

            // 카메라의 위치와 방향 설정
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 targetPosition = new Vector3(0f, 0f, 0f); // 원하는 기준점으로 수정할 것
            // Vector3 cameraDirection = targetPosition - cameraPosition;

            // 마우스 클릭 지점에서의 스크린 좌표
            Vector3 mousePosition = Input.mousePosition;

            // 마우스 클릭 지점에서의 스크린 좌표를 월드 좌표로 변환
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

            // 카메라에서 마우스 클릭 지점을 향하는 레이 생성
            Ray ray = new Ray(cameraPosition, mousePositionInWorld - cameraPosition);
            RaycastHit hit;

            // 레이캐스트로 오브젝트 충돌 확인
            if (Physics.Raycast(ray, out hit))
            {
                IInteractionable interactionObject = hit.transform.GetComponent<IInteractionable>();

                if (interactionObject != null)
                {
                    CheckInteractableObject(interactionObject);
                }
            }
        }
    }

    private void CheckInteractableObject(IInteractionable interactionableObject)
    {
        interactionableObject.InteractionObject();
    }

    void OnDrawGizmos()
    {
        // 기즈모 색상 설정
        Gizmos.color = Color.red;

        // 현재 마우스 위치에서 레이 생성
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // 레이 길이까지 기즈모 그리기
        Gizmos.DrawRay(ray.origin, ray.direction * rayLength);
    }
}