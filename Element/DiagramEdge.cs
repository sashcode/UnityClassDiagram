using UnityEngine;
using System.Collections;

public class DiagramEdge : DiagramSelectableElement
{
	[System.NonSerialized]
	public Texture2D targetAnchor = null;
	[System.NonSerialized]
	public Texture2D sourceAnchor = null;
	
	public virtual void Draw(DiagramContext context){
		
	}
	public DiagramHandle[] GetHandles(){
		return null;
	}
	
	public DiagramElement HitTest(DiagramContext context,Vector2 position){
		
		return null;
	}
	
	public void DrawHandle(DiagramContext context){
		
	}
	public DiagramDragTracker GetDragTracker(){
		return null;
	}
}