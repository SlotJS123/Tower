using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private Queue<Vector3> route = new Queue<Vector3>();
    private Vector3 nextPoint;
    private bool isDead = false;
    private bool moveDone = false;
    protected int hp;
    protected int monsterId;
    protected float speed;

    private void Update()
    {
        //  나중에 오브젝트 풀로 반환하게 수정
        if (isDead)
            Destroy(this.gameObject);
    }

    public void StartMoving(List<Vector3> _route)
    {
        SetRoute(_route);
        StartCoroutine(MonsterMovingCoroutine());
    }

    private void SetRoute(List<Vector3> _route)
    {

        for (int i = 0; i < _route.Count; i++)
        {
            route.Enqueue(_route[i]);
        }
    }

    private IEnumerator MonsterMovingCoroutine()
    {
        int count = route.Count;
        for (int i = 0; i < count; i++)
        {
            nextPoint = route.Dequeue();
            StartCoroutine(Move());

            yield return new WaitUntil(() => moveDone);

            moveDone = false;
        }

        //  끝까지 도착
        isDead = true;
    }

    private IEnumerator Move()
    {
        var runTime = 0.0f;
        while (true)
        {
            runTime += Time.deltaTime* speed;

            transform.position = Vector3.Lerp(transform.position, nextPoint, runTime / 1);
            yield return null;

            if (Mathf.Abs((nextPoint - transform.position).magnitude) <= 0.05f)
            {
                moveDone = true;
                break;
            }
        }
    }
}
