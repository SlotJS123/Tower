using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{
    [SerializeField]
    private Button cancelButton;
    [SerializeField]
    private Button selectButton;
    private Text stageNameText;
    private Image stageImage;

    void Start()
    {
        stageNameText = GetComponentInChildren<Text>();
        stageImage = transform.GetChild(1).GetComponent<Image>();

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(() => OnClickCancelButton());

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => OnClickCancelButton());
    }

    private void OnClickCancelButton()
    {
        this.gameObject.SetActive(false);
    }

}
