using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public Quest CurrentQuest;

    [SerializeField] GameObject QuestUI;

    [SerializeField] TextMeshProUGUI QuestTitle;
    [SerializeField] TextMeshProUGUI QuestDescription;

    [SerializeField] TextMeshProUGUI RewardText;
    [SerializeField] Image RewardSprite;

    [SerializeField] Image IsTutorialImage;

    public KeyCode HideShowKey;

    [SerializeField] TextMeshProUGUI HideShow;
    [SerializeField] string HideShowText;

    private void Start()
    {
        HideShow.text = string.Format(HideShowText, HideShowKey.ToString());
    }

    private void Update()
    {
        SetQuest(CurrentQuest);
        HideShowQuestUI();
    }

    public void SetQuest(Quest quest)
    {
        CurrentQuest.QuestState = QuestState.Disabled;

        CurrentQuest = quest;
        CurrentQuest.QuestState = QuestState.InProgress;

        QuestTitle.text = quest.QuestTitle;
        QuestDescription.text = quest.QuestDescription;

        RewardText.text = quest.RewardText;
        RewardSprite.sprite = quest.RewardSprite;

        if (quest.IsTutorial)
        { 
            IsTutorialImage.gameObject.SetActive(true); 
        }
        else 
        {
            IsTutorialImage.gameObject.SetActive(false); 
        }
    }

    void HideShowQuestUI()
    {
        if (Input.GetKeyDown(HideShowKey) && QuestUI.activeSelf)
        {
            QuestUI.SetActive(false);
        }
        else if (Input.GetKeyDown(HideShowKey))
        {
            QuestUI.SetActive(true);
        }
    }

}
