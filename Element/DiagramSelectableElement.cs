using UnityEngine;
using System.Collections;

public class DiagramSelectableElement : DiagramElement {
	
	
	
	public virtual void Draw(DiagramContext context){
		
	}
	public virtual DiagramElement HitTest(DiagramContext context , Vector2 position){
		return this;
	}
	public virtual void DrawHandle(DiagramContext context){
		
	}
	
	public virtual DiagramDragTracker GetDragTracker(){
		return null;
	}
	
	public virtual DiagramHandle[] GetHandles(){
		return null;
	}
	
	
}
