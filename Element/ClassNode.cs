using UnityEngine;
using System.Collections;


[System.Serializable]
public class ClassNode : DiagramNode
{
	public string name;
	public string id;
	public string iconPath;
	public Attribute[] attributes = new Attribute[0];
	public  CompositeEdge[] compositeReferences = new  CompositeEdge[0];
	public ReferenceEdge[] references = new ReferenceEdge[0];
	public GeneralizationEdge[] generalizations = new GeneralizationEdge[0];
	
	public string superClassName = null;

	public ClassNode ()
	{
		name = "class name";
		id = System.Guid.NewGuid ().ToString ("N");
	}

	public ClassNode GetSuperClassNode (ClassDiagramRoot data)
	{
		for (int index = 0; index < data.classes.Length; index++) {
			ClassNode clazz = (ClassNode)data.classes.GetValue (index);
			if (clazz.name != null && 0 < clazz.name.Length && clazz.name == superClassName) {
				return clazz;
			}
		}
		return null;
	}
}
	


[System.Serializable]
public class Attribute
{
	public string name = "name";
	public string type = "int";
	public string iconPath;
}
