using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace HexGrid
{
    public class HexMapEditor : MonoBehaviour
    {
        Color _activeColor;
        [SerializeField] HexGrid _hexGrid;
        public Color[] colors;

        void Awake()
        {
            SelectColor(0);
        }

        void Update()
        {
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                HandleInput();
            }
        }

        void HandleInput()
        {
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(inputRay, out RaycastHit hit))
            {
                _hexGrid.ColorCell(hit.point, _activeColor);
            }
        }
        public void SelectColor(int index)
        {
            _activeColor = colors[index];
        }
    }
}
