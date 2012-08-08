using UnityEngine;
using System.Collections;

[System.Serializable]
public class ClassDiagramData : MonoBehaviour
{
	public string diagramName;
	public Class[] classes;

	public ClassDiagramData ()
	{
		classes = new Class [1];
		classes [0] = new Class ();
		diagramName = "diagram name";
	}
}

[System.Serializable]
public class Class : DiagramNode
{
	public string name;
	public string iconPath;
	public Attribute[] attributes = new Attribute[0];
	public string superClassName = "";
	public Reference[] references = new Reference[0];
	public CompositeReference[] compositeReferences;

	public Class ()
	{
		name = "class name";
		iconPath = "Assets/UnityClassDiagram/icons/sword.png";
	}

	public Class GetSuperClass (ClassDiagramData data)
	{
		for (int index = 0; index < data.classes.Length; index++) {
			Class clazz = (Class)data.classes.GetValue (index);
			if (clazz.name == superClassName) {
				return clazz;
			}
		}
		return null;
	}
}
	
[System.Serializable]
public class Reference
{
	public string name;
	public string  classId;
	[System.NonSerialized]
	public Class destClass;

	public Class GetDestClass (ClassDiagramData data)
	{
		for (int index = 0; index < data.classes.Length; index++) {
			
		}
		return null;
	}
	
}

public class CompositeReference
{
	public string name;
	public Class type;
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