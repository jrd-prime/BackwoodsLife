using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.Quest
{
    public class QuestPanelUIController : MonoBehaviour, IUIPanelController
    {
        [SerializeField] private VisualTreeAsset questPanelInWindowUITemplate;

        public VisualTreeAsset GetTemplateFor(string inWindow)
        {
            //TODO refact
            return inWindow switch
            {
                "InWindow" => questPanelInWindowUITemplate,
                _ => throw new ArgumentOutOfRangeException(nameof(inWindow), inWindow, null)
            };
        }
    }
}
