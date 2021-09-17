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
            GunAnimator = GetComponent<Animator>();
        }

        public void DoAttack() 
        {
            if (CurrentAmmo <= 0) { return ;}
            if (!IsAllowShooting()) { return;}
            CurrentAmmo -= 1;
            GunAnimator.Play("Fire", 0, 0);
            Shooting();
            CreateBullet();
            lastFireTime = Time.time;
        }
        private bool IsAllowShooting()
        {
            return Time.time - lastFireTime > 1/FireRate;
        }

        private void CreateBullet()
        {
            //枪口位置
            GameObject tmp_Bullet = Instantiate(BullePrefab, MuzzlePoint.position,MuzzlePoint.rotation);
            var tmp_BulletRigidbody = tmp_Bullet.GetComponent<Rigidbody>();
            Debug.Log(tmp_Bullet.transform);
            tmp_BulletRigidbody.velocity = tmp_Bullet.transform.forward * 200f; //设置子弹射出去的速度
        }

    }
}