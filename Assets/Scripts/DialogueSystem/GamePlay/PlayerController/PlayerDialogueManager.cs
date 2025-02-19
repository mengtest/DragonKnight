﻿using System;
using System.Collections.Generic;
using RPG.InputSystyem;
using RPG.Interact;
using RPG.Module;
using RPG.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem.Graph
{
    public class PlayerDialogueManager : BaseSingletonWithMono<PlayerDialogueManager>
    {
        private Dictionary<string, DialogueTreeNode> _cacheDic = new Dictionary<string, DialogueTreeNode>();
        private DialogueTreeNode _rootNode;
        private GameObject _currentObj;

        public void StartDialogue(DialogueNPC dialogueNpc)
        {
            // 获取根节点(StartNode)
            _rootNode = dialogueNpc.dialogueSO.GetDialogueTree();
            _cacheDic.Clear();
            _rootNode.Traverse(treeNode =>
            {
                string nodeUniqueID = treeNode.BaseNodeSaveData.UniqueID;
                if (!_cacheDic.ContainsKey(nodeUniqueID))
                {
                    _cacheDic.Add(treeNode.BaseNodeSaveData.UniqueID, treeNode);
                }
            });
            
            _currentObj = dialogueNpc.gameObject;
            
            // 关闭其他交互按钮的显示
            BaseUI.GetController<InteractionController>().HideAllButton();
            InputManager.Instance.inputData.CloseAllKeyInput(1);
            InputManager.Instance.CloseMouseInput();
            BaseUI.GetController<DialogueController>().Show();
            HandleTreeNode(_rootNode);
        }
        
        public void EndDialogue()
        {
            // 开启其他交互按钮的显示
            BaseUI.GetController<DialogueController>().Hide();
            InputManager.Instance.OpenMouseInput();
            InputManager.Instance.inputData.OpenAllKeyInput(1);
            BaseUI.GetController<InteractionController>().ShowAllButton();
        }
        
        private void HandleTreeNode(DialogueTreeNode startTreeNode)
        {
            if (startTreeNode != null && startTreeNode.BaseNodeSaveData.HandleData(startTreeNode, _currentObj))
            {
                foreach (DialogueTreeNode childTreeNode in startTreeNode.GetNextNodes())
                {
                    HandleTreeNode(childTreeNode);
                }
            }
        }

        /// <summary>
        /// 从指定的节点开始对话
        /// </summary>
        /// <param name="recordNodeUniqueID">指定的节点</param>
        public void ContinueDialogue(string recordNodeUniqueID)
        {
            DialogueTreeNode recordTreeNode = _cacheDic[recordNodeUniqueID];
            foreach (DialogueTreeNode childTreeNode in recordTreeNode.GetNextNodes())
            {
                HandleTreeNode(childTreeNode);
            }
        }
    }
}