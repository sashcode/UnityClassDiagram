using UnityEngine;
using System.Collections.Generic;

public class DiagramContext
{

	public DiagramEditorWindow editor;
	[System.NonSerialized]
	 public DiagramCommand command ;
	private DiagramSelection selection = new DiagramSelection ();
	
	public DiagramContext (DiagramEditorWindow editorWindow)
	{
		editor = editorWindow;
		command = new DiagramCommand(this);
		
	}
	
	public DiagramCommand GetCommand(){
		return command;
	}

	public DiagramNode FindNode (string id)
	{
		
		foreach (DiagramNode node in editor.GetRoot().nodes) {
			Debug.Log (" find Node !! " + node.uuid + "   " + id);
			if (node.uuid == id) {
				return node;	
			}
		}
		return null;
	}
	
	public EdgeAdapter GetEdgeAdapter (int typeId)
	{
		switch (typeId) {
		case Config.EDGE_TYPE_COMPOSITE:
			return new CompositeEdgeAdapter ();
		case Config.EDGE_TYPE_GENERALIZATION:
			return new GeneralizationEdgeAdapter ();
		case Config.EDGE_TYPE_REFERENCE:
			return new ReferenceEdgeAdapter ();
		}
		return null;
	}
	
	public NodeAdapter GetNodeAdapter (int typeId)
	{
		switch (typeId) {
		case Config.NODE_TYPE_CLASS:
			return new ClassNodeAdapter();
		}
		return null;
	}
	
	public DiagramSelection GetSelection ()
	{
		
		return selection;
	}

	public bool IsMainSelection (DiagramElement element)
	{
		return false;
	}
	
	private float dragOldX = 0;
	private float dragOldY = 0;
	private Vector2 dragDelta = new Vector2 ();
	
	public void DragStart (Vector2 position)
	{
		dragOldX = position.x;
		dragOldY = position.y;
		dragDelta.x = 0;
		dragDelta.y = 0;
	}

	public void Drag (Vector2 position)
	{
		dragDelta.x = position.x - dragOldX;
		dragDelta.y = position.y - dragOldY;
		dragOldX = position.x;
		dragOldY = position.y;
	}

	public void DragEnd (Vector2 position)
	{
		dragDelta.x = 0;
		dragDelta.y = 0;
		dragOldX = 0;
		dragOldY = 0;
	}

	public Vector2 GetDragDelta ()
	{
		return dragDelta;
	}
	
}

public class DiagramSelection
{
	
	private List<DiagramElement> elements = new List<DiagramElement> ();

	public void Clear ()
	{
		elements.Clear ();
	}

	public List<DiagramElement> GetElements ()
	{
		return new List<DiagramElement> (elements);
	}

	public List<DiagramElement> RemoveElement (DiagramElement element)
	{
		elements.Remove (element);
		return GetElements ();
	}
	
	public List<DiagramElement> SetElement (DiagramElement element)
	{
		elements.Clear ();
		elements.Add (element);
		return GetElements ();
	}

	public List<DiagramElement> AddElement (DiagramElement element)
	{
		elements.Add (element);
		return GetElements ();
	}
	
}