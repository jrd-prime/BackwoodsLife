using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.Quest
{
    public class QuestPanelUIController : MonoBehaviour, IUIPanelController
    {
        [SerializeField] private VisualTreeAsset questPanelInWindowUITemplate;
        [SerializeField] private VisualTreeAsset questItemTemplate;

        public VisualTreeAsset GetTemplateFor(string inWindow)
        {
            //TODO refact
            return inWindow switch
            {
                "InWindow" => questPanelInWindowUITemplate,
                "QuestItem" => questItemTemplate,
                _ => throw new ArgumentOutOfRangeException(nameof(inWindow), inWindow, null)
            };
        }
    }
}
