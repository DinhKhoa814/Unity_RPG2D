using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleAnimation : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        if (!anim) { return; }// k co anim return

        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);//tra ve thong tin cua animator  tren layer 0 
        anim.Play(state.fullPathHash,-1,Random.Range(0f,1f));//bat dau animation o 1 vi tri ngau nhien
    }

}
//state.fullPathHash đại diện cho trạng thái hiện tại) trên layer mặc định (-1 có nghĩa là không chỉ định layer cụ thể).