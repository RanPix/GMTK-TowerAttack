using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Unit.UnitTags
{
    public class UnitTagsCollection : MonoBehaviour
    {
        private static Dictionary<UnitStatus, UnitTag> TagsDictionary;

        [SerializeField] private UnitTag[] unitTags;

        private void Awake()
        {
            SetDictionary();
        }

        private void SetDictionary()
        {
            if (TagsDictionary != null)
            {
                Debug.LogWarning("Another instance of UnitTagsCollection already exist");
                return;
            }


            Dictionary<UnitStatus, UnitTag> tagsDictionary = new();

            foreach (var unitTag in unitTags)
            {
                if (!tagsDictionary.TryAdd(unitTag.Status, unitTag))
                    Debug.LogWarning($"Intersepting unitTypes {unitTag}", this);
            }

            TagsDictionary = tagsDictionary;
        }

        public static UnitTag GetUnitTag(UnitStatus status)
            => TagsDictionary[status];
    }
}