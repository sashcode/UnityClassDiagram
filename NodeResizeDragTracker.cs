using UnityEngine;
using System.Collections;

public class NodeResizeDragTracker  : DiagramDragTracker
{
	private NodeHandle nodeHandle;

	public NodeResizeDragTracker (NodeHandle nodeHandle)
	{
		this.nodeHandle = nodeHandle;
	}

	
	public void Resize (ref Rect rect,float x, float y, float width, float height)
	{
		rect.width += width;
		rect.height += height;
		if(rect.width<10){
		rect.width = 10;	
		}else{
					rect.x += x;
		
		}
		if(rect.height<10){
		rect.height
				= 10;	
		}else{
			rect.y += y;
		}
	}
		
	override public  void OnDrag (DiagramContext context, Vector2 position)
	{
		int pos = nodeHandle.GetPosition ();
		Vector2 delta = context.GetDragDelta ();
		foreach (DiagramElement element in context.GetSelection().GetElements()) {
			if (element is DiagramNode) {
				DiagramNode node = (DiagramNode)element;
				Debug.Log( " resize delta !!! " + delta);
				Debug.Log( " resize !!! " + node.rect);
				switch (pos) {
				case NodeHandle.POS_TOP_LEFT:
					{
						Resize (ref node.rect,delta.x, delta.y, -delta.x, -delta.y);
						break;	
					}
				case NodeHandle.POS_TOP:{
						Resize (ref node.rect,0, delta.y, 0, -delta.y);
						break;	
					}
				case NodeHandle.POS_TOP_RIGHT:{
						Resize (ref node.rect, 0, delta.y , delta.x, -delta.y);
						break;	
					}
				case NodeHandle.POS_LEFT:{
						Resize (ref node.rect,delta.x, 0, -delta.x, 0);
						break;	
					}
				case NodeHandle.POS_RIGHT:{
					Resize (ref node.rect,0, 0, delta.x, 0);
						break;	
					}
				case NodeHandle.POS_BOTTOM_LEFT:{
					Resize (ref node.rect,delta.x, 0, -delta.x, delta.y);
						break;	
					}
				case NodeHandle.POS_BOTTOM:{
					Resize (ref node.rect,0, 0, 0, delta.y);
						break;	
					}
				case NodeHandle.POS_BOTTOM_RIGHT:{
					Resize (ref node.rect,0, 0, delta.x, delta.y);
						break;	
					}
				
			
				}
			}
		}
		
	}
}
