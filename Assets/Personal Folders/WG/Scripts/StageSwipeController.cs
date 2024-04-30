using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSwipeController : MonoBehaviour
{
    [SerializeField]
    private Scrollbar scrollBar;
    [SerializeField]
    private Transform stageLayoutParant;
    [SerializeField]
    private Button[] moveButtons;

    private float swipeTime = 0.2f;      // page swipe 되는 시간
    private float swipeDistance = 50.0f; // page swipe 되기 위해 움직여야 하는 거리

    private float[] scrollPageValues;    // scroll bar의 value / page수 로 0 ~ 1까지의 값을 저장하여 저장한 위치로 이동하기 위한 배열
    private float valueDistance = 0;     // 페이지 간 거리
    private float startTouchX;
    private float endTouchX;
    private int currentPage = 0;
    private int maxPage = 0;
    private bool isSwipe = false;

    public Transform StageLayoutParant => stageLayoutParant;
    public int CurrentPage => currentPage;

    private void Awake()
    {
        scrollPageValues = new float[stageLayoutParant.childCount];

        valueDistance = 1f / (scrollPageValues.Length - 1f);

        for (int i = 0; i < scrollPageValues.Length; i++)
        {
            scrollPageValues[i] = valueDistance * i;
        }

        maxPage = stageLayoutParant.childCount;
    }

    private void Start()
    {
        SetScrollBarValue(0);

        for (int i = 0; i < moveButtons.Length; i++)
        {
            moveButtons[i].onClick.RemoveAllListeners();
        }

        moveButtons[0].onClick.AddListener(() => OnMoveStage(true));
        moveButtons[1].onClick.AddListener(() => OnMoveStage(false));
    }

    private void Update()
    {
        UpdateInput();
    }

    private void SetScrollBarValue(int index)
    {
        currentPage = index;
        scrollBar.value = scrollPageValues[index];
    }

    private void UpdateInput()
    {
        if (isSwipe == true)
            return;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            startTouchX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTouchX = Input.mousePosition.x;
            UpdateSwipe();
        }
#endif

        // 차후 안드로이드 빌드시 사용
//#if UNITY_ANDROID
        // if (Input.touchCount == 1)
        // {
        //     Touch touch = Input.GetTouch(0);
        // 
        //     if (touch.phase == TouchPhase.Began)
        //     {
        //         startTouchX = touch.position.x;
        //     }
        //     else if (touch.phase == TouchPhase.Ended)
        //     {
        //         endTouchX = touch.position.x;
        //         UpdateSwipe();
        //     }
        // }
//#endif
    }

    private void UpdateSwipe()
    {
        if (Mathf.Abs(startTouchX - endTouchX) < swipeDistance)
        {
            StartCoroutine(OnSwipeOneStep(currentPage));
            return;
        }

        bool isLeft = startTouchX < endTouchX ? true : false;

        OnMoveStage(isLeft);
        StartCoroutine(OnSwipeOneStep(currentPage));
    }

    // 좌우 화살표버튼 클릭 시
    private void OnMoveStage(bool isLeft)
    {
        if (isLeft)
        {
            if (currentPage == 0)
                return;

            currentPage--;
        }
        else
        {
            if (currentPage == maxPage - 1)
                return;

            currentPage++;
        }
        StartCoroutine(OnSwipeOneStep(currentPage));
    }

    // 이동 애니메이션 코루틴
    private IEnumerator OnSwipeOneStep(int index)
    {
        float start = scrollBar.value;
        float current = 0;
        float percent = 0;

        isSwipe = true;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / swipeTime;

            scrollBar.value = Mathf.Lerp(start, scrollPageValues[index], percent);

            yield return null;
        }

        isSwipe = false;
    }
}
