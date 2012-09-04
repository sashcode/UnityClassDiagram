using UnityEngine;
using System.Collections;
using UnityEditor;

public class Config
{
	public static string INSTALLED_PATH = "Assets/";
	//public static string INSTALLED_PATH = "Assets/SimpleData/";
	
	
	//EDGE TYPE
	public const int EDGE_TYPE_GENERALIZATION = 0;
	public const int EDGE_TYPE_REFERENCE = 1;
	public const int EDGE_TYPE_COMPOSITE = 2;
	
	//NODE TYPE
	public const int NODE_TYPE_CLASS = 0;
	
	public static string[] types = new string[]{"int", "float", "string", "bool", "Color", "Vector2", "Vector3", "Vector4", "AnimationCurve","Rect", "Texture", "Texture2D", "Object"};
	
	
	//TEXTURE
	public static Texture2D TEX_ADD = loadTexture ("UnityClassDiagram/icons/add.png");
	public static  Texture2D TEX_TOOL_REMOVE = loadTexture ("UnityClassDiagram/icons/remove.png");
	public static  Texture2D TEX_TOOL_REMOVE_MINI = loadTexture ("UnityClassDiagram/icons/removeMini.png");
	public static  Texture2D TEX_TOOL_GENERALIZATION = loadTexture ("UnityClassDiagram/icons/triangle_tool.png");
	public static  Texture2D TEX_TOOL_COMPOSITE = loadTexture ("UnityClassDiagram/icons/composite_tool.png");
	public static  Texture2D TEX_TOOL_REFERENCE = loadTexture ("UnityClassDiagram/icons/reference_tool.png");
	
	public static  Texture2D TEX_NO_IMAGE = loadTexture ("UnityClassDiagram/icons/no_image.png");
	public static  Texture2D TEX_NO_IMAGE_16 = loadTexture ("UnityClassDiagram/icons/no_image16.png");
	public static  Texture2D TEX_ANCHOR_GENERALIZATION = loadTexture ("UnityClassDiagram/icons/triangle.png");
	public static  Texture2D TEX_ANCHOR_COMPOSITE = loadTexture ("UnityClassDiagram/icons/composite.png");
	public static  Texture2D TEX_ANCHOR_REFERENCE = loadTexture ("UnityClassDiagram/icons/reference.png");
	
	public static Texture2D loadTexture (string relativePath)
	{
		return (Texture2D)AssetDatabase.LoadAssetAtPath (Config.INSTALLED_PATH + relativePath, typeof(Texture2D));
	}

}
