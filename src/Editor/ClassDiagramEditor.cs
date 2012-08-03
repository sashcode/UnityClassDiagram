using UnityEngine;
using UnityEditor;
using System.Collections;


public class ClassDiagramEditor: EditorWindow{
	static ClassDiagramEditor window = null;
	
	
    [MenuItem("Window/ClassDiagramEditor")]
    static void init()
    {

		if(window){
            window.Close();
		}
        window = EditorWindow.GetWindow<ClassDiagramEditor>();
    }
	private ClassDiagramData cuurentClassData;
	
    void drawWindow(int id)
    {
		Class clazz  = (Class)cuurentClassData.classes.GetValue(id);
		clazz.name = GUI.TextField(new Rect(3,  20 , clazz.rect.width - 6 , 20), clazz.name);
		
        
		if(GUI.Button(new Rect(3, 40, 25, 15), "+")){
		    System.Collections.Generic.List<Attribute> attrList = new System.Collections.Generic.List<Attribute>(clazz.attributes);
			attrList.Add(new Attribute());
			clazz.attributes = attrList.ToArray();	
		}
		
		float bx = clazz.rect.width - 13;
		 if(GUI.Button(new Rect(bx, 3, 10, 10), "")){
			System.Collections.Generic.List<Class> classList = new System.Collections.Generic.List<Class>(cuurentClassData.classes);
			classList.Remove(clazz);
			cuurentClassData.classes = classList.ToArray();
		 }
		
		float height = 60;
		for(int index = 0; index < clazz.attributes.Length;index++){
			Attribute attr  = (Attribute)clazz.attributes.GetValue(index);
			float width = clazz.rect.width;
			float nwidth = (width - 15) / 2;
			float twidth = (width - 15) / 2;;
			
			float y = 56 + 22 * index;
			
			attr.name = GUI.TextField(new Rect(3, y, nwidth, 20), attr.name);
			attr.type = GUI.TextField(new Rect(nwidth + 3, y, twidth, 20), attr.type);
			
			if(GUI.Button(new Rect(nwidth + 3 + twidth + 1, y, 10, 10), "+")){
		       System.Collections.Generic.List<Attribute> attributeList = new System.Collections.Generic.List<Attribute>(clazz.attributes);
			   attributeList.Remove(attr);
			   clazz.attributes = attributeList.ToArray();
		    }
		}
		height += clazz.attributes.Length * 22;
		if(100 < height ){
			clazz.rect.height = height;
		}
		
		
        
		
		GUI.DragWindow();
    }
	
    void OnGUI()
    {
		GameObject gameObject =Selection.activeGameObject;
		if(!gameObject){
			return;
		}
			
		ClassDiagramData classData = gameObject.GetComponent<ClassDiagramData>();
		if(!classData){
			return;
		}
		
		cuurentClassData = classData;
		
		
		if(GUI.Button(new Rect(10, 35, 25, 18), "+")){
		    Debug.Log("+ push");
			Debug.Log(cuurentClassData.classes);
			System.Collections.Generic.List<Class> classList = new System.Collections.Generic.List<Class>(classData.classes);
			classList.Add(new Class());
			cuurentClassData.classes = classList.ToArray();	
        }
		
		cuurentClassData.diagramName = GUI.TextField(new Rect(10, 10, 200, 20), cuurentClassData.diagramName);
		
        BeginWindows();
		for(int index = 0; index < cuurentClassData.classes.Length;index++){
			Class clazz  = (Class)cuurentClassData.classes.GetValue(index);	
			clazz.rect = GUI.Window(index, clazz.rect, drawWindow, "");
		}
		
        EndWindows();
		
		if(Input.GetMouseButton(0)) {
			Debug.Log ("AA");
		}
    }
	
}
