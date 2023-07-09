using System;
using UnityEngine;

namespace Towers
{
    public class Crossbow : Tower
    {

        [SerializeField] private Sprite unloadedCrossbow;
        [SerializeField] private Sprite loadedCrossbow;


        private void Start()
        {
            canAttack = true;
            OnReload += () => rotatablePart.GetComponentInChildren<SpriteRenderer>().sprite = loadedCrossbow;
        }
        

        protected override void Shoot()
        {
            base.Shoot();
            rotatablePart.GetComponentInChildren<SpriteRenderer>().sprite = unloadedCrossbow;
        }
    }
}