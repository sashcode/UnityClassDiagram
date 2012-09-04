using UnityEngine;
using System.Collections.Generic;

public class ClassNodeAdapter : NodeAdapter {
	public string name = "class name";
	public string iconPath;
	private Texture2D texIcon ;
	private GUIStyle iconStyle = new GUIStyle (GUIStyle.none);
	
	override public void DrawNode (DiagramNode node ,DiagramContext context)
	{
		
		GUI.Box (node.rect, "");
		
		
		
		Rect iconRect = new Rect (6, 22, 32, 32);
		if (iconPath != null) {
			texIcon = loadTexture (iconPath);
		}
		if (texIcon == null) {
			texIcon = Config.TEX_NO_IMAGE;
		}
		

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
		
		
		
		
		
		
	}
}
