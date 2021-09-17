using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
namespace Scripts.weapon
{
    public class AssualtRifle : Firearms
    {
        protected override void Shooting()
        {
            Debug.Log("Shooting");
        }
        protected override void Reload()
        {
            Debug.Log("Reload");
        }

        private void Update() {
            if (Input.GetMouseButton(0))
            {
                DoAttack();
            }
        }
    }
}