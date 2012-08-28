using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DiagramNode:DiagramSelectableElement
{
	public List<DiagramEdge> edges = new List<DiagramEdge> ();
	public Rect rect = new Rect (50, 100, 180, 30);
	[System.NonSerialized]
	private NodeHandle[] handles = new NodeHandle[8];
	
	public DiagramNode(){
		for(int i = 0 ; i < 8 ; i++){
			handles[i] = new NodeHandle(this , i);
		}
	}
	
	override public DiagramHandle[] GetHandles(){
		return handles;
	}
	
	public void Draw(DiagramContext context){
		
		
	}
	
	public virtual void DrawNode (DiagramContext context)
	{ 
		GUI.Box (rect, "");
	}
	
	public DiagramElement HitTest(DiagramContext context,Vector2 position){
		//Debug.Log(" node log  " + position);
		if(rect.Contains(position)){
			return this;
		}
		return null;
	}
	
	
 	public virtual void DrawEdge (DiagramContext context)
	{

		foreach (DiagramEdge edge in edges) {
			edge.Draw (context);
		}
	}
	
	override public  void DrawHandle(DiagramContext context){
		
		foreach(NodeHandle handle in  handles){
			handle.Draw(context);
		}
	}
	
	override public DiagramDragTracker GetDragTracker(){
		return new MoveDragTracker();
	}
}
