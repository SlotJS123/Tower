using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameSpeedState
{
    Default,
    X2,
    X4,
    Pause
}


public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [SerializeField]
    private Button speedButton;
    [SerializeField]
    private Button optionButton;
    [SerializeField]
    private GameObject setTowerBoard;

    private Button[] setTowerBoardButtons;
    private Text[] setTowerBoardButtonTexts = new Text[2];
    private Text setTowerBoardText;
    private Text speedButtonText;
    private GameSpeedState gameSpeed;

    public static UIManager Instance => instance;

    // 게임 속도 관리 프로퍼티
    public GameSpeedState GameSpeed
    {
        get { return gameSpeed; }
        set
        {
            gameSpeed = value;

            switch (gameSpeed)
            {
                case GameSpeedState.Default:
                    speedButtonText.text = $"Speed{"\n"}X1";
                    Time.timeScale = 1;
                    break;

                case GameSpeedState.X2:
                    speedButtonText.text = $"Speed{"\n"}X2";
                    Time.timeScale = 2;
                    break;

                case GameSpeedState.X4:
                    speedButtonText.text = $"Speed{"\n"}X4";
                    Time.timeScale = 4;
                    break;

                case GameSpeedState.Pause:
                    Time.timeScale = 0;
                    break;
            }
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }

        Destroy(this.gameObject);
    }

    private void Start()
    {
        // 차후 Init 함수로 정리할것
        speedButtonText = speedButton.GetComponentInChildren<Text>();
        setTowerBoardButtons = setTowerBoard.GetComponentsInChildren<Button>();
        setTowerBoardText = setTowerBoard.GetComponentInChildren<Text>();

        for (int i = 0; i < setTowerBoardButtonTexts.Length; i++)
        {
            setTowerBoardButtonTexts[i] = setTowerBoardButtons[i].GetComponentInChildren<Text>();
        }

        speedButton.onClick.RemoveAllListeners();
        optionButton.onClick.RemoveAllListeners();

        speedButton.onClick.AddListener(() => { OnClickSpeedButton(); });
        optionButton.onClick.AddListener(() => { OnClickOptionButton(); });

        GameSpeed = GameSpeedState.Default;
    }

    private void Update()
    {
        // ChangeTimerText();
    }

    // 타워 설치할 자리 터치시 실행 함수
    public void OnClickEmptyZone()
    {
        setTowerBoard.SetActive(true);
        foreach (Button btn in setTowerBoardButtons)
            btn.onClick.RemoveAllListeners();

        setTowerBoardButtons[0].onClick.AddListener(() => 
        { 
            GameManager.Instance.MakeTower();
            DisableTowerBoard();
        } );
        setTowerBoardButtons[1].onClick.AddListener(() => DisableTowerBoard());

        setTowerBoardText.text = "타워를 설치하시겠습니까?";
        setTowerBoardButtonTexts[0].text = "설치하기";
        setTowerBoardButtonTexts[1].text = "취소하기";
    }

    // 타워 터치시 실행 함수
    public void OnClickTower()
    {
        // TODO
    }

    public void SetEnergyText(int _energy)
    {

    }

    public void SetHeartText(int _heart)
    {

    }

    public void SetWaveText(int _wave)
    {

    }

    private void ChangeTimerText()
    {

    }

    private void OnClickSpeedButton()
    {
        int nowState = (int)GameSpeed;
        nowState++;

        if (nowState == (int)GameSpeedState.Pause)
            nowState = (int)GameSpeedState.Default;

        GameSpeed = (GameSpeedState)nowState;
    }

    private void OnClickOptionButton()
    {
        print("Click Option Button");
        // TODO
    }

    private void DisableTowerBoard()
    {
        foreach (Button btn in setTowerBoardButtons)
            btn.onClick.RemoveAllListeners();

        GameManager.Instance.selectTile = null;
        setTowerBoard.SetActive(false);
    }

}
