using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRangeGame : MonoBehaviour {

    [Tooltip("Duration of the game")]
    public float duration;

    [Tooltip("The time between targets rising")]
    public float timeBetween;

    /*
    [Tooltip("Max time the target stays up if not shot down")]
    public float maxTimeUp;
    */

    [Tooltip("References to the targets")]
    public Target_Script[] targets;


    public AudioClip startSound, endSound;
    private AudioSource audioSource;

    private bool started;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    float timeSince = 0f;

    public IEnumerator Game (){
        float time = duration;

        foreach (Target_Script target in targets) {
            target.MakeFall();
            //target.StartCoroutine(target.BringDown());
        }

        while (time > 0) {
            if (timeSince >= timeBetween) {
                RiseRandomTarget();
            }

            yield return new WaitForSecondsRealtime(0.1f);
            time -= 0.1f;
            timeSince += 0.1f;
        }

        foreach (Target_Script target in targets)
        {
            target.MakeFall();
            //target.StartCoroutine(target.BringDown());
        }

        audioSource.PlayOneShot(endSound);
        started = false;
    }


    /// <summary>
    /// Rises a random target.
    /// </summary>
    public void RiseRandomTarget () {
        Target_Script target = targets[Random.Range(0, targets.Length)].GetComponent<Target_Script>();
        if (target.isDown == false){
            return;
        } 

        //target.StopAllCoroutines();
        //target.StartCoroutine(target.BringUp();
        target.TargetRise();
        timeSince = 0f;
        Debug.Log("rose a target");
    }

    /// <summary>
    /// Starts the game coroutine
    /// This is needed for buttons / events to be able to start the game
    /// </summary>
    public void StartGame () {
        if (!started)
        {
            StartCoroutine(Game());
            audioSource.PlayOneShot(startSound);
            started = true;
            Debug.Log("Started game");
        }
    }
}
