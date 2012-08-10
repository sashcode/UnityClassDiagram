using UnityEngine;
using UnityEditor;
using System.Collections;

public class ClassDiagramEditor: EditorWindow
{
	Texture2D texAdd = loadTexture ("UnityClassDiagram/icons/add.png");
	Texture2D texRemove = loadTexture ("UnityClassDiagram/icons/remove.png");
	Texture2D texRemoveMini = loadTexture ("UnityClassDiagram/icons/removeMini.png");
	Texture2D texSuper = loadTexture ("UnityClassDiagram/icons/super.png");
	Texture2D texNoImage = loadTexture ("UnityClassDiagram/icons/no_image.png");
	Texture2D texNoImage16 = loadTexture ("UnityClassDiagram/icons/no_image16.png");
	Texture2D texTriangle = loadTexture ("UnityClassDiagram/icons/triangle.png");
    
	public static Texture2D loadTexture (string relativePath)
	{
		return (Texture2D)AssetDatabase.LoadAssetAtPath (Config.INSTALLED_PATH + relativePath, typeof(Texture2D));
	}
	
	public ClassDiagramEditor ()
	{
		
	}

	private void changeModeDraw ()
	{
		Debug.Log ("changeModeDraw");
		wantsMouseMove = false;
		refModeClass = null;
		refModeTargetClass = null;
		mode = mode_draw;
	}

	private void changeModeRef (Class clazz)
	{
		Debug.Log ("changeModeRef");
		wantsMouseMove = true;
		refModeClass = clazz;
		mode = mode_ref;
	}
	
	public void OnInspectorUpdate ()
	{
		Repaint ();
	}
	
	static ClassDiagramEditor window = null;
	
	void OnSelectionChange ()
	{
		
		
	}
	
	public void Update ()
	{
		//Debug.Log("update");
	}
		
	[MenuItem("Assets/Create/ClassDiagram")]
	static void Create ()
	{     
		GameObject gameObject = new GameObject ();
		gameObject.AddComponent ("ClassDiagramData");
		string newPrefabPath = "Assets/ClassDiagram";
		string extension = ".prefab";
		string newPath = newPrefabPath + extension;
		GameObject newPrefab = null;
		for (int i = 0; i < System.Int16.MaxValue; i++) {
			if (i == 0) {
				newPath = newPrefabPath + extension;
			} else {
				newPath = newPrefabPath + " " + i + extension;
			}
			if (!System.IO.File.Exists (newPath)) {
				newPrefab = PrefabUtility.CreatePrefab (newPath, gameObject);
				break;
			}
		}	
		
		Object.DestroyImmediate (gameObject, true);
		Selection.activeObject = newPrefab;
		
		init ();
	}
	
	[MenuItem("Window/ClassDiagramEditor")]
	static void init ()
	{

		if (window) {
			//window.Close ();
		} else {
			window = EditorWindow.GetWindow<ClassDiagramEditor> ();
		}
	}

	private ClassDiagramData cuurentClassData;
	private int focusWidowId = -1;
	private Class focusClass = null;
	private int mode = 0;
	private int mode_draw = 0;
	private int mode_ref = 1;
	private Class refModeClass = null;
	private Class refModeTargetClass = null;
	
	void drawWindow (int id)
	{
		
		Class clazz = (Class)cuurentClassData.classes.GetValue (id);
		
		Rect boxRect = new Rect (0, 16, clazz.rect.width, clazz.rect.height - 16);
		GUI.Box (boxRect, "");
		
		if ((Event.current.button == 0) && (Event.current.type == EventType.MouseDown)) {
			focusWidowId = id;
			if (mode == mode_ref) {
				if (refModeClass != null && refModeClass != clazz) {
					refModeTargetClass = clazz;
				}
			}
		}
		
		
		string iconPath = clazz.iconPath;
		Texture2D texIcon = null;
		Rect iconRect = new Rect (6, 22, 32, 32);
		if (iconPath != null) {
			texIcon = loadTexture (iconPath);
		}
		if (texIcon == null) {
			texIcon = texNoImage;
		}
		
		GUIStyle iconStyle = new GUIStyle (GUIStyle.none);
		iconStyle.normal.background = texIcon;
		if (GUI.Button (iconRect, "", iconStyle)) {
			string path = EditorUtility.OpenFilePanel ("Select Icon", "Assets", "");
			if (path != null) {
				if (Application.dataPath.Length < path.Length) {
					path = path.Substring (Application.dataPath.Length);
					char[] chTrims = {'/', '\\'};
					path = path.TrimStart (chTrims);
					clazz.iconPath = path;
				}
			}
		}
		
		
		Rect textRect = new Rect (iconRect.x + iconRect.width + 10, iconRect.y + 8, clazz.rect.width - (iconRect.x + iconRect.width + 8) - 20, 16);
		if (id == focusWidowId) {
			clazz.name = GUI.TextField (textRect, clazz.name);
		} else {
			GUI.Label (textRect, clazz.name);
		}
		
		float bx = clazz.rect.width - 18;
		
		if (id == focusWidowId) {
			GUIStyle style = new GUIStyle (GUIStyle.none);
			style.normal.background = texSuper;
			Rect btnRect = new Rect (0, 0, 16, 16);
			if (GUI.Button (btnRect, "", style)) {
				if (clazz.superClassName != null && 0 < clazz.superClassName.Length) {
					clazz.superClassName = null;
				} else {
					changeModeRef (clazz);
				}
			}
			style.normal.background = texAdd;
			btnRect.x += 18;
			if (GUI.Button (btnRect, "", style)) {
				System.Collections.Generic.List<Attribute> attrList = new System.Collections.Generic.List<Attribute> (clazz.attributes);
				attrList.Add (new Attribute ());
				clazz.attributes = attrList.ToArray ();	
			}
			style.normal.background = texRemove;
			btnRect.x = bx;
			btnRect.y = 0;
			if (GUI.Button (btnRect, "", style)) {
				System.Collections.Generic.List<Class> classList = new System.Collections.Generic.List<Class> (cuurentClassData.classes);
				classList.Remove (clazz);
				cuurentClassData.classes = classList.ToArray ();
			}
		}
		
		
		for (int index = 0; index < clazz.attributes.Length; index++) {
			Attribute attr = (Attribute)clazz.attributes.GetValue (index);
			float width = clazz.rect.width;
			float nwidth = 50;
			float twidth = 90;
			float y = 60 + 18 * index;
			
			Rect irect = new Rect (12, y, 16, 16);
			Rect nrect = new Rect (irect.x + irect.width + 4, y, nwidth, 16);
			Rect crect = new Rect (nrect.x + nwidth, y, 8, 16);
			Rect trect = new Rect (crect.x + crect.width - 10, y, twidth, 16);
			
			
			texIcon = null;
			string attrIconPath = attr.iconPath;
			Texture2D attrTexIcon = null;
			if (attrIconPath != null) {
				texIcon = loadTexture (attrIconPath);
			}
			if (texIcon == null) {
				texIcon = texNoImage16;
			}
			GUIStyle attrIconStyle = new GUIStyle (GUIStyle.none);
			attrIconStyle.normal.background = texIcon;
			if (GUI.Button (irect, "", attrIconStyle)) {
				string path = EditorUtility.OpenFilePanel ("Select Icon", "Assets", "");
				if (path != null) {
					if (Application.dataPath.Length < path.Length) {
						path = path.Substring (Application.dataPath.Length);
						char[] chTrims = {'/', '\\'};
						path = path.TrimStart (chTrims);
						attr.iconPath = path;
					}
				}
			}
			
			
			if (id == focusWidowId) {
				attr.name = GUI.TextField (nrect, attr.name);
				GUI.Label (crect, ":");
				attr.type = DrawTypeField (trect, attr.type);
			} else {
				GUI.Label (nrect, attr.name);
				GUI.Label (crect, ":");
				DrawTypeField (trect, attr.type);
				//GUI.Label (trect, attr.type);
			}
			
			
			if (id == focusWidowId) {
				
				GUIStyle style = new GUIStyle (GUIStyle.none);
				style.normal.background = texRemoveMini;
				if (GUI.Button (new Rect (trect.x + trect.width - 4, y, 13, 16), "", style)) {
					System.Collections.Generic.List<Attribute> attributeList = new System.Collections.Generic.List<Attribute> (clazz.attributes);
					attributeList.Remove (attr);
					clazz.attributes = attributeList.ToArray ();
				}
			}
		}
		
		int space = 12;
		if (0 < clazz.attributes.Length) {
			space = 20;
		}
		
		if (clazz.rect.height - boxRect.y - space < clazz.attributes.Length * 18 + iconRect.height) {
			clazz.rect.height = boxRect.y + clazz.attributes.Length * 18 + iconRect.height + space;
		} else if (0 < (clazz.rect.height - boxRect.y - space) - (clazz.attributes.Length * 18 + iconRect.height)) {
			clazz.rect.height = clazz.attributes.Length * 18 + iconRect.height + boxRect.y + space;
		}
		
		
		
		GUI.DragWindow ();
	}
	
	string DrawTypeField (Rect rect, string type)
	{
		string[] types = null;
		if (cuurentClassData != null) {
			types = cuurentClassData.types;
		} else {
			types = new string[]{"int" , "string"};
		}
		for (int i = 0; i < types.Length; i++) {
			if (types [i] == type) {
				int selection = EditorGUI.Popup (rect, i, types);
				return types [selection];
			}
		}

		return null;
	}
	
	void OnGUI ()
	{
		Event e = Event.current;
		switch (e.type) {
		case EventType.keyDown:
			{
				if (mode == mode_ref) {
					changeModeDraw ();
				}
				break;
			}
		}
		
		GameObject gameObject = Selection.activeGameObject;
		if (!gameObject) {
			GUI.Label (new Rect (20, 20, 300, 100), "ClassDiagramData is not found.");
			return;
		}
			
		ClassDiagramData classData = gameObject.GetComponent<ClassDiagramData> ();
		if (!classData) {
			GUI.Label (new Rect (20, 20, 300, 100), "Select ClassDiagramData.");
			return;
		}
		
		if (cuurentClassData != classData) {
			focusClass = null;
			focusWidowId = -1;
			mode = mode_draw;
			refModeClass = null;
			refModeTargetClass = null;
		}
		
		cuurentClassData = classData;
		
		int focusWindowId_old = focusWidowId;
		Class focusWindowClass_old = focusClass;
		
		
		if (mode == mode_draw) {
			if ((Event.current.button == 0) && (Event.current.type == EventType.MouseDown)) {
				focusWidowId = -1;
				focusClass = null;
			}
		}
		
		if (GUI.Button (new Rect (10, 35, 25, 18), "+")) {
			System.Collections.Generic.List<Class> classList = new System.Collections.Generic.List<Class> (classData.classes);
			Class newClass = new Class ();
			{
				string newName = newClass.name;
				int size = classData.classes.Length;
				if (0 < size) {
					string nameTemp = newName;
					int index = 1;
					while (true) {
						bool exist = false;
						foreach (Class c in classList) {
							if (c.name == nameTemp) {
								exist = true;
								break;
							}
						}
						if (exist) {
							nameTemp = newName + " " + index;
							index ++;
						} else {
							newName = nameTemp; 
							break;
						}
					
					}
			
				}
				newClass.name = newName;
			}
			{
				Rect newRect = new Rect (newClass.rect);
				int size = classData.classes.Length;
				if (0 < size) {
					while (true) {
						bool exist = false;
						foreach (Class c in classList) {
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
				newClass.rect = newRect;
			}
			
			classList.Add (newClass);
			cuurentClassData.classes = classList.ToArray ();	
		}
		
		cuurentClassData.diagramName = GUI.TextField (new Rect (10, 10, 200, 20), cuurentClassData.diagramName);
		
		GUIStyle windowStyle = new GUIStyle (GUIStyle.none);
		BeginWindows ();
		for (int index = 0; index < cuurentClassData.classes.Length; index++) {
			Class clazz = (Class)cuurentClassData.classes.GetValue (index);	
			clazz.rect = GUI.Window (index, clazz.rect, drawWindow, "", windowStyle);
			
			if (focusWidowId == index) {
				focusClass = clazz;
			}
			
			if (clazz.superClassName != null) {
				Class target = clazz.GetSuperClass (cuurentClassData);
				Rect clazzRect = new Rect (clazz.rect.x, clazz.rect.y + 16, clazz.rect.width, clazz.rect.height - 16);
				if (target != null) {
					drawLine (clazzRect
						, new Rect (target.rect.x, target.rect.y + 16, target.rect.width, target.rect.height - 16), new Color (0.3f, 0.4f, 0.7f), false);
				} else {
					if (clazz.superClassName != null && 0 < clazz.superClassName.Length) {
						drawLine (clazzRect
						, new Rect (clazzRect.x, clazzRect.y - 50, clazzRect.width, clazzRect.height), new Color (0.3f, 0.4f, 0.7f), true);
					}
				}
			}
		}
		EndWindows ();
		
		if (mode == mode_ref) {
			Vector2 mouse = Event.current.mousePosition;
			drawLine (new Rect (refModeClass.rect.x, refModeClass.rect.y + 16, refModeClass.rect.width, refModeClass.rect.height - 16)
						, new Rect (mouse.x - 5, mouse.y - 5, 5, 5), new Color (0.3f, 0.4f, 0.7f), true);
			
			
			if (refModeTargetClass != null) {
				refModeClass.superClassName = refModeTargetClass.name;
				changeModeDraw ();
			}
		}
		
	}
	
	public static Texture2D lineTex = null;
	
	void drawLine (Rect wr, Rect wr2, Color color, bool mouse)
	{
		Vector2 pointA = new Vector2 (wr.x + wr.width / 2, wr.y + wr.height / 2);
		Vector2 pointB = new Vector2 (wr2.x + wr2.width / 2, wr2.y + wr2.height / 2);
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
		
		if (mouse) {
			arrowPos.x = pointB.x;
			arrowPos.y = pointB.y;
		} else {
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
		}
		
		GUI.color = savedColor;
		Vector2 arrowPivot = new Vector2(arrowPos.x , arrowPos.y);
		arrowPos.x -= 16;
		arrowPos.y -= 8;
		GUI.matrix = Matrix4x4.TRS (arrowPos, Quaternion.identity, Vector3.one);
		GUIUtility.RotateAroundPivot (angle, arrowPivot);
		GUI.DrawTexture (new Rect (0, 0, 16, 16), texTriangle);
		
		GUI.matrix = savedMatrix;
		GUI.color = savedColor;
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
