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

        #region Terrace Fields
        public const float elevationStep = 5f;
        public const int terracesPerSlope = 2;
        public const int terraceSteps = terracesPerSlope * 2 + 1;
        public const float horizontalTerraceStepSize = 1f / terraceSteps;
        public const float verticalTerraceStepSize = 1f / (terracesPerSlope + 1);
        #endregion

        #region Corner Methods
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
        #endregion

        #region Terrace Methods
        public static Vector3 TerraceLerp(Vector3 a, Vector3 b, int step)
        {
            float h = step * HexMetrics.horizontalTerraceStepSize;
            a.x += (b.x - a.x) * h;
            a.z += (b.z - a.z) * h;

            float v = ((step + 1) / 2) * HexMetrics.verticalTerraceStepSize;
            a.y += (b.y - a.y) * v;
            return a;
        }
        public static Color TerraceLerp(Color a, Color b, int step)
        {
            float h = step * HexMetrics.horizontalTerraceStepSize;
            return Color.Lerp(a, b, h);
        }
        public static HexEdgeType GetEdgeType(int elevation1, int elevation2)
        {
            if(elevation1 == elevation2)
                return HexEdgeType.Flat;

            int delta = Mathf.Abs(elevation1 - elevation2);
            if (delta == 1)
                return HexEdgeType.Slope;

            return HexEdgeType.Cliff;
        }
        #endregion
    }
    public enum HexEdgeType
    {
        Flat, Slope, Cliff
    }
}