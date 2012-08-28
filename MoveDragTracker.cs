using UnityEngine;
using System.Collections;

public class MoveDragTracker : DiagramDragTracker {
		
	override public  void OnDrag(DiagramContext context , Vector2 position){
		
		Debug.Log(" OnDrag!!! ");
		Vector2 delta = context.GetDragDelta();
		foreach(DiagramElement element in context.GetSelection().GetElements()){
			if(element is DiagramNode){
				DiagramNode node = (DiagramNode)element;
				node.rect.x += delta.x;
				node.rect.y += delta.y;
			}
		}
		
	}
}
