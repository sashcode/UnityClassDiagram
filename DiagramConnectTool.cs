using UnityEngine;
using System.Collections;

public class DiagramConnectTool : DiagramDefaultTool
{
	public override bool OnGUI (DiagramContext context)
	{
	
		Event e = Event.current;
		switch (e.type) {
			
		case EventType.KeyDown:
			{
				Debug.Log ("key down  " + e.keyCode);
			if(e.keyCode == KeyCode.Escape){
				Debug.Log  ("key down !!!! " + e.keyCode);
				context.editor.GetRoot().SetDefaultTool();
				context.editor.Repaint();
				
			}
				break;
			}
		case EventType.KeyUp:
			{
				break;
			}
	
		}
		
		return true;
	}
}
