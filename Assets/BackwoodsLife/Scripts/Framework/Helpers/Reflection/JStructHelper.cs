using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Data.Common.Structs.Required;
using BackwoodsLife.Scripts.Data.Inventory;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Framework.Helpers.Reflection
{
// TODO rename
    public static class JStructHelper
    {
        public static void CompileReturnableElements(
            ref List<CollectableElement> listToSave,
            ref List<ReturnCollectables> returnCollectablesList)
        {
            listToSave.Clear();
            foreach (var collectable in returnCollectablesList)
            {
                Type type = collectable.GetType();
                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

                foreach (FieldInfo field in fields)
                {
                    var fieldValue = field.GetValue(collectable);

                    if (fieldValue is IEnumerable list)
                    {
                        foreach (var item in list)
                        {
                            Type itemType = item.GetType();
                            FieldInfo collectableTypeField = itemType.GetField("type");
                            FieldInfo collectRangeField = itemType.GetField("range");

                            Assert.IsNotNull(collectableTypeField, "collectableTypeField is null");
                            Assert.IsNotNull(collectRangeField, "collectRangeField is null");

                            var range = collectRangeField.GetValue(item) is CollectRange
                                ? (CollectRange)collectRangeField.GetValue(item)
                                : default;

                            listToSave.Add(new CollectableElement
                            {
                                Name = collectableTypeField.GetValue(item).ToString(),
                                Range = range
                            });
                        }
                    }
                }
            }
        }

        public static void CompiledRequiredElements(
            ref List<RequiredElement> listToSave,
            ref List<RequirementForCollect> requirementsForCollecting)
        {
            listToSave.Clear();
            foreach (var collectable in requirementsForCollecting)
            {
                Type type = collectable.GetType();
                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

                foreach (FieldInfo field in fields)
                {
                    var fieldValue = field.GetValue(collectable);

                    if (fieldValue is IEnumerable list)
                    {
                        foreach (var item in list)
                        {
                            Type itemType = item.GetType();
                            FieldInfo typeField = itemType.GetField("typeName");
                            FieldInfo valueField = itemType.GetField("value");

                            Assert.IsNotNull(typeField, "collectableTypeField is null");
                            Assert.IsNotNull(valueField, "collectRangeField is null");

                            var value = valueField.GetValue(item) is int
                                ? (int)valueField.GetValue(item)
                                : default;

                            listToSave.Add(new RequiredElement
                            {
                                typeName = typeField.GetValue(item).ToString(),
                                value = value
                            });
                        }
                    }
                }
            }
        }
    }
}
