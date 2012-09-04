using UnityEngine;
using System.Collections;

public abstract class DiagramDragTracker  {
	
	public virtual void OnDrag(DiagramContext context , Vector2 position){
		
	}
	
	public virtual void OnDrop(DiagramContext context , Vector2 position){
		
	}
}
