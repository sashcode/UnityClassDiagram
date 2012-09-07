using UnityEngine;
using System.Collections;

[System.Serializable]
public class ClassDiagramRoot : DiagramRoot
{
	public Rect buttonRect = new Rect (10, 30, 20, 20);
	public GUIStyle iconStyle = new GUIStyle (GUIStyle.none);

	
	public ClassNode[] classes;
	
	public ClassDiagramRoot ()
	{
		DiagramNode node1 = new DiagramNode ();
		node1.attributes.Add(new Attribute());
		node1.attributes.Add(new Attribute());
		node1.attributes.Add(new Attribute());
		DiagramNode node2 = new DiagramNode ();
		DiagramNode node3 = new DiagramNode ();
		DiagramEdge edge1 = new DiagramEdge ();
		
		edge1.SetSource(node1);
		edge1.SetTarget(node2);
		
		
		node1.edges.Add(edge1);
		nodes.Add(node1);
		nodes.Add(node2);
		nodes.Add(node3);
		
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
	
	override public void Draw(DiagramContext context ){
		iconStyle.normal.background = Config.TEX_TOOL_COMPOSITE;
		iconStyle.active.background = Config.TEX_TOOL_REFERENCE;
		if (GUI.Button (buttonRect, "", iconStyle)) {
			context.GetCommand().AddNode();
		}
		base.Draw(context);
	
	}
}
