using UnityEngine;
using System.Collections;


[System.Serializable]
public class ClassNode : DiagramNode
{
	
	public string superClassName = null;

	public ClassNode ()
	{
		name = "class name";
		uuid = System.Guid.NewGuid ().ToString ("N");
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
	
