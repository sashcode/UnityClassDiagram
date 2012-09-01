using UnityEngine;
using System.Collections;

public class DiagramUtil{
	
	
	public static Rect ShrinkRect(Rect rect , float dw , float dh){
		return new Rect(rect.x + dw/2 , rect.y + dh/2 , rect.width - dw , rect.height - dh);
	}
	
	public static Rect ExpandRect(Rect rect , float dw , float dh){
		return new Rect(rect.x - dw/2 , rect.y - dh/2 , rect.width + dw , rect.height + dh);
	}
	
	public static Rect UnionRect(Rect rect , float x1 , float y1){
				if (x1 < rect.x) {
			rect.width += (rect.x - x1);
			rect.x = x1;
		} else {
			float right = rect.x + rect.width;
			if (x1 >= right) {
				right = x1 + 1;
				rect.width = right - rect.x;
			}
		}
		if (y1 < rect.y) {
			rect.height += (rect.y - y1);
			rect.y = y1;
		} else {
			float bottom = rect.y + rect.height;
			if (y1 >= bottom) {
				bottom = y1 + 1;
				rect.height = bottom - rect.y;
			}
		}
		return rect;

	}
	
	
	
	public static bool containsEdge(DiagramEdge edge, float x, float y,int tolerance) {
		
		float[] coordinates = new float[edge.handles.Count];
		
		for(int i = 0 ; i< edge.handles.Count -1 ; i++ ){
			EdgeHandle handle1 = edge.handles[i];
			EdgeHandle handle2 = edge.handles[i+1];
			if (containsSegment(handle1.position.x,
                         handle1.position.y, handle2.position.x,
                         handle2.position.y, x, y, tolerance)) {
                    return true;
               }
			
			
		}
          
          return false;
     }
	public static bool containsSegment(float x1, float y1, float x2, float y2,
               float px, float py, int tolerance) {
          /*
           * Point should be located inside Rectangle(x1 -+ tolerance, y1 -+
           * tolerance, x2 +- tolerance, y2 +- tolerance)
           */
          Rect lineBounds = new Rect();
          lineBounds.width = 0;
		lineBounds.height = 0;
		lineBounds.x = x1;
		lineBounds.y = y1;
		
          lineBounds = UnionRect(lineBounds , x2, y2);
		
          lineBounds = ExpandRect(lineBounds,tolerance, tolerance);
          if (!lineBounds.Contains(new Vector2(px, py))) {
               return false;
          }

          /*
           * If this is horizontal, vertical line or dot then the distance between
           * specified point and segment is not more then tolerance (due to the
           * lineBounds check above)
           */
          if (x1 == x2 || y1 == y2) {
               return true;
          }

          /*
           * Calculating square distance from specified point to this segment
           * using formula for Dot product of two vectors.
           */
          float v1x = x2 - x1;
          float v1y = y2 - y1;
          float v2x = px - x1;
          float v2y = py - y1;
          float numerator = v2x * v1y - v1x * v2y;
          float denominator = v1x * v1x + v1y * v1y;
          float squareDistance = numerator * numerator / denominator;
          return squareDistance <= tolerance * tolerance;
     }


}