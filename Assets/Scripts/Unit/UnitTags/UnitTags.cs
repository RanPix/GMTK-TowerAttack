using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Unit.UnitTags
{
    public class UnitTags : MonoBehaviour, IEnumerable
    {
        [SerializeField] private UnitStatus[] desiredStatuses;

        private List<UnitTag> _unitTags;
        private Queue<UnitStatus> _deleteQueue = new();
    
        public Action<UnitStatus, bool> OnTagsChanged;

        private void Awake()
        {
            ConfigureEntryTags();
        }

        private void ConfigureEntryTags()
        {
            List<UnitTag> tags = new();

            foreach (var status in desiredStatuses)
            {
                if (!ContainsConflictingTypes(status))
                    tags.Add(UnitTagsCollection.TagsDictionary[status]);
            }

            _unitTags = tags;
        }

        /// <returns> 
        ///     True - conflicting types, hence its not valid
        /// </returns>
        private bool ContainsConflictingTypes(UnitStatus status)
        {
            foreach (UnitTag tag in _unitTags)
            {
                if (tag.ConflictingTypes.Contains(status))
                {
                    Debug.LogError($"ConflinctingTypes with {status}", this);
                    return true;
                }

                if (tag.Type == status && !tag.IsStackAble)
                {
                    Debug.LogError($"ConflinctingTypes with {status}", this);
                    return true;
                }
            }

            return false;
        }

        public bool ContainsUnitStatus(UnitStatus requestedTag)
            => _unitTags.Contains(UnitTagsCollection.TagsDictionary[requestedTag]);

        public IEnumerator GetEnumerator()
            => _unitTags.GetEnumerator();

        public bool AddTag(UnitStatus status)
        {
            if (ContainsUnitStatus(status) && !UnitTagsCollection.TagsDictionary[status].IsStackAble)
                return false;

            _unitTags.Add(UnitTagsCollection.TagsDictionary[status]);
            OnTagsChanged?.Invoke(status, true);

            return true;
        }

        public void RemoveTag(UnitStatus status)
        {
            OnTagsChanged?.Invoke(status, false);
            _unitTags.Remove(UnitTagsCollection.TagsDictionary[status]);
        }

        private void RemoveQueuedTag()
        {
            RemoveTag(_deleteQueue.Dequeue());
        }

        public void AddTemporarTag(UnitStatus status, float time, bool canStack = true)
        {
            if (!AddTag(status))
                return;

            _deleteQueue.Enqueue(status);
            Invoke(nameof(RemoveQueuedTag), time);
        }
    }
}