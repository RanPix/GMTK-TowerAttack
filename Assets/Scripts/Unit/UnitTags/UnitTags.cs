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
                    tags.Add(UnitTagsCollection.GetUnitTag(status));
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
                if (tag.ConflictingStatuses.Contains(status))
                {
                    Debug.LogError($"ConflinctingTypes with {status}", this);
                    return true;
                }

                if (tag.Status == status && !tag.IsStackable)
                {
                    Debug.LogError($"ConflinctingTypes with {status}", this);
                    return true;
                }
            }

            return false;
        }

        public bool ContainsUnitStatus(UnitStatus status)
            => _unitTags.Contains(UnitTagsCollection.GetUnitTag(status));

        public IEnumerator GetEnumerator()
            => _unitTags.GetEnumerator();

        public bool AddTag(UnitStatus status)
        {
            if (ContainsUnitStatus(status) && !UnitTagsCollection.GetUnitTag(status).IsStackable)
                return false;

            _unitTags.Add(UnitTagsCollection.GetUnitTag(status));
            OnTagsChanged?.Invoke(status, true);

            return true;
        }

        public void RemoveTag(UnitStatus status)
        {
            OnTagsChanged?.Invoke(status, false);
            _unitTags.Remove(UnitTagsCollection.GetUnitTag(status));
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