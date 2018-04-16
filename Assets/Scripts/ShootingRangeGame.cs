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


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public IEnumerator Game (){
        float time = duration;
        float timeSince = 0f;

        foreach (Target_Script target in targets) {
            target.StartCoroutine(target.BringDown());
        }


        while (time > 0) {
            if (timeSince >= timeBetween) {
                RiseRandomTarget();
            }

            yield return new WaitForSecondsRealtime(0.1f);
            time -= 0.1f;
        }
        audioSource.PlayOneShot(endSound);

        foreach (Target_Script target in targets)
        {
            target.StartCoroutine(target.BringDown());
        }
    }


    /// <summary>
    /// Rises a random target.
    /// </summary>
    public void RiseRandomTarget () {
        Target_Script target = null;

        while (target == null) {
            Target_Script temp = targets[Random.Range(0, targets.Length)].GetComponent<Target_Script>();
            if (temp.shotDown) {
                target = temp;
            }
        }

        target.StartCoroutine(target.BringUp());
    }

    /// <summary>
    /// Starts the game coroutine
    /// This is needed for buttons / events to be able to start the game
    /// </summary>
    public void StartGame () {
        StartCoroutine(Game());
        audioSource.PlayOneShot(startSound);
    }
}
