using UnityEngine;
using System.Collections;

[System.Serializable]
public class ClassDiagramData : MonoBehaviour
{
	public string diagramName;
	public Class[] classes;
	public string[] types ;
	
	public ClassDiagramData ()
	{
		classes = new Class [1];
		classes [0] = new Class ();
		diagramName = "diagram name";
		types = new string[]{"int", "float", "string", "bool", "Color", "Vector2", "Vector3", "Vector4", "AnimationCurve","Rect", "Texture", "Texture2D", "Object"};
	}
	
	public Class GetClass (string classId)
	{
		for (int index = 0; index < classes.Length; index++) {
			Class clazz = (Class)classes.GetValue (index);
			if (clazz.id != null && 0 < clazz.id.Length && clazz.id == classId) {
				return clazz;
			}
		}
		return null;
	}
}

[System.Serializable]
public class Class : DiagramNode
{
	public string name;
	public string id;
	public string iconPath;
	public Attribute[] attributes = new Attribute[0];
	public CompositeReference[] compositeReferences = new CompositeReference[0];
	public Reference[] references = new Reference[0];
	public Generalization[] generalizations = new Generalization[0];
	
	public string superClassName = null;

	public Class ()
	{
		name = "class name";
		id = System.Guid.NewGuid ().ToString ("N");
	}

	public Class GetSuperClass (ClassDiagramData data)
	{
		for (int index = 0; index < data.classes.Length; index++) {
			Class clazz = (Class)data.classes.GetValue (index);
			if (clazz.name != null && 0 < clazz.name.Length && clazz.name == superClassName) {
				return clazz;
			}
		}
		return null;
	}
}
	
[System.Serializable]
public class Reference :DiagramConnection
{
	public string name;
	public string  classId;
	[System.NonSerialized]
	public Class destClass;

}
[System.Serializable]
public class CompositeReference:DiagramConnection
{
	public string name;
	public string  classId;
	[System.NonSerialized]
	public Class destClass;
}
[System.Serializable]
public class Generalization:DiagramConnection
{
	public string name;
	public string  classId;
	[System.NonSerialized]
	public Class destClass;	
}

[System.Serializable]
public class Attribute
{
	public string name = "name";
	public string type = "int";
	public string iconPath;
}

[System.Serializable]
public class DiagramNode
{
	public Rect rect = new Rect (50, 100, 180, 30);
	
}

public class DiagramConnection
{
	[System.NonSerialized]
	public Texture2D targetAnchor = null;
	[System.NonSerialized]
	public Texture2D sourceAnchor = null;
}