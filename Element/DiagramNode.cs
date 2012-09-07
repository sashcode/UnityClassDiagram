using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DiagramNode:DiagramSelectableElement
{
	public List<DiagramEdge> edges = new List<DiagramEdge> ();
	public int typeId = Config.NODE_TYPE_CLASS;
	
	public List<Attribute> attributes = new List<Attribute>();
	public string name = "class name";
	public string iconPath;
	public Texture2D texIcon ;
	public GUIStyle iconStyle = new GUIStyle (GUIStyle.none);
	
	public Rect rect = new Rect (50, 100, 180, 30);
	[System.NonSerialized]
	private NodeHandle[] handles = new NodeHandle[8];
	
	public string uuid;
	
	public DiagramNode(){
		for(int i = 0 ; i < 8 ; i++){
			handles[i] = new NodeHandle(this , i);
		}
		uuid = System.Guid.NewGuid ().ToString ("N");
	}
	
	override public DiagramHandle[] GetHandles(){
		return handles;
	}
	
	override public void Draw(DiagramContext context){
		
	}
	
	public virtual void DrawNode (DiagramContext context)
	{ 
		NodeAdapter adapter = context.GetNodeAdapter(typeId);
		adapter.DrawNode(this , context);
	}
	
	public DiagramElement HitTest(DiagramContext context,Vector2 position){
		//Debug.Log(" node log  " + position);
		if(rect.Contains(position)){
			return this;
		}
		NodeAdapter adapter = context.GetNodeAdapter(typeId);
		if(adapter.HitTest(this , context , position)){
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

[System.Serializable]
public class Attribute
{
	public string name = "name";
	public string type = "type";
	public string iconPath;
}