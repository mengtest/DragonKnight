﻿using RPG.DialogueSystem.Graph;
using RPG.QuestSystem;
using UnityEngine;

namespace DialogueSystem.Graph
{
    public class DialogueHasQuestConditionHandler : DialogueBaseConditionHandler
    {
        public bool HandleCondition(ScriptableObject conditionSO, GameObject obj)
        {
            return PlayerQuestManager.Instance.HasQuest(conditionSO as QuestSO);
        }
    }
}