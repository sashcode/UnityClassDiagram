using UnityEngine;
using System.Collections;

[System.Serializable]
public class EdgeAnchorHandle  : EdgeHandle
{
	[System.NonSerialized]
	public DiagramNode node;
	public string nodeId;
	
	
	public DiagramNode GetNode(DiagramContext context){
		if(node == null){
			node = context.FindNode(nodeId);;
		}
		return node;
	}
	
	
	override public DiagramDragTracker GetDragTracker(){
		return null;
	}
	
}