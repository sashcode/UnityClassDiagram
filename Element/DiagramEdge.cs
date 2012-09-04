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
	[System.NonSerialized]
	public static List<Vector2> TEMP_POSITIONS = new List<Vector2> ();
	
	public int typeId = Config.EDGE_TYPE_GENERALIZATION;
	
	public void SetSource (DiagramNode node)
	{
		EdgeAnchorHandle handle = new EdgeAnchorHandle ();
		handle.nodeId = node.uuid;
		sourceAnchor = handle;
		sourceAnchor.node = node;
	}
	
	public void SetTarget (DiagramNode node)
	{
		EdgeAnchorHandle handle = new EdgeAnchorHandle ();
		handle.nodeId = node.uuid;
		targetAnchor = handle;
		targetAnchor.node = node;
	}
	
	public void UpdateAnchor (DiagramContext context)
	{
		
		//
		Rect sourceRect = sourceAnchor.GetNode (context).rect;
		Rect targetRect = targetAnchor.GetNode (context).rect;
		
		Vector2 pointA = new Vector2 (sourceRect.x + sourceRect.width / 2, sourceRect.y + sourceRect.height / 2);
		Vector2 pointB = new Vector2 (targetRect.x + targetRect.width / 2, targetRect.y + targetRect.height / 2);
		
		bool source = false;
		bool target = false;
		if (sourceAnchor.relative) {
			//
		} else {
			if (sourceAnchor.relativePosition == EdgeAnchorHandle.CENTER) {
				sourceAnchor.position = GetAnchorPos (pointB, sourceRect, pointA);
			} else {
				//if (!((sourceAnchor.relativePosition.x == 1.0f || sourceAnchor.relativePosition.x == 0.0f) && (sourceAnchor.relativePosition.y == 1.0f || sourceAnchor.relativePosition.y == 0.0f))) {
					source = true;
				//} else {
				//	float tempX = sourceRect.x + sourceRect.width * sourceAnchor.relativePosition.x;
				//	float tempY = sourceRect.y + sourceRect.height * sourceAnchor.relativePosition.y;
				//	sourceAnchor.position.x = tempX;
				//	sourceAnchor.position.y = tempY;
				//}
			}
		}
		
		if (targetAnchor.relative) {
			//
		} else {
			if (targetAnchor.relativePosition == EdgeAnchorHandle.CENTER) {
				targetAnchor.position = GetAnchorPos (pointA, targetRect, pointB);	
			} else {
				//if (!((targetAnchor.relativePosition.x == 1.0f || targetAnchor.relativePosition.x == 0.0f) && (targetAnchor.relativePosition.y == 1.0f || targetAnchor.relativePosition.y == 0.0f))) {
					target = true;
				//} else {
				//	targetAnchor.position.x = targetRect.x + targetRect.width * targetAnchor.relativePosition.x;
				//	targetAnchor.position.y = targetRect.y + targetRect.height * targetAnchor.relativePosition.y;	
				//}
			}
		}
		
		Debug.Log(" source anchor = " + sourceAnchor.relativePosition);
		Debug.Log(" target anchor = " + targetAnchor.relativePosition);
		if (source || target) {
			if (source && target) {
				pointA = new Vector2 (sourceRect.x + sourceRect.width * sourceAnchor.relativePosition.x, sourceRect.y + sourceRect.height * sourceAnchor.relativePosition.y);
				pointB = new Vector2 (targetRect.x + targetRect.width * targetAnchor.relativePosition.x, targetRect.y + targetRect.height * targetAnchor.relativePosition.y);
			} else if (source && !target) {
				pointA = new Vector2 (sourceRect.x + sourceRect.width * sourceAnchor.relativePosition.x, sourceRect.y + sourceRect.height * sourceAnchor.relativePosition.y);
			} else if (!source && target) {
				pointB = new Vector2 (targetRect.x + targetRect.width * targetAnchor.relativePosition.x, targetRect.y + targetRect.height * targetAnchor.relativePosition.y);
			}
			if (source && target) {
				sourceAnchor.position = GetAnchorPos (pointB, sourceRect, pointA);
				targetAnchor.position = GetAnchorPos (pointA, targetRect, pointB);	
			} else if (source && !target) {
				Debug.Log(" source ");
				sourceAnchor.position = GetAnchorPos (pointB, sourceRect, pointA);
			} else if (!source && target) {
				Debug.Log(" target ");
				targetAnchor.position = GetAnchorPos (pointA, targetRect, pointB);	
			}	
		}
		if(!sourceRect.Contains(pointA)){
			Debug.LogError(" source Error pointA ! ");
		}
		if(!sourceRect.Contains(sourceAnchor.position)){
			Debug.LogError(" source Error ! ");
		}
		if(!targetRect.Contains(targetAnchor.position)){
			Debug.LogError(" target Error ! ");
		}
		
		Debug.Log(" source anchor = " + sourceAnchor.position + "  sourceRect = "  + sourceRect);
		Debug.Log("  pointA = " + pointA + " " + sourceRect.Contains(pointA));
		Debug.Log(" target anchor = " + targetAnchor.position + "  targetRect = "  + targetRect);
		Debug.Log("  pointB = " + pointB + " " + targetRect.Contains(pointB));
		
	}
	
	public virtual void Draw (DiagramContext context)
	{
		EdgeHandle[] handles = GetEdgeHandles ();
		for (int i = 0; i < handles.Length -1; i++) {
			EdgeHandle handle1 = handles [i];
			EdgeHandle handle2 = handles [i + 1];
			EdgeAdapter adapter = context.GetEdgeAdapter(typeId);
			drawLine (handle1.position, handle2.position, lineColor, false, adapter.GetSourceAnchorTexture(), adapter.GetTargetAnchorTexture());
		}
	}
	
	override public DiagramHandle[] GetHandles ()
	{
		TEMP_HANDLES.Clear ();
		TEMP_HANDLES.Add (sourceAnchor);
		TEMP_HANDLES.AddRange (handles);
		TEMP_HANDLES.Add (targetAnchor);
		return TEMP_HANDLES.ToArray ();
	}
	
	public EdgeHandle[] GetEdgeHandles ()
	{
		TEMP_HANDLES.Clear ();
		TEMP_HANDLES.Add (sourceAnchor);
		TEMP_HANDLES.AddRange (handles);
		TEMP_HANDLES.Add (targetAnchor);
		
		return TEMP_HANDLES.ToArray ();
	}
	
	public DiagramElement HitTest (DiagramContext context, Vector2 position)
	{
		EdgeHandle[] handles = GetEdgeHandles ();
		TEMP_POSITIONS.Clear ();
		for (int i = 0; i < handles.Length; i++) {
			EdgeHandle handle1 = handles [i];
			TEMP_POSITIONS.Add (handle1.position);
		}	
		if (DiagramUtil.ContainsEdge (TEMP_POSITIONS, position, 4)) {
			return this;
		}
		return null;
	}
	
	override public void DrawHandle (DiagramContext context)
	{
		foreach (EdgeHandle handle in GetEdgeHandles()) {
			handle .Draw (context);
		}
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
			DrawAnchor (pointB, angle, destAnchor);
		}
		if (srcAnchor != null) {
			DrawAnchor (pointA, angle + 180, srcAnchor);
		}
		
		
		GUI.matrix = savedMatrix;
		GUI.color = savedColor;
	}
	
	Vector2 GetAnchorPos (Vector2 pointA, Rect wr2, Vector2 pointB)
	{
		//DiagramUtil.ExpandRect(wr2 , 10 , 10 ) ;
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
	
	void DrawAnchor (Vector2 destAnchorPos, float angle, Texture2D texAnchor)
	{
		Color savedColor = GUI.color;
		Matrix4x4 savedMatrix = GUI.matrix;
		
		Vector2 arrowPivot = new Vector2 (destAnchorPos.x, destAnchorPos.y);
		Vector2 arrowPoint = new Vector2 (destAnchorPos.x, destAnchorPos.y);
		arrowPoint.x -= 16;
		arrowPoint.y -= 8;
		GUI.matrix = Matrix4x4.TRS (arrowPoint, Quaternion.identity, Vector3.one);
		GUIUtility.RotateAroundPivot (angle, arrowPivot);
		GUI.DrawTexture (new Rect (0, 0, 16, 16), texAnchor);
		
		GUI.matrix = savedMatrix;
		GUI.color = savedColor;
	}
}