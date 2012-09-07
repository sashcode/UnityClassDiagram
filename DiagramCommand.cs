using UnityEngine;
using System.Collections.Generic;

public class DiagramCommand
{
	private DiagramContext context;
	
	public DiagramCommand (DiagramContext context)
	{
		this.context = context;
	}
	
	public void DeleteNode (DiagramNode node)
	{
		context.editor.GetRoot ().nodes.Remove (node);
		context.GetSelection ().RemoveElement (node);
	}
	
	public void AddNode ()
	{
		DiagramNode newNode = new DiagramNode ();
		
		Rect newRect = new Rect (newNode.rect);
		List<DiagramNode> nodes = context.editor.GetRoot().nodes;
		int size = nodes.Count;
		if (0 < size) {
			while (true) {
				bool exist = false;
				foreach (DiagramNode c in nodes) {
					if (c.rect.x == newRect.x && c.rect.y == newRect.y) {
						exist = true;
						break;
					}
				}
				if (exist) {
					newRect.x += 20;
					newRect.y += 20;
				} else {
					break;
				}
			}
		}
		newNode.rect = newRect;
		nodes.Add (newNode);
		
	}
	
	
	public void AddAttribute(DiagramNode node){
		Attribute attribute = new Attribute ();
		node.attributes.Add(attribute );
		
		
		
	}
	
	
	
	
	
}
