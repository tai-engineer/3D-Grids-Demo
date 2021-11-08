using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HexGrid
{
    public class HexCell : MonoBehaviour
    {
        public HexCoordinates coordinate;
        public Color color;
        [SerializeField] HexCell[] _neighbors;

        public HexCell GetNeighbor(HexDirection direction)
        {
            return _neighbors[(int)direction];
        }
        public void SetNeighbor(HexDirection direction, HexCell cell)
        {
            _neighbors[(int)direction] = cell;
            cell._neighbors[(int)direction.Opposite()] = this;
        }
    }

    public enum HexDirection
    {
        NE, E, SE, SW, W, NW
    }
    public static class HexDirectionExtensions
    {
        public static HexDirection Opposite(this HexDirection direction)
        {
            return (int)direction < 3 ? direction + 3 : direction - 3;
        }

        public static HexDirection Next(this HexDirection direction)
        {
            return direction == HexDirection.NW ? HexDirection.NE : (direction + 1);
        }

        public static HexDirection Previous(this HexDirection direction)
        {
            return direction == HexDirection.NE ? HexDirection.NW : (direction - 1);
        }
    }
}