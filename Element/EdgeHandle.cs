using UnityEngine;
using System.Collections;

public class EdgeHandle : DiagramHandle
{
	private static Rect RECT_TEMP = new Rect ();
	public const int HALF_SIZE = 3;
	public Vector2 position = new Vector2 ();
	
	public EdgeHandle ()
	{
	}
	
	public void Draw (DiagramContext context)
	{
		GUI.Box (GetRect (), "");
	}
	
	public Rect GetRect ()
	{
		RECT_TEMP.width = 2 * HALF_SIZE;
		RECT_TEMP.height = 2 * HALF_SIZE;
		RECT_TEMP.x = position.x - HALF_SIZE;
		RECT_TEMP.y = position.y - HALF_SIZE;
		return RECT_TEMP;
	}
	
	override public DiagramDragTracker GetDragTracker ()
	{
		return null;
	}
	
	override  public DiagramElement HitTest (DiagramContext context, Vector2 position)
	{
		if (DiagramUtil.ExpandRect (GetRect (), 6, 6).Contains (position)) {
			return this;
		}
		return null;
	}
	
}
