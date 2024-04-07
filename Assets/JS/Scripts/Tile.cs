using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.UIElements;

public enum TileState
{
    On,  //타워 설치가 가능한 상태
    Off  //타워 설치가 불가능한 상태 
}
public class Tile : MonoBehaviour
{
    public UnityEngine.UI.Button touchButton;

    public Tile(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

    public bool isWall;
    public Tile ParentNode;

    public GameObject tile_1;
    public GameObject tile_2;


    // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
    public int x, y, G, H;
    public int F { get { return G + H; } }
    public TileState state;

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
    ///  Map Manager에서 map을 만들 때 tile 프리팹을 생성하며 가장 먼저 실행하기 될 함수입니다 
    /// </summary>
    /// <param name="_x">x 좌표값을 할당 받을지 몰라서 준비 </param>
    /// <param name="_y">y 좌표값을 할당 받을지 몰라서 준비 </param>
    public void Info(int _x, int _y)
    {
        x = _x; y = _y; 
        //일단 테스트용도로 On/Off로만 존재합니다 
        // On인경우 몬스터의 이동경로가 되며
        // Off인 경우 타워설치 경로가 됩니다 
        state = TileState.On;
        H = 190;
        G = 10;
        isWall = true;
        tile_2.gameObject.SetActive(false);
    }

    /// <summary>
    /// 버튼을 클릭했을 경우 실행되는 이벤트 함수입니다 
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
            Debug.Log("여기서 이제 타워를 생성해주는 로직을 생성해서 적용을 시켜야합니다 ");

            GameManager.Instance.towerManager.JS_TowerInstallation(this, GameManager.Instance.towerManager.GetUpTowerData());
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
