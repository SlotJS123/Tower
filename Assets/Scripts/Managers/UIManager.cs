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
    [SerializeField]
    private Button speedButton;
    private Text speedButtonText;
    [SerializeField]
    private Button optionButton;

    private GameSpeedState gameSpeed;

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

    private void Start()
    {
        speedButtonText = speedButton.GetComponentInChildren<Text>();

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
        // TODO
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

}
