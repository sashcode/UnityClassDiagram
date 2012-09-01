using UnityEngine;
using System.Collections;

[System.Serializable]
public class ReferenceEdge :DiagramEdge
{
	public string name;
	public string  classId;
	[System.NonSerialized]
	public ClassNode destClass;
	

}

