using UnityEngine;
using System.Collections;

public interface DiagramElement  {
	
	void Draw(DiagramContext context);
	
	 DiagramElement HitTest(Vector2 position);
	
	void DrawHandle(DiagramContext context);
	
	DiagramDragTracker GetDragTracker();
	
}
