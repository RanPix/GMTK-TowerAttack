using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Unit.TagSystem
{
    public class UnitTags : IEnumerable
    {
        private List<UnitTag> _unitTags = new();
        private Queue<UnitStatus> _deleteQueue = new();
    
        public Action<UnitStatus, bool> OnTagsChanged;

        public UnitTags(UnitTemplate template)
        {
            ConfigureEntryTags(template.DesiredTypes);
        }

        private void ConfigureEntryTags(UnitStatus[] desiredStatuses)
        {
            foreach (var status in desiredStatuses)
            {
                if (!ContainsConflictingTypes(status))
                    _unitTags.Add(UnitTagsCollection.GetUnitTag(status));
            }
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
                    Debug.LogError($"ConflinctingTypes with {status}");
                    return true;
                }

                if (tag.Status == status && !tag.IsStackable)
                {
                    Debug.LogError($"ConflinctingTypes with {status}");
                    return true;
                }
            }

            return false;
        }

        public bool Contains(UnitStatus status)
            => _unitTags.Contains(UnitTagsCollection.GetUnitTag(status));

        public IEnumerator GetEnumerator()
            => _unitTags.GetEnumerator();

        public bool AddTag(UnitStatus status)
        {
            if (Contains(status) && !UnitTagsCollection.GetUnitTag(status).IsStackable)
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

        public void AddTemporarTag(UnitStatus status, float time, bool canStack = true)
        {
            if (!AddTag(status))
                return;

            _deleteQueue.Enqueue(status);

            Task.Delay((int)(time * 1000))
                .ContinueWith(o => RemoveQueuedTag());
        }

        private void RemoveQueuedTag()
            => RemoveTag(_deleteQueue.Dequeue());
    }
}