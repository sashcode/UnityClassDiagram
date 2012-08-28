using UnityEngine;
using System.Collections;

public interface DiagramElement  {
	
	void Draw(DiagramContext context);
	
	DiagramElement HitTest(DiagramContext context ,Vector2 position);
	
	
	DiagramHandle[] GetHandles();
	
	void DrawHandle(DiagramContext context);
	
	
	DiagramDragTracker GetDragTracker();
	
}
