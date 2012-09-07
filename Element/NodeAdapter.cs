using UnityEngine;
using System.Collections;

public class NodeAdapter
{
	virtual public void DrawNode (DiagramNode node , DiagramContext context)
	{
		
	}
	
	virtual public bool HitTest (DiagramNode node , DiagramContext context , Vector2 position)
	{
		return false;
	}
}
