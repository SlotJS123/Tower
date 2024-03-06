using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.UIElements;

public enum TileState
{
    On,  //Ÿ�� ��ġ�� ������ ����
    Off  //Ÿ�� ��ġ�� �Ұ����� ���� 
}
public class Tile : MonoBehaviour
{
    public UnityEngine.UI.Button touchButton;

    public Tile(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

    public bool isWall;
    public Tile ParentNode;

    public GameObject tile_1;
    public GameObject tile_2;


    // G : �������κ��� �̵��ߴ� �Ÿ�, H : |����|+|����| ��ֹ� �����Ͽ� ��ǥ������ �Ÿ�, F : G + H
    public int x, y, G, H;
    public int F { get { return G + H; } }
    TileState state;

    public Action<Tile> OnTileClick;    

    // Start is called before the first frame update
    void Start()
    {
        if(touchButton != null)
        touchButton.onClick.AddListener(TouchTile);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    ///  Map Manager���� map�� ���� �� tile �������� �����ϸ� ���� ���� �����ϱ� �� �Լ��Դϴ� 
    /// </summary>
    /// <param name="_x">x ��ǥ���� �Ҵ� ������ ���� �غ� </param>
    /// <param name="_y">y ��ǥ���� �Ҵ� ������ ���� �غ� </param>
    public void Info(int _x, int _y)
    {
        x = _x; y = _y; 
        //�ϴ� �׽�Ʈ�뵵�� On/Off�θ� �����մϴ� 
        // On�ΰ�� ������ �̵���ΰ� �Ǹ�
        // Off�� ��� Ÿ����ġ ��ΰ� �˴ϴ� 
        state = TileState.On;
        H = 190;
        G = 10;
        isWall = true;
        tile_2.gameObject.SetActive(false);
    }

    /// <summary>
    /// ��ư�� Ŭ������ ��� ����Ǵ� �̺�Ʈ �Լ��Դϴ� 
    /// </summary>
    public void TouchTile()
    {
        if (GameManager.Instance.mapManager.towerSetState == false)
        {

            state = TileState.Off;
            isWall = false;

            tile_1.gameObject.SetActive(false);
            tile_2.gameObject.SetActive(true);

            OnTileClick?.Invoke(this);
        }
        else
        {
            Debug.Log("���⼭ ���� Ÿ���� �������ִ� ������ �����ؼ� ������ ���Ѿ��մϴ� ");

            GameManager.Instance.towerSpawn.JS_TowerInstallation(this);
        }



    }

    public void TargetTile()
    {
        state = TileState.Off;
        //OnTileClick?.Invoke(this);
        isWall = false;

        tile_1.gameObject.SetActive(false);
        tile_2.gameObject.SetActive(true);
    }




    public void ResetData()
    {
        isWall = true;

        state = TileState.On;
        tile_1.gameObject.SetActive(true);
        tile_2.gameObject.SetActive(false);

    }
}
