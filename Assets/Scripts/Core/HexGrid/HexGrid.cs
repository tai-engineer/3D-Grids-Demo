using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HexGrid
{
    public class HexGrid : MonoBehaviour
    {
        public int width = 6;
        public int height = 6;

        [SerializeField] HexCell _cellPrefab;
        [SerializeField] Text _cellLabelPrefab;

        HexCell[] _cells;
        Canvas _gridCanvas;
        HexMesh _hexMesh;

        public Color _defaultColor = Color.white;
        public Color touchedColor = Color.magenta;
        void Awake()
        {
            _gridCanvas = GetComponentInChildren<Canvas>();
            _hexMesh = GetComponentInChildren<HexMesh>();

            _cells = new HexCell[width * height];

            for(int z = 0, i = 0; z < height; z++)
            {
                for(int x = 0; x < width; x++)
                {
                    CreateCell(x, z, i++);
                }
            }
        }

        void Start()
        {
            _hexMesh.Triangulate(_cells);
        }
        void CreateCell(int x, int z, int i)
        {
            Vector3 position;
            position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
            position.z = z * (HexMetrics.outerRadius * 1.5f);
            position.y = 0;

            HexCell cell = _cells[i] = Instantiate<HexCell>(_cellPrefab);
            cell.transform.SetParent(transform,false);
            cell.transform.localPosition = position;
            cell.coordinate = HexCoordinates.FromOffsetCoordinates(x, z);
            cell.color = _defaultColor;
            if(x > 0)
            {
                cell.SetNeighbor(HexDirection.W, _cells[i - 1]);
            }
            if(z > 0)
            {
                if( (z & 1) == 0)
                {
                    cell.SetNeighbor(HexDirection.SE, _cells[i - width]);
                    if (x > 0)
                        cell.SetNeighbor(HexDirection.SW, _cells[i - width - 1]);
                }
                else
                {
                    cell.SetNeighbor(HexDirection.SW, _cells[i - width]);
                    if (x < width - 1)
                        cell.SetNeighbor(HexDirection.SE, _cells[i - width + 1]);
                }
            }

            Text label = Instantiate<Text>(_cellLabelPrefab);
            label.rectTransform.SetParent(_gridCanvas.transform, false);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
            label.text = cell.coordinate.ToStringSeparateLines();
        }
        
        public void ColorCell(Vector3 position, Color color)
        {
            position = transform.InverseTransformPoint(position);
            HexCoordinates coord = HexCoordinates.FromPosition(position);
            int index = coord.Z * width + coord.X + coord.Z / 2;
            _cells[index].color = color;
            _hexMesh.Triangulate(_cells);
        }
    }

    [System.Serializable]
    public struct HexCoordinates
    {
        [SerializeField] int _x, _z;
        public int X { get { return _x; } }
        public int Z { get { return _z; } }
        public int Y
        {
            get
            {
                return -_x - _z;
            }
        }

        public HexCoordinates(int x, int z)
        {
            _x = x;
            _z = z;
        }
        public string ToStringSeparateLines()
        {
            return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
        }
        public override string ToString()
        {
            return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
        }

        public static HexCoordinates FromOffsetCoordinates(int x, int z)
        {
            return new HexCoordinates(x - z / 2, z);
        }
        public static HexCoordinates FromPosition(Vector3 position)
        {
            float x = position.x / (HexMetrics.innerRadius * 2);
            float y = -x;

            float offset = position.z / (HexMetrics.outerRadius * 3);
            x -= offset;
            y -= offset;

            int iX = Mathf.RoundToInt(x);
            int iY = Mathf.RoundToInt(y);
            int iZ = Mathf.RoundToInt(-x - y);

            if ((iX + iY + iZ) != 0)
            {
                float dX = Mathf.Abs(x - iX);
                float dY = Mathf.Abs(y - iY);
                float dZ = Mathf.Abs(-x - y - iZ);
                if(dX > dY && dX > dZ)
                {
                    iX = -iY - iZ;
                }
                else if(dZ > dY)
                {
                    dZ = -iX - iY;
                }
            }
            return new HexCoordinates(iX, iZ);
        }
    }
}