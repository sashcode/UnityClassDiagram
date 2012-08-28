using UnityEngine;
using System.Collections;

public class NodeHandle : DiagramHandle
{
	public const int POS_TOP_LEFT = 0;
	public  const int POS_TOP = 1;
	public  const int POS_TOP_RIGHT = 2;
	public  const int POS_LEFT = 3;
	public  const int POS_RIGHT = 4;
	public  const int POS_BOTTOM_LEFT = 5;
	public  const int POS_BOTTOM = 6;
	public  const int POS_BOTTOM_RIGHT = 7;
	private static Rect RECT_TEMP = new Rect ();
	private int pos = -1;
	private int half_size = 3;
	private DiagramNode node;
	
	public int GetPosition(){
		return pos;
	}
	
	public NodeHandle (DiagramNode node, int  position)
	{
		this.node = node;
		pos = position;
	}
	
	public void Draw (DiagramContext context)
	{
		GUI.Box (GetRect(), "");
	}
	
	private Rect GetRect ()
	{
		
		RECT_TEMP.width = 2 * half_size;
		RECT_TEMP.height = 2 * half_size;
		switch (pos) {
		case POS_TOP_LEFT:{
				RECT_TEMP.x = node.rect.x - half_size;
				RECT_TEMP.y = node.rect.y - half_size;
				break;
			}
		case POS_TOP:{
				RECT_TEMP.x = node.rect.x + (node.rect.width / 2) - half_size;
				RECT_TEMP.y = node.rect.y - half_size;
				break;
			}
			
		case POS_TOP_RIGHT:{
				RECT_TEMP.x = node.rect.x + node.rect.width - half_size;
				RECT_TEMP.y = node.rect.y - half_size;
				break;
			}
		case POS_LEFT:{
				RECT_TEMP.x = node.rect.x - half_size;
				RECT_TEMP.y = node.rect.y + (node.rect.height / 2) - half_size;
				break;
			}
		case POS_RIGHT:{
				RECT_TEMP.x = node.rect.x + node.rect.width - half_size;
				RECT_TEMP.y = node.rect.y + (node.rect.height / 2) - half_size;
				break;
			}
		case POS_BOTTOM_LEFT:{
				RECT_TEMP.x = node.rect.x - half_size;
				RECT_TEMP.y = node.rect.y + node.rect.height - half_size;
				break;
			}
		case POS_BOTTOM:{
				RECT_TEMP.x = node.rect.x + (node.rect.width / 2) - half_size;
				RECT_TEMP.y = node.rect.y + node.rect.height - half_size;
				break;
			}
		case POS_BOTTOM_RIGHT:{
				RECT_TEMP.x = node.rect.x + node.rect.width - half_size;
				RECT_TEMP.y = node.rect.y + node.rect.height - half_size;
				break;
			}
		
		}
		return RECT_TEMP;
	}
	
	
	override public DiagramDragTracker GetDragTracker(){
		return new NodeResizeDragTracker(this);
	}
	
	
	override  public DiagramElement HitTest(DiagramContext context , Vector2 position){
		Debug.Log(" handle hit test " + position);
		if(GetRect ().Contains(position)){
			return this;
		}
		return null;
	}
	
}
