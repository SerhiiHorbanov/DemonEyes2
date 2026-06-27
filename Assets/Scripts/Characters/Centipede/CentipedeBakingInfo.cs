using System.Collections.Generic;
using UnityEngine;

namespace Characters.Centipede
{
	[CreateAssetMenu(fileName = "New Centipede Baking Info", menuName = "ScriptableObjects/Centipede Baking Info", order = 0)]
	public class CentipedeBakingInfo : ScriptableObject
	{
		[SerializeField] public List<GameObject> _SegmentPrefabs;
	}
}
