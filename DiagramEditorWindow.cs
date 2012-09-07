using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public abstract class DiagramEditorWindow : EditorWindow
{
	
	protected DiagramRoot currentDiagramRoot;
	private DiagramContext diagramContext ;
	protected DiagramTool currentDiagramTool;
	
	public DiagramRoot GetRoot ()
	{
		return currentDiagramRoot;
	}
	
	protected abstract DiagramRoot getDiagramRoot (GameObject activeGameObject);

	protected abstract string GetNullDiagramRootMessage ();
	
	public DiagramEditorWindow ()
	{
		diagramContext = new DiagramContext (this);	
	}
	
	void log (string txt)
	{
		//Debug.Log (txt);
	}
	
	void OnSelectionChange ()
	{
		currentDiagramRoot = getDiagramRoot ();
		Repaint ();
	}
	
	protected DiagramRoot getDiagramRoot ()
	{
		GameObject gameObject = Selection.activeGameObject;
		if (!gameObject) {
			return null;
		}
		return getDiagramRoot (gameObject);
	}
	
	void OnGUI ()
	{
		 
		if (currentDiagramRoot == null) {
			string msg = GetNullDiagramRootMessage ();
			GUI.Label (new Rect (20, 20, 300, 100), msg);
			return;
		}
		if (currentDiagramTool == null) {
			currentDiagramTool = new DiagramDefaultTool ();
		}
		
		
		if (currentDiagramTool.OnGUI (diagramContext)) {
			//Update Anchors  position
			foreach (DiagramNode node in currentDiagramRoot.nodes) {
				foreach (DiagramEdge edge in node.edges) {
					edge.UpdateAnchor (diagramContext);
				}
			}
			
			// Draw DiagramRoot and children
			currentDiagramRoot.Draw (diagramContext);
			
			
			//Draw Selected Element handles
			List<DiagramElement> elements = diagramContext.GetSelection ().GetElements ();
			foreach (DiagramElement elm in elements) {
				elm.DrawHandle (diagramContext);
			}
			
			//Draw Tools
			
			
			

			
		}
		
		//OnGUIimpl();
	}
	
	protected abstract void OnGUIimpl ();
	
	
}
