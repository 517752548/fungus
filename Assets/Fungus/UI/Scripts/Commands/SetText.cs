using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.Serialization;

namespace Fungus
{
	[CommandInfo("UI", 
	             "Set Text", 
	             "Sets the text property on a UI Text object and/or an Input Field object.")]
	
	[AddComponentMenu("")]
	public class SetText : Command 
	{
		[Tooltip("Text object to set text on. Can be a UI Text, Text Field or Text Mesh object.")]
		public GameObject targetTextObject;
		
		[Tooltip("String value to assign to the text object")]
		public StringData stringData;
		
		public override void OnEnter()
		{
			Flowchart flowchart = GetFlowchart();
			string newText = flowchart.SubstituteVariables(stringData.Value);
			
			if (targetTextObject == null)
			{
				Continue();
				return;
			}
			
			// Use first component found of Text, Input Field or Text Mesh type
			Text uiText = targetTextObject.GetComponent<Text>();
			if (uiText != null)
			{
				uiText.text = newText;
			}
			else
			{
				InputField inputField = targetTextObject.GetComponent<InputField>();
				if (inputField != null)
				{
					inputField.text = newText;
				}
				else
				{
					TextMesh textMesh = targetTextObject.GetComponent<TextMesh>();
					if (textMesh != null)
					{
						textMesh.text = newText;
					}
				}
			}
			
			Continue();
		}
		
		public override string GetSummary()
		{
			if (targetTextObject != null)
			{
				return targetTextObject.name + " : " + stringData.Value;
			}
			
			return "Error: No text object selected";
		}
		
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, 255);
		}

		// Backwards compatibility with Fungus v2.1.2
		[HideInInspector]
		[FormerlySerializedAs("textObject")]
		public Text _textObjectObsolete;
		protected virtual void OnEnable()
		{
			if (_textObjectObsolete != null)
			{
				targetTextObject = _textObjectObsolete.gameObject;
			}
		}
	}
	
}