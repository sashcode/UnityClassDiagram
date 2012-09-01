using UnityEngine;
using System.Collections;

public class EdgeHandle : DiagramHandle
{
	public const float EDGE_HANDLE_WIDTH = 6;
	public const float EDGE_HANDLE_HEIGHT = 6;
	public Vector2 position =  new Vector2();
	
	public EdgeHandle ()
	{
	}
	
	
	public void Draw (DiagramContext context)
	{
		GUI.Box (GetRect(), "");
	}
	
	public Rect GetRect(){
		return new Rect(position.x, position.y  , EDGE_HANDLE_WIDTH , EDGE_HANDLE_HEIGHT);
	}
	
	
	override public DiagramDragTracker GetDragTracker(){
		return null;
	}
	
	
	override  public DiagramElement HitTest(DiagramContext context , Vector2 position){
		if(DiagramUtil.ExpandRect(GetRect () , 6 , 6).Contains(position)){
			return this;
		}
		return null;
	}
	
}
