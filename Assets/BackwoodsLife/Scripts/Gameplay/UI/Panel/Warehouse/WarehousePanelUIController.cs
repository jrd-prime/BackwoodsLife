using System;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel;
using UnityEngine;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Gameplay.UI.Panel.Warehouse
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
