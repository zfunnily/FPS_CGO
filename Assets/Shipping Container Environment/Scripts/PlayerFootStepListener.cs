using System.Threading;
using System.Drawing;
using System.Numerics;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Color = UnityEngine.Color;
public class PlayerFootStepListener: MonoBehaviour
{
    public FootStepAudioData footStepAudioData;
    public AudioSource footStepAudioSource;
    private CharacterController characterController;
    private Transform footSetpTransform;
    private float nextPlayTime;

    //Q: 角色发出声音的必备条件
    //A: 角色移动或者发出较大幅度动作的时候

    //Q: 如何检测角色是否有异动
    //A: 用Physic API检测

    //Q: 如何实现角色踩踏位置对应材质的声音
    //A: 用Physic API检测

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        footSetpTransform = transform;
    }

    private void FixedUpdate() 
    {
        //判断角色是否落地
        if (characterController.isGrounded)
        {
            if (characterController.velocity.normalized.magnitude >= 0.1f)
            {
                nextPlayTime += Time.fixedDeltaTime;
                bool tmp_IsHit = Physics.Linecast(footSetpTransform.position, 
                footSetpTransform.position + Vector3.down * (characterController.height / 2 + characterController.skinWidth - characterController.center.y),
                out RaycastHit tmp_HitInfo);

                #if UNITY_EDITOR
                    Debug.DrawLine(footSetpTransform.position, 
                footSetpTransform.position + Vector3.down * (characterController.height / 2 + characterController.skinWidth - characterController.center.y),
                Color.red,
                0.25f);
                #endif
                if (tmp_IsHit)
                {
                    //TODO: 检测类型
                    foreach (var tmp_AudioElement in footStepAudioData.footSetpAudio)
                    {
                        if (tmp_HitInfo.collider.CompareTag(tmp_AudioElement.tag) && nextPlayTime >= tmp_AudioElement.delay)
                        {
                            //TODO 根据移动状态来判断播放对应的移动声音
                            //播放移动声音
                            int tmp_AudioIndex = UnityEngine.Random.Range(0,tmp_AudioElement.audioClips.Count);
                            AudioClip tmp_FootStepAudioClip = tmp_AudioElement.audioClips[tmp_AudioIndex];
                            footStepAudioSource.clip = tmp_FootStepAudioClip;
                            footStepAudioSource.Play();
                            nextPlayTime = 0;
                            break;
                        }
                    }
                }
            }
        }
    }
}
