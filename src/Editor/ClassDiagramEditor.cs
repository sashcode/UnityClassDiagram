using UnityEngine;
using UnityEditor;
using System.Collections;

public class ClassDiagramEditor: EditorWindow
{
	Texture2D texAdd = loadTexture ("UnityClassDiagram/src/UnityClassDiagram/icons/add.png");
	Texture2D texRemove = loadTexture ("UnityClassDiagram/src/UnityClassDiagram/icons/remove.png");
	Texture2D texRemoveMini = loadTexture ("UnityClassDiagram/src/UnityClassDiagram/icons/removeMini.png");
	Texture2D texSuper = loadTexture ("UnityClassDiagram/src/UnityClassDiagram/icons/super.png");
	Texture2D texNoImage = loadTexture ("UnityClassDiagram/src/UnityClassDiagram/icons/no_image.png");
	Texture2D texNoImage16 = loadTexture ("UnityClassDiagram/src/UnityClassDiagram/icons/no_image16.png");
	
	public static Texture2D loadTexture (string relativePath)
	{
		return (Texture2D)AssetDatabase.LoadAssetAtPath ("Assets/" + relativePath, typeof(Texture2D));
	}
	
	public ClassDiagramEditor ()
	{
		
	}
	
	static ClassDiagramEditor window = null;
	Color s = new Color (0.4f, 0.4f, 0.5f);
	
	[MenuItem("Assets/Create/ClassDiagram")]
	static void Create ()
	{     
		GameObject gameObject = new GameObject ();
		gameObject.AddComponent ("ClassDiagramData");
		PrefabUtility.CreatePrefab ("Assets/MyObject.prefab", gameObject);
		
		
		init ();
		
			
		//PrefabUtility.DisconnectPrefabInstance (gameObject);
		//UnityEngine.Object prefab = PrefabUtility.CreateEmptyPrefab ("Assets/MyObject.prefab");
		

		//EditorUtility.ReplacePrefab (gameObject, prefab, ReplacePrefabOptions.ReplaceNameBased);
		//Destroy(gameObject);
		 

		//flag = false;
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
	int focusWidowId = -1;
	Class focusClass = null;
	int mode = 0;
	int mode_draw = 0;
	int mode_ref = 1;
	Class refModeClass = null;
	Class refModeTargetClass = null;
	
	void drawWindow (int id)
	{
		
		Class clazz = (Class)cuurentClassData.classes.GetValue (id);
		
		Rect boxRect = new Rect (0, 16, clazz.rect.width, clazz.rect.height - 16);
		GUI.Box (boxRect, "");
		
		if ((Event.current.button == 0) && (Event.current.type == EventType.MouseDown)) {
			focusWidowId = id;
			if (mode == mode_ref) {
				if (refModeClass != clazz) {
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
				Debug.Log (path);
				Debug.Log (Application.dataPath);
				if (Application.dataPath.Length < path.Length) {
					path = path.Substring (Application.dataPath.Length);
					Debug.Log (path);
					char[] chTrims = {'/', '\\'};
					path = path.TrimStart (chTrims);
					Debug.Log (path);
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
				mode = mode_ref;
				refModeClass = clazz;
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
		
		//if(focusWidowId == id){
		
		//}
		
		
		
		for (int index = 0; index < clazz.attributes.Length; index++) {
			Attribute attr = (Attribute)clazz.attributes.GetValue (index);
			float width = clazz.rect.width;
			float nwidth = 60;
			float twidth = 60;
			float y = 60 + 18 * index;
			
			Rect irect = new Rect (12, y, 16, 16);
			Rect nrect = new Rect (irect.x + irect.width + 4, y, nwidth, 16);
			Rect crect = new Rect (nrect.x + nwidth, y, 8, 16);
			Rect trect = new Rect (crect.x + crect.width, y, twidth, 16);
			
			
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
				attr.type = GUI.TextField (trect, attr.type);
			} else {
				GUI.Label (nrect, attr.name);
				GUI.Label (crect, ":");
				GUI.Label (trect, attr.type);
			}
			

			
			
			
			
			if (id == focusWidowId) {
				
				GUIStyle style = new GUIStyle (GUIStyle.none);
				style.normal.background = texRemoveMini;
				if (GUI.Button (new Rect (trect.x + trect.width + 2, y, 13, 16), "", style)) {
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
		}
		
		
		
		GUI.DragWindow ();
	}
	
	void OnGUI ()
	{
		
		GameObject gameObject = Selection.activeGameObject;
		if (!gameObject) {
			return;
		}
			
		ClassDiagramData classData = gameObject.GetComponent<ClassDiagramData> ();
		if (!classData) {
			return;
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
				if (target != null) {
					curveFromTo (clazz.rect, target.rect, new Color (0.3f, 0.7f, 0.4f), s);
				}
			}
		}
		EndWindows ();
		
		if (mode == mode_ref) {
			if (refModeTargetClass != null) {
				refModeClass.superClassName = refModeTargetClass.name;
				
				mode = mode_draw;
				refModeTargetClass = null;
			}
		}
		
	}
	
	void curveFromTo (Rect wr, Rect wr2, Color color, Color shadow)
	{
		
		Drawing.bezierLine (
            new Vector2 (wr.x + wr.width, wr.y + wr.height / 2),
            new Vector2 (wr.x + wr.width + Mathf.Abs (wr2.x - (wr.x + wr.width)) / 2, wr.y + wr.height / 2),
            new Vector2 (wr2.x, wr2.y + wr2.height / 2),
            new Vector2 (wr2.x - Mathf.Abs (wr2.x - (wr.x + wr.width)) / 2, wr2.y + wr2.height / 2), color, 2, true, 20);
	}
	
}
