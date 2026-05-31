using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    private AudioSource audioSource;
    public AudioClip[] clips;
    private void Start()
    {
        anim = GetComponent<Animator>();
        state = GetComponent<State>();
        audioSource = GetComponent<AudioSource>();
        state.LoadValue(Path.SkeletonValuePath);
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        behaviour = Behaviour.Idle;

        //¹¥»÷
        if (Vector3.Distance(GameController.instance.GetPlayerPosition(), transform.position) < state.value.attackDistance && timer - lastAttackTime > state.value.attackSpan)
        {
            audioSource.clip = clips[1];
            audioSource.Play();
            SetBehavior(Behaviour.Attack);
        }

        //¾¯½ä×·»÷
        JudgeAlarmed(GameController.instance.GetPlayerPosition(), state.value.alarmDistance);
        if (isAlarmed)
        {   
            if(state.canMove)
            {
                target = GameController.instance.GetPlayerPosition();
                SetBehavior(Behaviour.GoToTarget);
            }
        }
        else
        {
            SetBehavior(Behaviour.Circle);
        }

        //ÊÜ»÷
        if (GetComponent<State>().isAttacked)
        {
            audioSource.clip = clips[0];
            audioSource.Play();
            SetBehavior(Behaviour.Attacked);
        }

        //ËÀÍö
        if(GetComponent<State>().value.presentHP<=0)
        {
            SetBehavior(Behaviour.Die);
        }

        AnimtorControl(behaviour);
        BehaviorControl(behaviour);
    }
}
