using UnityEngine;



public enum QuestState
{
    Disabled,
    Finished,
    InProgress
}

public enum QuestType
{
    ItemCollecting,
    MoveTo,
    Sell,
    Cut
}

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/New Quest")]
public class Quest : ScriptableObject
{
    public string QuestTitle = "Quest Title";
    public string QuestDescription = "Quest Description";
    
    public float XPReward = 0f;
    public string RewardText = "The reward is...";
    public Sprite RewardSprite;

    public QuestState QuestState = QuestState.Disabled;
    public bool IsTutorial = false;
}
