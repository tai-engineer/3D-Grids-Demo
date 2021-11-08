using UnityEngine;
namespace HexGrid
{
    public class HexMetrics
    {
        public const float solidFactor = 0.75f;
        public const float blendFactor = 1f - solidFactor;
        public static float outerRadius = 10f;
        public static float innerRadius = outerRadius * 0.866025404f;
        static Vector3[] _corners =
        {
            new Vector3(0f, 0f, outerRadius),
            new Vector3(innerRadius, 0f, outerRadius * 0.5f),
            new Vector3(innerRadius, 0f, -outerRadius * 0.5f),
            new Vector3(0f, 0f, -outerRadius),
            new Vector3(-innerRadius, 0f, -outerRadius * 0.5f),
            new Vector3(-innerRadius, 0f, outerRadius * 0.5f),
            new Vector3(0f, 0f, outerRadius)
        };

        public static Vector3 GetFirstCorner(HexDirection direction)
        {
            return _corners[(int)direction];
        }
        public static Vector3 GetSecondCorner(HexDirection direction)
        {
            return _corners[(int)direction + 1];
        }

        public static Vector3 GetFirstSolidCorner(HexDirection direction)
        {
            return _corners[(int)direction] * solidFactor;
        }

        public static Vector3 GetSecondSolidCorner(HexDirection direction)
        {
            return _corners[(int)direction + 1] * solidFactor;
        }

        public static Vector3 GetBridge(HexDirection direction)
        {
            return (_corners[(int)direction] + _corners[(int)direction + 1]) * blendFactor;
        }
    }
}