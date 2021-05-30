using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    [System.Serializable]
    public class DialogueGraphStartNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        public DialogueGraphStartNodeSaveData(string uniqueID, string title, Rect rectPos, List<Port> inputPorts, List<Port> outputPorts, DialogueGraphView graphView) : base(uniqueID, title, rectPos, inputPorts, outputPorts, graphView)
        {
        }
    }
}