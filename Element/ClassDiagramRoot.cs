using UnityEngine;
using System.Collections;

[System.Serializable]
public class ClassDiagramRoot : DiagramRoot
{
	public ClassNode[] classes;
	
	public ClassDiagramRoot ()
	{
		ClassNode node1 = new ClassNode ();
		ClassNode node2 = new ClassNode ();
		ClassNode node3 = new ClassNode ();
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
			if (clazz.id != null && 0 < clazz.id.Length && clazz.id == classId) {
				return clazz;
			}
		}
		return null;
	}
}
