using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridSystem.HexGrid
{
    public class HexGrid : MonoBehaviour
    {
        public int width = 6;
        public int height = 6;

        [SerializeField] HexCell _cellPrefab;
        [SerializeField] Text _cellLabelPrefab;

        HexCell[] _cells;
        Canvas _gridCanvas;
        void Awake()
        {
            _gridCanvas = GetComponentInChildren<Canvas>();

            _cells = new HexCell[width * height];

            for(int z = 0, i = 0; z < height; z++)
            {
                for(int x = 0; x < width; x++)
                {
                    CreateCell(x, z, i++);
                }
            }
        }

        void CreateCell(int x, int z, int i)
        {
            Vector3 position;
            position.x = (x + z *  0.5f) * (HexMetrics.innerRadius * 2f);
            position.z = z * (HexMetrics.innerRadius * 1.5f); ;
            position.y = 0;

            HexCell cell = _cells[i] = Instantiate<HexCell>(_cellPrefab);
            cell.transform.SetParent(transform,false);
            cell.transform.localPosition = position;

            Text label = Instantiate<Text>(_cellLabelPrefab);
            label.rectTransform.SetParent(_gridCanvas.transform, false);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
            label.text = x.ToString() + "\n" + z.ToString();
        }
    }
}