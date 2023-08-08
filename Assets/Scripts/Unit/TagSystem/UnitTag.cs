using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Unit.TagSystem
{
    [CreateAssetMenu(fileName = "Unit tag", menuName = "Unit Tag")]
    public class UnitTag : ScriptableObject, IEquatable<UnitTag>
    {
        [field: SerializeField] public UnitStatus Status { get; private set; }
        [field: SerializeField] public List<UnitStatus> ConflictingStatuses { get; private set; }
        [field: SerializeField] public bool IsStackable { get; private set; }

        public static implicit operator UnitStatus(UnitTag tag)
            => tag.Status;

        public static bool operator ==(UnitTag tag1, UnitTag tag2)
        {
            if (tag1 is null && tag2 is null)
                return true;

            if (tag1 is null || tag2 is null)
                return false;

            return tag1.Status == tag2.Status && tag1.IsStackable == tag2.IsStackable;
        }

        public static bool operator !=(UnitTag tag1, UnitTag tag2)
            => !(tag1 == tag2);

        public static bool operator ==(UnitTag tag, UnitStatus type)
            => tag.Status == type;

        public static bool operator !=(UnitTag tag, UnitStatus type)
            => !(tag == type);

        public bool Equals(UnitStatus type)
        {
            return Status == type;
        }

        public bool Equals(UnitTag tagObj)
        {
            if (tagObj == null)
                return false;

            return this == tagObj;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            UnitTag tagObj = obj as UnitTag;

            return Equals(tagObj);
        }

        public override int GetHashCode()
        {
            return Status.GetHashCode();
        }
    }
}
