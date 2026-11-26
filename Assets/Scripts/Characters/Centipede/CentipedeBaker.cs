using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Centipede
{
	public class CentipedeBaker : MonoBehaviour
	{
		[FormerlySerializedAs("_BakingInfo")] [SerializeField] private CentipedeBakingInfo _bakingInfo;

		public void BakeSegments()
		{
			Debug.Log("Baking segments");
			List<GameObject> segmentBakingInfos = _bakingInfo.SegmentPrefabs;
			
			CentipedeHead head = GetComponentInChildren<CentipedeHead>();
			
			if (head is null)
			{
				Debug.LogError("CentipedeHead component not found on children of CentipedeBaker");
				return;
			}
			
			head.ClearAndDestroySegments();
			head.ReserveSegmentsMemory(segmentBakingInfos.Count);
			
			float totalLength = head.Length * 0.5f;
			Vector3 initialPosition = head.transform.position;
			
			foreach (GameObject prefab in segmentBakingInfos)
			{
				GameObject segmentGo = Instantiate(prefab, transform);
				CentipedeSegment segment = segmentGo.GetComponent<CentipedeSegment>();
				
				if (segment is null)
				{
					Debug.LogError($"Segment prefab {prefab.name} does not have a CentipedeSegment component");
					return;
				}
				
				float length = segment.Length;
				
				totalLength += length;
				float lengthToCenterOfSegment = totalLength - (length * 0.5f);
				
				int delay = head.CalculateDelayForSegmentOnLength(lengthToCenterOfSegment);
				segment.Delay = delay;
				segment.transform.position = initialPosition + Vector3.left * lengthToCenterOfSegment;
				segment.Head = head;
				
				head.AddSegment(segment);
			}
		}
	}
}
