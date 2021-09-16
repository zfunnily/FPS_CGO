using System.Threading;
using UnityEngine;
namespace Scripts.weapon
{
    public abstract class Firearms : MonoBehaviour, IWeapon
    {
        public Transform MuzzlePoint; //枪口位置
        public Transform CasingPoing; //抛射的位置
        public ParticleSystem MuzzleParticle;// 枪焰
        public ParticleSystem CasingParticle;//弹壳

        public float FireRate; //开火速度 s/发
        public int AmmoInMag = 30;//弹夹
        public int MaxAmmoCarried = 120; //多少发子弹


        protected int CurrentAmmo;
        protected int CurrentMaxAmmoCarried;
        public float lastFireTime; //最后一次开火时间；

        protected Animator GunAnimator;

        //射击
        protected abstract void Shooting();
        protected abstract void Reload();

        protected virtual void Start()
        {
            CurrentAmmo = AmmoInMag;
            CurrentMaxAmmoCarried = MaxAmmoCarried;
        }

        public void DoAttack() 
        {
            if (CurrentAmmo <= 0) { Reload(); return ;}
            if (!IsAllowShooting()) { return;}
            CurrentAmmo -= 1;
            Shooting();
            lastFireTime = Time.time;
        }
        private bool IsAllowShooting()
        {
            return Time.time - lastFireTime > 1/FireRate;
        }

    }
}