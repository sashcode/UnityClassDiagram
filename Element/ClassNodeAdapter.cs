using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class ClassNodeAdapter : NodeAdapter {
	
	private static Rect iconRect = new Rect (6, 6, 32, 32);
	private static Rect addAttributeToolRect = new Rect (6, -16, 16, 16);
	private static Rect TEMP = new Rect ();
	
	
	override public bool HitTest (DiagramNode node , DiagramContext context , Vector2 position)
	{
		bool selected = context.GetSelection().GetElements().Contains(node);
		if(selected){
		TEMP =  DiagramUtil.TranslateRect(addAttributeToolRect , node.rect.x , node.rect.y );
			if(TEMP.Contains(position)){
				return true;
			}
		}
		return false;
	}
	override public void DrawNode (DiagramNode node ,DiagramContext context)
	{
		
		bool selected = context.GetSelection().GetElements().Contains(node);
		GUI.Box (node.rect, "");
		
		
		
		if(selected){
			
			
			GUIStyle style = new GUIStyle (GUIStyle.none);
			style.normal.background = Config.TEX_ADD;
			
			TEMP =  DiagramUtil.TranslateRect(addAttributeToolRect , node.rect.x , node.rect.y );
			if (GUI.Button (TEMP, "", style)) {
				context.GetCommand().AddAttribute(node);
			}
			
			
			
			
			
		}
		
		
		
		
		TEMP =  DiagramUtil.TranslateRect(iconRect , node.rect.x , node.rect.y );
		
		
		if (node.iconPath != null) {
			node.texIcon = Config.loadTexture (node.iconPath);
		}
		if (node.texIcon == null) {
			node.texIcon = Config.TEX_NO_IMAGE;
		}
		

		node.iconStyle.normal.background = node.texIcon;
		if (GUI.Button (TEMP, "", node.iconStyle)) {
			string path = EditorUtility.OpenFilePanel ("Select Icon", "Assets", "");
			if (path != null) {
				if (Application.dataPath.Length < path.Length) {
					path = path.Substring (Application.dataPath.Length);
					char[] chTrims = {'/', '\\'};
					path = path.TrimStart (chTrims);
					node.iconPath = path;
				}
			}
		}		
		
		
		TEMP = DiagramUtil.TranslateRect( new  Rect (iconRect.x + iconRect.width + 10, iconRect.y + 8, node.rect.width - (iconRect.x + iconRect.width + 8) - 20, 16) , node.rect.x , node.rect.y);
		if (selected) {
			node.name = GUI.TextField (TEMP, node.name);
		} else {
			GUI.Label (TEMP, node.name);
		}

		
		
		
		
		for (int index = 0; index < node.attributes.Count; index++) {
			Attribute attr = (Attribute)node.attributes[index];
			float nwidth = 50;
			float twidth = 90;
			float y = 60 + 18 * index;
			
			Rect irect =new Rect (12, y, 16, 16);
			Rect nrect = new Rect (irect.x + irect.width + 4, y, nwidth, 16);
			Rect crect = new Rect (nrect.x + nwidth, y, 8, 16);
			Rect trect = new Rect (crect.x + crect.width - 10, y, twidth, 16);
			
			
			Texture2D texIcon = null;
			string attrIconPath = attr.iconPath;
			if (attrIconPath != null) {
				texIcon = Config.loadTexture (attrIconPath);
			}
			if (texIcon == null) {
				texIcon = Config.TEX_NO_IMAGE_16;
			}
			GUIStyle attrIconStyle = new GUIStyle (GUIStyle.none);
			attrIconStyle.normal.background = texIcon;
			
			TEMP = DiagramUtil.TranslateRect( irect,node.rect.x , node.rect.y);
			if (GUI.Button (TEMP, "", attrIconStyle)) {
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
			
			
			
			
			if (selected) {
				TEMP = DiagramUtil.TranslateRect( nrect,node.rect.x , node.rect.y);
				attr.name = GUI.TextField (TEMP, attr.name);
				TEMP = DiagramUtil.TranslateRect( crect,node.rect.x , node.rect.y);
				GUI.Label (TEMP, ":");
				TEMP = DiagramUtil.TranslateRect( trect,node.rect.x , node.rect.y);
				attr.type = DrawTypeField (TEMP, attr.type);
			} else {
				TEMP = DiagramUtil.TranslateRect( nrect,node.rect.x , node.rect.y);
				GUI.Label (TEMP, attr.name);
				TEMP = DiagramUtil.TranslateRect( crect,node.rect.x , node.rect.y);
				GUI.Label (TEMP, ":");
				TEMP = DiagramUtil.TranslateRect( trect,node.rect.x , node.rect.y);
				DrawTypeField (TEMP, attr.type);
				//GUI.Label (trect, attr.type);
			}
			
			
			if (selected) {
				
				GUIStyle style = new GUIStyle (GUIStyle.none);
				style.normal.background = Config.TEX_TOOL_REMOVE_MINI;
				TEMP = DiagramUtil.TranslateRect(new Rect (trect.x + trect.width - 4, y, 13, 16),node.rect.x , node.rect.y);
				if (GUI.Button (TEMP, "", style)) {
					node.attributes.Remove (attr);
				}
			}

			
		}
		
		
	}
	
	
	string DrawTypeField (Rect rect, string type)
	{
		string[] types = Config.types;
		int index = 0;
		for (int i = 0; i < types.Length; i++) {
			if (types [i] == type) {
			index = i;	
			}
		}

		int selection = EditorGUI.Popup (rect, index, types);
		return types [selection];
	}
	
}
