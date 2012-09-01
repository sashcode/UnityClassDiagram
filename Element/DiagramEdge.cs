using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DiagramEdge : DiagramSelectableElement
{
	public Color lineColor = new Color (0.3f, 0.4f, 0.7f);
	private Texture2D lineTex;
	public List<EdgeHandle> handles = new List<EdgeHandle> ();
	public EdgeAnchorHandle sourceAnchor = new EdgeAnchorHandle ();
	public EdgeAnchorHandle targetAnchor = new EdgeAnchorHandle ();
	public string targetId;
	[System.NonSerialized]
	public static List<EdgeHandle> TEMP_HANDLES = new List<EdgeHandle> ();
	
	public void SetSource (DiagramNode node)
	{
		EdgeAnchorHandle handle = new EdgeAnchorHandle ();
		handle.nodeId = node.uuid;
		sourceAnchor = handle;
	}
	
	public void SetTarget (DiagramNode node)
	{
		EdgeAnchorHandle handle = new EdgeAnchorHandle ();
		handle.nodeId = node.uuid;
		targetAnchor = handle;
	}
	
	public virtual void Draw (DiagramContext context)
	{
			
		Rect wr = sourceAnchor.GetNode (context).rect;
		Rect wr2 = targetAnchor.GetNode (context).rect;
		Vector2 pointA = new Vector2 (wr.x + wr.width / 2, wr.y + wr.height / 2);
		Vector2 pointB = new Vector2 (wr2.x + wr2.width / 2, wr2.y + wr2.height / 2);
		sourceAnchor.position = GetAnchorPos (pointA, wr2);
		targetAnchor.position = GetAnchorPos (pointB, wr);
		
		TEMP_HANDLES.Clear ();
		TEMP_HANDLES.Add (sourceAnchor);
		TEMP_HANDLES.AddRange (handles);
		TEMP_HANDLES.Add (targetAnchor);
		
		for (int i = 0; i < TEMP_HANDLES.Count -1; i++) {
			EdgeHandle handle1 = TEMP_HANDLES [i];
			EdgeHandle handle2 = TEMP_HANDLES [i + 1];
			drawLine (handle1.position, handle2.position, lineColor, false, null, null);
		}
	}
	
	public DiagramHandle[] GetHandles ()
	{
		return null;
	}
	
	public DiagramElement HitTest (DiagramContext context, Vector2 position)
	{
		
		return null;
	}
	
	public void DrawHandle (DiagramContext context)
	{
		
	}

	public DiagramDragTracker GetDragTracker ()
	{
		return null;
	}
	
	void drawLine (Vector2 pointA, Vector2 pointB, Color color, bool mouse, Texture2D srcAnchor, Texture2D destAnchor)
	{
		float width = 2;
			
		Color savedColor = GUI.color;
		Matrix4x4 savedMatrix = GUI.matrix;
        
		width *= 3;
		if (!lineTex) {
			lineTex = new Texture2D (1, 3, TextureFormat.ARGB32, true);
			lineTex.SetPixel (0, 0, new Color (1, 1, 1, 0));
			lineTex.SetPixel (0, 1, Color.white);
			lineTex.SetPixel (0, 2, new Color (1, 1, 1, 0));
			lineTex.Apply ();
		}
		
		
		float angle = Vector3.Angle (pointB - pointA, Vector2.right) * (pointA.y <= pointB.y ? 1 : -1);
		//Debug.Log (" a=" + pointA + " b=" + pointB + " A=" + angle);
		
		float dx = pointB.x - pointA.x;
		float dy = pointB.y - pointA.y;
		float length = Mathf.Sqrt (dx * dx + dy * dy);
		GUI.color = color;
		GUI.matrix = Matrix4x4.TRS (new Vector3 (pointA.x, pointA.y, 0), Quaternion.identity, Vector3.one);
		GUIUtility.RotateAroundPivot (angle, pointA);
		
		GUI.DrawTexture (new Rect (0, 0, length, 1), lineTex);
		
		
		GUI.color = savedColor;
		
		if (destAnchor != null) {
			//DrawAnchor (pointB, angle, destAnchor);
		}
		if (srcAnchor != null) {
			//DrawAnchor (pointA, angle + 180, srcAnchor);
		}
		
		
		GUI.matrix = savedMatrix;
		GUI.color = savedColor;
	}
	
	Vector2 GetAnchorPos (Vector2 pointA, Rect wr2)
	{
		Vector2 pointB = new Vector2 (wr2.x + wr2.width / 2, wr2.y + wr2.height / 2);
		Vector2 arrowPos = new Vector2 ();
		float left = wr2.x;
		float right = wr2.x + wr2.width;
		float top = wr2.y;
		float bottom = wr2.y + wr2.height;
		
		Vector2 topLeft = new Vector2 (wr2.x, wr2.y);
		Vector2 topRight = new Vector2 (wr2.x + wr2.width, wr2.y);
		Vector2 bottomLeft = new Vector2 (wr2.x, wr2.y + wr2.height);
		Vector2 bottomRight = new Vector2 (wr2.x + wr2.width, wr2.y + wr2.height);
		
		float ans1 = calcPosition (topLeft, bottomRight, pointA);
		float ans2 = calcPosition (bottomLeft, topRight, pointA);
		
		
		if (0 <= ans1 && 0 <= ans2) {
			//Debug.Log("bottom");
			float w1 = Mathf.Abs (pointB.y - pointA.y);
			float w2 = Mathf.Abs (pointB.y - bottom);
			float h1 = pointB.x - pointA.x;
			float h2 = h1 * w2 / w1;
			arrowPos.y = bottom;
			arrowPos.x = pointB.x - h2;			
		} else if (0 > ans1 && 0 <= ans2) {
			//Debug.Log("right");
			float w1 = Mathf.Abs (pointB.x - pointA.x);
			float w2 = Mathf.Abs (pointB.x - right);
			float h1 = pointB.y - pointA.y;
			float h2 = h1 * w2 / w1;
			arrowPos.x = right;
			arrowPos.y = pointB.y - h2;
		} else if (0 <= ans1 && 0 > ans2) {
			//Debug.Log("left");
			float w1 = Mathf.Abs (pointB.x - pointA.x);
			float w2 = Mathf.Abs (pointB.x - left);
			float h1 = pointB.y - pointA.y;
			float h2 = h1 * w2 / w1;
			arrowPos.x = left;
			arrowPos.y = pointB.y - h2;
		} else if (0 > ans1 && 0 > ans2) {
			//Debug.Log("top");
			float w1 = Mathf.Abs (pointB.y - pointA.y);
			float w2 = Mathf.Abs (pointB.y - top);
			float h1 = pointB.x - pointA.x;
			float h2 = h1 * w2 / w1;
			arrowPos.y = top;
			arrowPos.x = pointB.x - h2;
		}
		return arrowPos;
	}

	private float calcPosition (Vector2 topLeft, Vector2 bottomRight, Vector2 pointA)
	{
		float vx1 = bottomRight.x - topLeft.x;
		float vy1 = bottomRight.y - topLeft.y;
		float vx2 = pointA.x - topLeft.x;
		float vy2 = pointA.y - topLeft.y;
		
		return  vx1 * vy2 - vy1 * vx2;	
		
	}
	
}