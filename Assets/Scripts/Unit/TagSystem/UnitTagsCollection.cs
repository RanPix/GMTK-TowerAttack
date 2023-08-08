using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Unit.TagSystem
{
    public class UnitTagsCollection : MonoBehaviour
    {
        private static Dictionary<UnitStatus, UnitTag> _tagsDictionary;

        [SerializeField] private UnitTag[] unitTags;

        private void Awake()
        {
            SetDictionary();
        }

        private void SetDictionary()
        {
            if (_tagsDictionary != null)
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

            _tagsDictionary = tagsDictionary;
        }

        public static UnitTag GetUnitTag(UnitStatus status)
            => _tagsDictionary[status];
    }
}