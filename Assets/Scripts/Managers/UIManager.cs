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
    [SerializeField]
    private Button optionButton;

    private GameSpeedState gameSpeed = GameSpeedState.Default;

    // 게임 속도 관리 변수
    public GameSpeedState GameSpeed
    {
        get { return gameSpeed; }
        set
        {
            gameSpeed = value;

            switch (gameSpeed)
            {
                case GameSpeedState.Default:
                    Time.timeScale = 1;
                    break;

                case GameSpeedState.X2:
                    Time.timeScale = 2;
                    break;

                case GameSpeedState.X4:
                    Time.timeScale = 4;
                    break;

                case GameSpeedState.Pause:
                    Time.timeScale = 0;
                    break;
            }
        }
    }

    void Start()
    {
        speedButton.onClick.RemoveAllListeners();
        optionButton.onClick.RemoveAllListeners();

        speedButton.onClick.AddListener(() => { OnClickSpeedButton(); });
        optionButton.onClick.AddListener(() => { OnClickOptionButton(); });
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
