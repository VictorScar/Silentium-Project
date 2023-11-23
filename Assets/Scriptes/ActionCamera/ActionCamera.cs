using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionCamera : MonoBehaviour
{
    [SerializeField] AnimationManager animationManager;


    [SerializeField] GameCharacter target;
    [SerializeField] Vector3 defaultOffset;
    [SerializeField] Vector3 attackOffset;
    [SerializeField] Vector3 entryOffset;
    [SerializeField] float attackSpeed = 20f;
    [SerializeField] float entrySpeed = 20f;
    // ActionManager actionManager;

    [SerializeField] Vector3 defaultPosition;

    bool isToTarget = false;

    void Start()
    {
        animationManager.onEntryStarted += EntryFlyby;
        animationManager.onAttackStarted += AttackCameraFlyby;
    }


    void DefaultOverview()
    {
        StartCoroutine(CameraHome(defaultPosition, 15));
    }

    public void AttackCameraFlyby(GameCharacter attackTarget)
    {
        if (target == null)
        {
            animationManager.onAttackStarted -= AttackCameraFlyby;
            return;
        }
        isToTarget = !isToTarget;

        if (isToTarget)
        {
            StartCoroutine(CameraMove(attackTarget.transform.position + attackOffset, 25));
        }
        else
        {
            DefaultOverview();
        }
    }


    void EntryFlyby(GameCharacter target)
    {
        StartCoroutine(EntryFlybyCoroutine(target, entrySpeed));
    }

    IEnumerator CameraMove(Vector3 targetPoint, float speed, float delay = 0)
    {
        while (transform.position != targetPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
            transform.LookAt(this.target.transform);
            yield return null;
        }

        yield return new WaitForSeconds(delay);
    }

    IEnumerator CameraHome(Vector3 targetPosition, float speed)
    {

        StartCoroutine(CameraMove(defaultPosition, 25));
        yield return null;



    }

    IEnumerator EntryFlybyCoroutine(GameCharacter target, float speed)
    {
        this.target = target;
        transform.position = target.transform.position + 2f * entryOffset;

        bool isEnded = false;
        Action checkEntryEnded = () => isEnded = true;

        animationManager.onEntryEnded += checkEntryEnded;

        while (!isEnded)
        {
            transform.LookAt(target.transform);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position + entryOffset, 5 * Time.deltaTime);
            yield return null;
        }

        animationManager.onEntryEnded -= checkEntryEnded;
        yield return CameraMove(target.transform.position - defaultOffset, speed);
        defaultPosition = transform.position;

    }
}
