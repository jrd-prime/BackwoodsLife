using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.UIPanel.Warehouse
{
    public class WarehousePanelUIController : MonoBehaviour, IUIPanelController
    {
        [SerializeField] private VisualTreeAsset warehouseInWindowPanelTemplate;

        public VisualTreeAsset GetTemplateFor(string inWindow)
        {
            //TODO refact
            return inWindow switch
            {
                "InWindow" => warehouseInWindowPanelTemplate,
                // "QuestItem" => questItemTemplate,
                _ => throw new ArgumentOutOfRangeException(nameof(inWindow), inWindow, null)
            };
        }
    }
}
