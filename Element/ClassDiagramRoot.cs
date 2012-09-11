using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ClassDiagramRoot : DiagramRoot
{
	
	public Rect buttonRect = new Rect (10, 30, 20, 20);
	public GUIStyle iconStyle = new GUIStyle (GUIStyle.none);
	public GUIStyle iconStyle1 = new GUIStyle (GUIStyle.none);
	public GUIStyle iconStyle2 = new GUIStyle (GUIStyle.none);
	public ClassNode[] classes;
	[System.NonSerialized]	
	private DiagramToolBar bar ;

	override public  DiagramTool GetTool (DiagramContext context)
	{
		return bar.GetActiveTool().GetTool ();
	}
	override public void SetDefaultTool(){
		bar.SetTool(0);
	}
	
	public ClassDiagramRoot ()
	{
		bar = new DiagramToolBar ();
		bar.position.x = 6;
		bar.position.y = 60;
		bar.Add (new DiagramToolElement (bar, Config.TEX_TOOL_COMPOSITE, Config.TEX_TOOL_REFERENCE, new DiagramDefaultTool ()));
		bar.Add (new DiagramToolElement (bar, Config.TEX_TOOL_COMPOSITE, Config.TEX_TOOL_REFERENCE, new DiagramConnectTool ()));
		bar.Add (new DiagramToolElement (bar, Config.TEX_TOOL_COMPOSITE, Config.TEX_TOOL_REFERENCE, new DiagramConnectTool ()));
		bar.Add (new DiagramToolElement (bar, Config.TEX_TOOL_COMPOSITE, Config.TEX_TOOL_REFERENCE, new DiagramConnectTool ()));
		
		DiagramNode node1 = new DiagramNode ();
		node1.attributes.Add (new Attribute ());
		node1.attributes.Add (new Attribute ());
		node1.attributes.Add (new Attribute ());
		DiagramNode node2 = new DiagramNode ();
		DiagramNode node3 = new DiagramNode ();
		DiagramEdge edge1 = new DiagramEdge ();
		
		edge1.SetSource (node1);
		edge1.SetTarget (node2);
		
		
		node1.edges.Add (edge1);
		nodes.Add (node1);
		nodes.Add (node2);
		nodes.Add (node3);
		
		classes = new ClassNode [1];
		classes [0] = new ClassNode ();
		diagramName = "diagram name";
		types = new string[]{"int", "float", "string", "bool", "Color", "Vector2", "Vector3", "Vector4", "AnimationCurve","Rect", "Texture", "Texture2D", "Object"};
	}
	
	public ClassNode GetClassNode (string classId)
	{
		for (int index = 0; index < classes.Length; index++) {
			ClassNode clazz = (ClassNode)classes.GetValue (index);
			if (clazz.uuid != null && 0 < clazz.uuid.Length && clazz.uuid == classId) {
				return clazz;
			}
		}
		return null;
	}
	
	override public void Draw (DiagramContext context)
	{
		bar.Draw (context);
		
		base.Draw (context);
	
	}
}

class DiagramToolBar
{
	public List<DiagramToolElement>  tools = new List<DiagramToolElement> ();
	public DiagramToolElement defaultTool;
	public Vector2 position = new Vector2 ();
	
	public void SetTool(int index){
		for(int i = 0 ; i< tools.Count ; i++ ){
			DiagramToolElement element = tools[i];
			if(i == index){
				element.active = true;
			}else{
				element.active = false;
			}
		}
	}
	
	public DiagramToolElement GetActiveTool(){
		foreach (DiagramToolElement tool in tools) {
			if (tool.active) {
				return tool;
			} 
		}
		defaultTool.active = true;
		return defaultTool;
	}
	
	public void Draw (DiagramContext context)
	{
		Vector2 positionTemp = new Vector2 ();
		positionTemp.x = position.x;
		positionTemp.y = position.y;
		DiagramToolElement activeTool = null;
		foreach (DiagramToolElement tool in tools) {
			tool.Draw (positionTemp);
			positionTemp.x += tool.rect.width;
		}
	}
	
	public void Add (DiagramToolElement  element)
	{
		if (defaultTool == null) {
			defaultTool = element;
		}
		tools.Add (element);
	}
	
	public void SelectionChanged (DiagramToolElement toolElement)
	{
		foreach (DiagramToolElement tool in tools) {
			if (toolElement != tool) {
				tool.active = false;
			} 
		}
	}
	
}

class DiagramToolElement
{
	public GUIStyle iconStyle = new GUIStyle (GUIStyle.none);
	public Texture2D texActive;
	public Texture2D texNormal;
	public DiagramTool tool;
	public bool active = false;
	public Rect rect = new Rect (0, 0, 16, 16);
	private DiagramToolBar bar;
	public int index;
	
	public DiagramToolElement (DiagramToolBar bar, Texture2D texNormal, Texture2D texActive, DiagramTool tool)
	{
		this.bar = bar;
		this.index = index;
		this.texActive = texActive;
		this.texNormal = texNormal;
		this.tool = tool;
	}
	
	public bool Draw (Vector2 position)
	{
		rect.x = position.x;
		rect.y = position.y;
		bool oldActive = active;
		if (active) {
			iconStyle.normal.background = texActive;
		} else {
			iconStyle.normal.background = texNormal;	
		}
		bool newState = GUI.Toggle (rect, active, "", iconStyle);
		if (oldActive != newState && newState) {
			active = newState;
			bar.SelectionChanged (this);
		}
		return false;
	}
	
	public DiagramTool GetTool ()
	{
		return tool;
	}
}
