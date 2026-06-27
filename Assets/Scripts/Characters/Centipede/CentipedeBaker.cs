using System.Collections.Generic;
using UnityEngine;

namespace Characters.Centipede
{
	public class CentipedeBaker : MonoBehaviour
	{
		[SerializeField] private CentipedeBakingInfo _BakingInfo;

		public void BakeSegments()
		{
			Debug.Log("Baking segments");
			List<GameObject> segmentBakingInfos = _BakingInfo._SegmentPrefabs;
			
			CentipedeHead head = GetComponentInChildren<CentipedeHead>();
			
			if (head is null)
			{
				Debug.LogError("CentipedeHead component not found on children of CentipedeBaker");
				return;
			}
			
			head.ClearAndDestroySegments();
			head.ReserveSegmentsMemory(segmentBakingInfos.Count);
			
			float totalLength = head._Length * 0.5f;
			Vector3 initialPosition = head.transform.position;
			
			foreach (GameObject prefab in segmentBakingInfos)
			{
				GameObject segmentGO = Instantiate(prefab, transform);
				CentipedeSegment segment = segmentGO.GetComponent<CentipedeSegment>();
				
				if (segment is null)
				{
					Debug.LogError($"Segment prefab {prefab.name} does not have a CentipedeSegment component");
					return;
				}
				
				float length = segment._Length;
				
				totalLength += length;
				float lengthToCenterOfSegment = totalLength - (length * 0.5f);
				
				int delay = head.CalculateDelayForSegmentOnLength(lengthToCenterOfSegment);
				segment._Delay = delay;
				segment.transform.position = initialPosition + Vector3.left * lengthToCenterOfSegment;
				segment._Head = head;
				
				head.AddSegment(segment);
			}
		}
	}
}
