using UnityEngine;
using System.Collections.Generic;

public class DiagramRoot : MonoBehaviour  , DiagramElement{
	
	public string diagramName;
	public Rect diagramNameRect = new Rect (10, 10, 200, 20);
	
	public List<DiagramNode> nodes = new List<DiagramNode> ();
	public string[] types ;
		
	virtual public void Draw(DiagramContext context){

		diagramName = GUI.TextField (diagramNameRect, diagramName);
		
		foreach(DiagramNode node in nodes){
			node.DrawNode(context);
		}
		
		foreach(DiagramNode node in nodes){
			node.DrawEdge(context);
		}
		
	}
	public DiagramDragTracker GetDragTracker(){
		return null;
	}
	
	public DiagramElement HitTest(DiagramContext context,Vector2 position){
		return this;
	}
	public void DrawHandle(DiagramContext context){
		
	}
	
	public DiagramHandle[] GetHandles(){
		return null;
	}
}
