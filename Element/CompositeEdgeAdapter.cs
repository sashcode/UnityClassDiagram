using UnityEngine;
using System.Collections;

public class  CompositeEdgeAdapter:EdgeAdapter
{
	override public Texture2D GetSourceAnchorTexture ()
	{
		return Config.TEX_ANCHOR_COMPOSITE;
	}

	override public Texture2D  GetTargetAnchorTexture ()
	{
		return null;
	}	
	
}

