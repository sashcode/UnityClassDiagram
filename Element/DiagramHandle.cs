using UnityEngine;
using System.Collections;

public class DiagramHandle : DiagramElement {
	
	
	public void Draw(DiagramContext context){
		
	}
	public DiagramElement HitTest(Vector2 position){
		return this;
	}
	public void DrawHandle(DiagramContext context){
		
	}
	
	public DiagramDragTracker GetDragTracker(){
		return null;
	}

}
