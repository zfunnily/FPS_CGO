using System;
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

        public float FireRate = 11.7f; //开火速度 s/发
        public int AmmoInMag = 30;//弹夹
        public int MaxAmmoCarried = 120; //多少发子弹
        public GameObject BullePrefab; //子弹的 prefab
        
        public Camera EyeCamera; //瞄准后的Camera

        //给开枪加声音
        public AudioSource FirearmsShootingAudioSource;
        //换弹夹声音
        public AudioSource FirearmsReloadAudioSource;

        public FirearmAudioData FirearmAudioData;


        protected int CurrentAmmo;
        protected int CurrentMaxAmmoCarried;
        public float LastFireTime; //最后一次开火时间；

        protected Animator GunAnimator;
        protected AnimatorStateInfo GunStateInfo;
        protected float OriginFov;
        protected bool IsAiming; 

        
        protected abstract void Shooting();//射击
        protected abstract void Reload();//换弹夹
        protected abstract void Aim(); //瞄准

        protected virtual void Start()
        {
            CurrentAmmo = AmmoInMag;
            CurrentMaxAmmoCarried = MaxAmmoCarried;
            GunAnimator = GetComponent<Animator>();
            OriginFov = EyeCamera.fieldOfView;
        }

        public void DoAttack() 
        {
            Shooting();
        }
        public bool IsAllowShooting()
        {
            return Time.time - LastFireTime > 1/FireRate;
        }

    }
}