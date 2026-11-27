using Unity.Mathematics.Geometry;
using UnityEngine;

namespace Characters.Hand
{
    [CreateAssetMenu(fileName = "HandsLayout", menuName = "ScriptableObjects/HandsLayout")]
    public class HandsLayout : ScriptableObject
    {
        [SerializeField] private MinMaxAABB[] _boxes;
    }
}