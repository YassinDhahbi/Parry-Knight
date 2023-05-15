using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
namespace Tagging
{
    [CreateAssetMenu(fileName = "Tag Condition", menuName = "Tagging System / Tagging Condition")]
    public class TaggingCondition : ScriptableObject
    {
        [SerializeField]
        List<Tagger> tagConditionComponents;
        [SerializeField]
        GameEvent OnResponseEvent;
        public bool CheckForCompatibility(GameObject currentObject, GameObject otherObject)
        {
            var result = false;
            if (currentObject.TryGetComponent(out TagManager col1TagManager) & otherObject.TryGetComponent(out TagManager col2TagManager))
            {
                var col1TagList = col1TagManager.tagList;
                var col2TagList = col2TagManager.tagList;
                int compatiblitiesOfManagerOne = 0;
                int compatiblitiesOfManagerTwo = 0;
                foreach (var item in col1TagList)
                {
                    if (tagConditionComponents.Contains(item))
                    {
                        compatiblitiesOfManagerOne++;
                    }
                }
                foreach (var item in col2TagList)
                {
                    if (tagConditionComponents.Contains(item))
                    {
                        compatiblitiesOfManagerTwo++;
                    }
                }

                result = CheckForTagExistence(col1TagList) && CheckForTagExistence(col2TagList) && CheckForRedundance(col1TagList, col2TagList);
                if (result & OnResponseEvent != null)
                {
                    OnResponseEvent.Raise();
                }

            }
            return result;
        }


        private bool CheckForRedundance(List<Tagger> firstManager, List<Tagger> secondManager)
        {
            int redundance = 0;
            foreach (var item in firstManager)
            {
                if (secondManager.Contains(item))
                {
                    redundance++;
                }
            }
            return redundance == 0;
        }
        private bool CheckForTagExistence(List<Tagger> colTagList)
        {
            var compatiblities = 0;
            foreach (var item in colTagList)
            {
                if (tagConditionComponents.Contains(item))
                {
                    compatiblities++;
                }
            }
            return compatiblities > 0;
        }
    }
}


