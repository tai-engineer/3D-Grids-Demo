using UnityEngine;
namespace GridSystem.HexGrid
{
    public class HexMetrics
    {
        public static float outerRadius = 10f;
        public static float innerRadius = outerRadius * 0.866025404f;

        public static Vector3[] corners =
        {
            new Vector3(0f, 0f, outerRadius),
            new Vector3(innerRadius, 0f, outerRadius * 0.5f),
            new Vector3(innerRadius, 0f, -outerRadius * 0.5f),
            new Vector3(0f, 0f, -outerRadius),
            new Vector3(-innerRadius, 0f, -outerRadius * 0.5f),
            new Vector3(-innerRadius, 0f, outerRadius * 0.5f)
        };
    }
}