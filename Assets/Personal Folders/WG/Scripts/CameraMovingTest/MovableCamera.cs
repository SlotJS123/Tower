using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovableCamera : MonoBehaviour
{
    private Transform mapRoot;
    private Camera mainCamera;

    private void LateUpdate()
    {
        UpdateCamera();
    }

    public void Init(Transform _mapRoot)
    {
        mapRoot = _mapRoot;

        this.gameObject.transform.position = new Vector3(mapRoot.position.x, 70, mapRoot.position.z);
        this.gameObject.transform.rotation = Quaternion.Euler(67.5f, 0, 0);

        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
        BoxCollider collider = mapRoot.GetComponent<BoxCollider>();
        confiner.m_BoundingVolume = collider;

        mainCamera = Camera.main;
    }

    // 차후 터치 지원되게 수정
    private void UpdateCamera()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            float horizontal = Input.GetAxis("Mouse X");
            float vertical = Input.GetAxis("Mouse Y");

            this.transform.position += new Vector3(-horizontal, 0, -vertical);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            this.transform.position = mainCamera.transform.position;
        }
#endif
    }
}
