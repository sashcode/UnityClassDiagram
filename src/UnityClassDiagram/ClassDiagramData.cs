using UnityEngine;
using System.Collections;

[System.Serializable]
public class ClassDiagramData : MonoBehaviour {
	public string diagramName;
	public Class[] classes;
	public ClassDiagramData(){
		classes = new Class [1];
		classes[0] = new Class();
		diagramName = "diagram name";
		
	}
}

[System.Serializable]
public class Class : DiagramNode {
	public string name;
	public Attribute[] attributes = new Attribute[0];
	public Reference[] references= new Reference[0];
	public CompositeReference[] compositeReferences;
	public Class(){
		name = "class name";
	}
	
}

[System.Serializable]
public class Reference {
	public string name;
	public Class type;
}

public class CompositeReference {
	public string name;
	public Class type;
}


[System.Serializable]
public class Attribute {
	public string name = "name";
	public string type = "type";
}

[System.Serializable]
public class DiagramNode{
	public Rect rect = new Rect(50, 100, 150, 100);
	
}