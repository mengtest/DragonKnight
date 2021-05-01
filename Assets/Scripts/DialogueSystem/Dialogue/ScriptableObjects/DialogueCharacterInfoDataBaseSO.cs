using System.Collections.Generic;
using UnityEngine;
namespace RPG.DialogueSystem
{

    [CreateAssetMenu(fileName = "New DialogueCharacterInfoDataBaseSO", menuName = "Dialogue System/DialogueCharacterInfoDataBaseSO")]
    public class DialogueCharacterInfoDataBaseSO : ScriptableObject
    {
        public DialogueCharacterInfoSO[] dialogueCharacterInfos;
        public Dictionary<DialogueCharacterInfoSO, GameObject> sceneCharacterInfoDic = new Dictionary<DialogueCharacterInfoSO, GameObject>();


        public void UpdateDateBaseID()
        {
            // 仓库序列化时赋值ID
            for (int i = 0; i < dialogueCharacterInfos.Length; i++)
            {
                dialogueCharacterInfos[i].id = i;
            }
            // 序列化场景NPC记录字典
            foreach (var sceneNPC in Transform.FindObjectsOfType<DialogueNPC>())
            {
                sceneCharacterInfoDic.Add(sceneNPC.NPCInfo, sceneNPC.gameObject);
            }
        }
    }
}