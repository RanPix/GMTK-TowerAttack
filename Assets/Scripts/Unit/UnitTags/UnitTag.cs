using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Unit.UnitTags
{
    [CreateAssetMenu(fileName = "Unit", menuName = "Unit Tags")]
    public class UnitTag : ScriptableObject
    {
        public readonly UnitStatus Type;
        public readonly List<UnitStatus> ConflictingTypes;
        public readonly bool IsStackAble;

        public static implicit operator UnitStatus(UnitTag tag)
            => tag.Type;

        public static bool operator ==(UnitTag tag1, UnitTag tag2)
            => tag1.Type == tag2.Type && tag1.IsStackAble == tag2.IsStackAble;

        public static bool operator !=(UnitTag tag1, UnitTag tag2)
            => !(tag1 == tag2);

        public static bool operator ==(UnitTag tag, UnitStatus type)
            => tag.Type == type;

        public static bool operator !=(UnitTag tag, UnitStatus type)
            => !(tag == type);

        public bool Equals(UnitStatus type)
        {
            return Type == type;
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
            return Type.GetHashCode();
        }
    }
}
