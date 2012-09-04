using UnityEngine;
using System.Collections;

public class GeneralizationEdgeAdapter:EdgeAdapter
{
	override public Texture2D GetSourceAnchorTexture(){
		return null;
	}
	override public Texture2D  GetTargetAnchorTexture(){
		return Config.TEX_ANCHOR_GENERALIZATION;
	}
}
