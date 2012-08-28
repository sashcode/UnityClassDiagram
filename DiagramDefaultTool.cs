using UnityEngine;
using System.Collections;

public class DiagramDefaultTool : DiagramTool
{
	private DiagramDragTracker dragTracker;

	public void log (string text)
	{
		//Debug.Log (text);
	}
	
	public override bool OnGUI (DiagramContext context)
	{
	
		Event e = Event.current;
		switch (e.type) {
			
		case EventType.KeyDown:
			{
				log ("key down");
				break;
			}
		case EventType.KeyUp:
			{
				log ("key up");
				
				break;
			}
		case EventType.ContextClick:
			{
				log ("ContextClick");
				break;
			}
		case EventType.DragExited:
			{
				log ("DragExited");
				break;
			}
		case EventType.DragPerform:
			{
				log ("DragPerform");
				break;
			}
		case EventType.DragUpdated:
			{
				log ("DragUpdated");
				break;
			}
		case EventType.ExecuteCommand:
			{
				log ("ExecuteCommand");
				break;
			}
		case EventType.Ignore:
			{
				log ("Ignore");
				break;
			}
		case EventType.Layout:
			{
				log ("Layout");
				break;
			}
		case EventType.MouseDown:
			{
				log ("MouseDown " + Event.current.mousePosition);
				context.DragStart (Event.current.mousePosition);
				Select (context, Event.current.mousePosition);
				context.editor.Repaint ();
				break;
			}
		case EventType.MouseDrag:
			{
				context.Drag (Event.current.mousePosition);
				log ("MouseDrag");
				if (dragTracker != null) {
					dragTracker.OnDrag (context, Event.current.mousePosition);
					context.editor.Repaint ();
				}
				break;
			}
		case EventType.MouseUp:
			{
				context.DragEnd (Event.current.mousePosition);
				log ("MouseUp");
				break;
			}
		case EventType.Repaint:
			{
				log ("Repaint");
			
				return true;
			}
		case EventType.ScrollWheel:
			{
				log ("ScrollWheel");
				break;
			}
		case EventType.Used:
			{
				log ("Used");
				break;
			}
		case EventType.ValidateCommand:
			{
				log ("ValidateCommand");
				break;
			}
		case EventType.MouseMove:
			{
				log ("MouseMove");
				//Repaint ();
				break;
			}
		}
		
		return true;
		
		//currentDiagramRoot.OnGUI();	
	}
	
	public virtual void Select (DiagramContext context, Vector2 position)
	{
		DiagramRoot root = context.editor.GetRoot ();
		
		DiagramElement hit = null;
		
		//Handle Hit Test
		foreach (DiagramElement element in context.GetSelection().GetElements()) {
			DiagramHandle[] handles = element.GetHandles ();
			if (handles != null) {
				foreach (DiagramHandle handle in handles) {
					hit = handle.HitTest (context, position);
					if (hit != null) {
						Debug.Log(" handle hit !!");
						break;
					}
				}
			}
		}
		
		if (hit == null) {
			//Node Hit Test
			foreach (DiagramNode node in root.nodes) {
				hit = node.HitTest (context, position);
				if (hit != null) {
					break;
				}
			}
		}
		
		//Edge Hit Test
		if (hit == null) {
			foreach (DiagramNode node in root.nodes) {
				foreach (DiagramEdge edge in node.edges) {
					hit = edge.HitTest (context, position);
					if (hit != null) {
						break;
					}	
				}
			}
		}
		
		//Diagram Hit Test (canvas)
		if (hit == null) {
			hit = root.HitTest (context, position);
		}
		
		
		if (hit is DiagramRoot) {
			//No Select
			context.GetSelection ().Clear ();
			dragTracker = null;
			return;
		} 
		
		dragTracker = hit.GetDragTracker ();
			
		// Modifirers check
		EventModifiers mod = Event.current.modifiers;
		bool selectAdd = mod.Equals (EventModifiers.Shift);
			
		if (hit is DiagramHandle) {
				
		} else {
			if (selectAdd) {
				context.GetSelection ().AddElement (hit);	
			} else {
				context.GetSelection ().SetElement (hit);	
			}
				
		}
	}
}
