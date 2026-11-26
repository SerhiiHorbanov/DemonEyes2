using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Centipede
{
	[CreateAssetMenu(fileName = "New Centipede Baking Info", menuName = "ScriptableObjects/Centipede Baking Info", order = 0)]
	public class CentipedeBakingInfo : ScriptableObject
	{
		[FormerlySerializedAs("_SegmentPrefabs")] [SerializeField] public List<GameObject> SegmentPrefabs;
	}
}
