using UnityEditor;
using UnityEngine;

namespace Characters.Centipede
{
	[CustomEditor(typeof(CentipedeBaker))]
	public class CentipedeBakerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			
			if (GUILayout.Button("Bake Centipede Segments"))
			{
				(target as CentipedeBaker)?.BakeSegments();
			}
		}
	}
}
