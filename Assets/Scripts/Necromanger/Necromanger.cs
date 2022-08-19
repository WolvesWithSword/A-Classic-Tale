using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Necromanger : MonoBehaviour, IInteractable 
{
    public float phase1Time = 20f;
    public float phase2Time = 30f;
    public float phase3Time = 35f;
    public float weakTime = 5f;
    public int life = 3;

    public GameObject zombieShield;
    public ZombieCirclePattern zombieCirclePattern;
    public ZombieGeneratorPattern zombieSpiralPattern;
    public ZombieGeneratorPattern zombieSlalomPattern;
    public BossDoorSystem bossDoor;

    public Transform pushPoint;
    public Tilemap foreground;

    private SlashEffect slashEffect;
    private DialogueTrigger deathDialogue;

    private bool havePatternRunning = false;
    private int phase = 0;
    private bool isNecromangerWeak = false;
    private IEnumerator currentPhase;
    private bool playerHasAxe = false;
    private bool isDeathDialogueStarted = false;

    private void Start()
    {
        if (GameEventManager.Instance.hasDefeatedNecromanger)
        {
            bossDoor.DeactivateDoor(true);
            foreground.SetTile(foreground.WorldToCell(transform.position), null);//We can go to his case
            Destroy(gameObject);
            return;
        }
        deathDialogue = GetComponent<DialogueTrigger>();
        deathDialogue.onDialogueEnd = BossDie;
        slashEffect = GetComponentInChildren<SlashEffect>();
        playerHasAxe = PlayerManager.Instance.playerStats.HasAxe;
        bossDoor.onDoorClosing = OnBossStart;
        AudioManager.Instance.StopPlayingSong();
    }

    // Update is called once per frame
    void Update()
    {
        if (life == 0)
        {
            if (!isDeathDialogueStarted)
            {
                RunDeathDialogue();
            }
            return;
        }

        if (isNecromangerWeak || havePatternRunning) return;

        if (phase == 1)
        {
            StartCoroutine(RunPhase(zombieCirclePattern, phase1Time));
        }

        if (phase == 2)
        {
            StartCoroutine(RunPhase(zombieSpiralPattern, phase2Time));
        }

        if (phase == 3)
        {
            StartCoroutine(RunPhase(zombieSlalomPattern, phase3Time));
        }
        if (phase > 3)
        {
            int choice = Random.Range(1, 4);
            switch (choice)
            {
                case 1:
                    StartCoroutine(RunPhase(zombieCirclePattern, phase1Time));
                    break;
                case 2:
                    StartCoroutine(RunPhase(zombieSpiralPattern, phase2Time));
                    break;
                case 3:
                    StartCoroutine(RunPhase(zombieSlalomPattern, phase3Time));
                    break;
                default:
                    break;
            }
        }
    }

    private IEnumerator RunPhase(IZombiePattern pattern,float runningTime)
    {
        if (pattern is ZombieGeneratorPattern)
        {
            (pattern as ZombieGeneratorPattern).StopInvoking = false;
        }
        currentPhase = pattern.RunPattern();
        StartCoroutine(currentPhase);
        havePatternRunning = true;

        yield return new WaitForSeconds(runningTime);

        pattern.CleanPattern();
        StopCoroutine(currentPhase);
        havePatternRunning = false;
        phase++;

        currentPhase = NecromangerWeakPhase();
        StartCoroutine(currentPhase);
    }

    private IEnumerator NecromangerWeakPhase()
    {
        isNecromangerWeak = true;
        zombieShield.SetActive(false);
        yield return new WaitForSeconds(weakTime);
        StartCoroutine(RestoreShield());
    }

    private IEnumerator RestoreShield()
    {
        yield return new WaitForSeconds(0.2f);
        StopCoroutine(currentPhase);
        PlayerManager.Instance.PushPlayerTo(pushPoint.position, 17f, EPlayerPosition.UP);
        yield return new WaitForSeconds(0.5f);

        zombieShield.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        isNecromangerWeak = false;
    }

    public void Interact()
    {
        if (isNecromangerWeak && playerHasAxe)
        {
            slashEffect.PlaySlashAnimation();
            StartCoroutine(RestoreShield());
            life--;
        }
    }

    private void OnBossStart()
    {
        StartCoroutine(BossStart());
    }

    private IEnumerator BossStart()
    {
        zombieShield.SetActive(true);
        AudioManager.Instance.PlayBossSong();
        yield return new WaitForSeconds(0.5f);
        phase++;
    }

    private void RunDeathDialogue()
    {
        CameraShake.Instance.StartShake(1.5f, 0.08f, 150);
        StopAllCoroutines();
        deathDialogue.TriggerDialogue();
        isDeathDialogueStarted = true;
    }

    private void BossDie()
    {
        bossDoor.OpenDoor();
        AudioManager.Instance.PlayAmbiantSong();
        foreground.SetTile(foreground.WorldToCell(transform.position), null);//We can go to his case
        GameEventManager.Instance.hasDefeatedNecromanger = true;
        Destroy(gameObject);
    }
}
