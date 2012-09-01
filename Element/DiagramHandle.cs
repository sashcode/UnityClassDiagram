using UnityEngine;
using System.Collections;

public class DiagramHandle : DiagramElement {
	
	
	virtual public void Draw(DiagramContext context){
		
	}
	virtual  public DiagramElement HitTest(DiagramContext context , Vector2 position){
		return this;
	}
	virtual public void DrawHandle(DiagramContext context){
		
	}
	
	virtual public DiagramDragTracker GetDragTracker(){
		return null;
	}
	
	virtual public DiagramHandle[] GetHandles(){
		return null;
	}
	
}
