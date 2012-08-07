using UnityEngine;
using UnityEditor;
using System.Collections;

public class ClassDiagramEditor: EditorWindow
{
	static ClassDiagramEditor window = null;
	Color s = new Color(0.4f, 0.4f, 0.5f);
	
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
		
		if ((Event.current.button == 0) && (Event.current.type == EventType.MouseDown)) {
			focusWidowId = id;
			if (mode == mode_ref) {
				if (refModeClass != clazz) {
					refModeTargetClass = clazz;
				}
			}
		}
		
		
		clazz.name = GUI.TextField (new Rect (3, 20, clazz.rect.width - 6, 20), clazz.name);
		
		float bx = clazz.rect.width - 13;
		
		if (id == focusWidowId) {
			if (GUI.Button (new Rect (2, 2, 12, 12), "<-")) {
				mode = mode_ref;
				refModeClass = clazz;
			}
			if (GUI.Button (new Rect (18, 2, 12, 12), "+")) {
				System.Collections.Generic.List<Attribute> attrList = new System.Collections.Generic.List<Attribute> (clazz.attributes);
				attrList.Add (new Attribute ());
				clazz.attributes = attrList.ToArray ();	
			}
			
			if (GUI.Button (new Rect (bx, 3, 10, 10), "")) {
				System.Collections.Generic.List<Class> classList = new System.Collections.Generic.List<Class> (cuurentClassData.classes);
				classList.Remove (clazz);
				cuurentClassData.classes = classList.ToArray ();
			}
		}
		
		//if(focusWidowId == id){
		
		//}
		
		
		
		float height = 60;
		for (int index = 0; index < clazz.attributes.Length; index++) {
			Attribute attr = (Attribute)clazz.attributes.GetValue (index);
			float width = clazz.rect.width;
			float nwidth = (width - 15) / 2;
			float twidth = (width - 15) / 2 + 10;
			float y = 56 + 22 * index;
			if (id == focusWidowId) {
				twidth = (width - 15) / 2;
			}
			attr.name = GUI.TextField (new Rect (3, y, nwidth, 20), attr.name);
			attr.type = GUI.TextField (new Rect (nwidth + 3, y, twidth, 20), attr.type);
			
			if (id == focusWidowId) {
			if (GUI.Button (new Rect (nwidth + 3 + twidth + 1, y, 10, 10), "+")) {
				System.Collections.Generic.List<Attribute> attributeList = new System.Collections.Generic.List<Attribute> (clazz.attributes);
				attributeList.Remove (attr);
				clazz.attributes = attributeList.ToArray ();
			}
			}
		}
		height += clazz.attributes.Length * 22;
		if (100 < height) {
			clazz.rect.height = height;
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
			Debug.Log ("+ push");
			Debug.Log (cuurentClassData.classes);
			System.Collections.Generic.List<Class> classList = new System.Collections.Generic.List<Class> (classData.classes);
			classList.Add (new Class ());
			cuurentClassData.classes = classList.ToArray ();	
		}
		
		cuurentClassData.diagramName = GUI.TextField (new Rect (10, 10, 200, 20), cuurentClassData.diagramName);
		
		BeginWindows ();
		for (int index = 0; index < cuurentClassData.classes.Length; index++) {
			Class clazz = (Class)cuurentClassData.classes.GetValue (index);	
			clazz.rect = GUI.Window (index, clazz.rect, drawWindow, "");
			
			if (focusWidowId == index) {
				focusClass = clazz;
			}
			
			 if(clazz.superClassName != null){
				Class target = clazz.GetSuperClass(cuurentClassData);
				if(target != null){
					
						curveFromTo(clazz.rect, target.rect, new Color(0.3f,0.7f,0.4f), s);
				}
			}
			
			
			//int cid = clazz.GetInstanceID();
			//Debug.Log(" instance id=" + cid);
			//Class clazz0 = (Class)EditorUtility.InstanceIDToObject(cid);
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
	
	void curveFromTo(Rect wr, Rect wr2, Color color, Color shadow)
    {
        Drawing.bezierLine(
            new Vector2(wr.x + wr.width, wr.y + 3 + wr.height / 2),
            new Vector2(wr.x + wr.width + Mathf.Abs(wr2.x - (wr.x + wr.width)) / 2, wr.y + 3 + wr.height / 2),
            new Vector2(wr2.x, wr2.y + 3 + wr2.height / 2),
            new Vector2(wr2.x - Mathf.Abs(wr2.x - (wr.x + wr.width)) / 2, wr2.y + 3 + wr2.height / 2), shadow, 5, true,20);
        Drawing.bezierLine(
            new Vector2(wr.x + wr.width, wr.y + wr.height / 2),
            new Vector2(wr.x + wr.width + Mathf.Abs(wr2.x - (wr.x + wr.width)) / 2, wr.y + wr.height / 2),
            new Vector2(wr2.x, wr2.y + wr2.height / 2),
            new Vector2(wr2.x - Mathf.Abs(wr2.x - (wr.x + wr.width)) / 2, wr2.y + wr2.height / 2), color, 2, true,20);
    }
	
}
