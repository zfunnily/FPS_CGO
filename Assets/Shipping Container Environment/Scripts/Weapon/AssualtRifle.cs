using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Scripts.weapon
{
    public class AssualtRifle : Firearms
    {
        enum LayerIndex { Base, Reload, Aim};
        private IEnumerator reloadAmmoCheckCoroutine;
        protected override void Shooting()
        {
            if (CurrentAmmo <= 0) { return ;}
            if (!IsAllowShooting()) { return;}
            MuzzleParticle.Play();
            CurrentAmmo -= 1;
            GunAnimator.Play("Fire", IsAiming ? (int)LayerIndex.Aim: (int)LayerIndex.Base , 0);
            CreateBullet();
            CasingParticle.Play();
            LastFireTime = Time.time;
            //开火的声音
            FirearmsShootingAudioSource.clip = FirearmAudioData.ShootingAudio;
            FirearmsShootingAudioSource.Play();
        }
        protected override void Reload()
        {
            GunAnimator.SetLayerWeight((int)LayerIndex.Reload, 1);
            GunAnimator.SetTrigger(CurrentAmmo > 0 ? "ReloadLeft":"ReloadOutOf");
            
            //换弹夹的声音
            FirearmsReloadAudioSource.clip = CurrentAmmo > 0 ? FirearmAudioData.ReloadLeft: FirearmAudioData.ReloadOutOf;
            FirearmsReloadAudioSource.Play();

            //换弹夹的动作
            if (reloadAmmoCheckCoroutine == null)
            {
                reloadAmmoCheckCoroutine = CheckReloadAnimationEnd();
                StartCoroutine(reloadAmmoCheckCoroutine);
            }
            else
            {
                StopCoroutine(reloadAmmoCheckCoroutine);
                reloadAmmoCheckCoroutine = null;
                reloadAmmoCheckCoroutine = CheckReloadAnimationEnd();
                StartCoroutine(reloadAmmoCheckCoroutine);
            }
        }

        protected override void Aim()
        {
            GunAnimator.SetBool("Aim", IsAiming);
        }

        private void Update() {
            if (Input.GetMouseButton(0))
            {
                DoAttack();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
            if (Input.GetMouseButtonDown(1))
            {
                //瞄准
                IsAiming = true;
                Aim();
            }
            if (Input.GetMouseButtonUp(1))
            {
                //退出瞄准
                IsAiming = false;
                Aim();
            }
        }

        private void CreateBullet()
        {
            //枪口位置
            GameObject tmp_Bullet = Instantiate(BullePrefab, MuzzlePoint.position,MuzzlePoint.rotation);
            var tmp_BulletRigidbody = tmp_Bullet.GetComponent<Rigidbody>();
            tmp_BulletRigidbody.velocity = tmp_Bullet.transform.forward * 200f; //设置子弹射出去的速度
        }

        private IEnumerator CheckReloadAnimationEnd()
        {
            while(true)
            {
                yield return null;
                GunStateInfo =  GunAnimator.GetCurrentAnimatorStateInfo(LayerIndex.Reload);
                if (GunStateInfo.IsTag("ReloadAmmo"))
                {
                    if (GunStateInfo.normalizedTime > 0.95f)
                    {
                        //10 - (30 - 25)
                        int tmp_CurrentMaxAmmoCarried = CurrentMaxAmmoCarried - (AmmoInMag-CurrentAmmo);
                        if (tmp_CurrentMaxAmmoCarried <= 0 ) 
                        {
                            CurrentAmmo += CurrentMaxAmmoCarried;
                        }
                        else 
                        {
                            CurrentAmmo = AmmoInMag;
                        }
                        CurrentMaxAmmoCarried = tmp_CurrentMaxAmmoCarried <=0 ? 0 : tmp_CurrentMaxAmmoCarried;
                        yield break;
                    }
                }
            }

        }
    }
}