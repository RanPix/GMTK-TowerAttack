using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Unit.UnitTags
{
    public class UnitTagsCollection : MonoBehaviour
    {
        [SerializeField] private UnitTag[] unitTags;

        public static Dictionary<UnitStatus, UnitTag> TagsDictionary { get; private set; } 


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
                if (!tagsDictionary.TryAdd(unitTag.Type, unitTag))
                    Debug.LogWarning($"Intersepting unitTypes {unitTag}", this);
            }

            TagsDictionary = tagsDictionary;
        }
    }
}