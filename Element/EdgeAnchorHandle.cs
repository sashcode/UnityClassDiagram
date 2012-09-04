using UnityEngine;
using System.Collections;

[System.Serializable]
public class EdgeAnchorHandle  : EdgeHandle
{
	[System.NonSerialized]
	public DiagramNode node;
	public string nodeId;
	public static Vector2 CENTER = new Vector2(0.5f,0.5f);
	public Vector2 relativePosition = new Vector2(0.5f,0.5f);
	public bool relative = false;
	
	public DiagramNode GetNode (DiagramContext context)
	{
		if (node == null) {
			node = context.FindNode (nodeId);
		}
		return node;
	}
	
	override public DiagramDragTracker GetDragTracker ()
	{
		return new EdgeAnchorHandleDragTracker (this);
	}
	
}

public class EdgeAnchorHandleDragTracker : DiagramDragTracker
{
	private EdgeAnchorHandle anchor ;

	public EdgeAnchorHandleDragTracker (EdgeAnchorHandle anchor)
	{
		this.anchor = anchor;
	}

	override public  void OnDrag (DiagramContext context, Vector2 position)
	{

		anchor.relative = true;
		Vector2 delta = context.GetDragDelta ();
		anchor.position.x += delta.x;
		anchor.position.y += delta.y;
	}
	
	override public  void OnDrop (DiagramContext context, Vector2 position)
	{
		anchor.relative = false;
		DiagramNode targetNode = null;
		anchor.relativePosition = new Vector2(0.5f,0.5f);
		foreach (DiagramNode node in context.editor.GetRoot().nodes) {
			DiagramElement element = node.HitTest (context, position);
			if (element is DiagramNode) {
				targetNode = (DiagramNode)element;
			}
		}
	    
		if (targetNode != null) {
			if (targetNode != anchor.GetNode (context)) {
				anchor.node = targetNode;
				anchor.nodeId = targetNode.uuid;
			}else{
				anchor.relativePosition.x = (position.x - targetNode.rect.x) /targetNode.rect.width;
				anchor.relativePosition.y = (position.y - targetNode.rect.y) /targetNode.rect.height;
				Debug.Log(anchor.relativePosition);
			}
		}
	}
}