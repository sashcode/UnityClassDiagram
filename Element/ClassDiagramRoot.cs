using UnityEngine;
using System.Collections;

[System.Serializable]
public class ClassDiagramRoot : DiagramRoot
{
	public ClassNode[] classes;
	
	public ClassDiagramRoot ()
	{
		nodes.Add(new ClassNode ());
		
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
