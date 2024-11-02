using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerManager : MonoBehaviour
{
    const string AnimatorDead = "isDead", AnimatorCheckpoint = "CheckpointActivation";
    const string DamageTag = "Damage", CheckpointTag = "Checkpoint";
    const string SceneLevel = "Level", Exit = "Exit", Entrance = "Entrance";
    const float SoundVolum = 1f;
    const int Offset = 1;

    public static PlayerManager player;
    public Animator playerAnimator;
    public AudioClip deathSound;

    private Transform checkPoint;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool canMove = true, lastSpriteFlipY;
    private float lastGravity;

    public void Awake()
    {
        if (player != null && player != this)
        {
            //Debug.Log("Destroy player");
            Destroy(this.gameObject);
        }
        else
        {
            //Debug.Log("Player is alive");
            player = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public bool CanPlayerMove()
    {
        return canMove;
    }

    public void SetCheckPoint(Transform newCheckPoint)
    {
        checkPoint = newCheckPoint;
    }

    public Transform GetCheckPoint()
    {
        return checkPoint;
    }

    private void SavePlayerData(Transform position)
    {
        SetCheckPoint(position);
        lastGravity = rb.gravityScale;
        lastSpriteFlipY = spriteRenderer.flipY;
    }

    private void SaveCheckpoint(Collider2D collision)
    {
        //Activamos la animacion del checkpoint.
        Animator checkpointAnimator = collision.gameObject.GetComponent<Animator>();
        checkpointAnimator.SetTrigger(AnimatorCheckpoint);

        //Guardamos los datos del juagdor.
        SavePlayerData(collision.transform);
        //Debug.Log("Checkpoint: " + transform.position + " " + lastGravity); ;
    }
    public void Respawn()
    {
        //El jugador aparece en el checkpoint con los datos que le pertenece.
        rb.gravityScale = lastGravity;
        spriteRenderer.flipY = lastSpriteFlipY;
        transform.position = GetCheckPoint().position;

        //Permitimos sus movimientos
        canMove = true;
        //Debug.Log("Respawn: " + transform.position + " " + lastGravity);
    }

    private void Death()
    {
        //Ejecuta el sonido una vez mientras se activa la animacion.
        audioSource.PlayOneShot(deathSound, SoundVolum);
        playerAnimator.SetBool(AnimatorDead, true);
        canMove = false;
        //Debug.Log("Death: " + transform.position + " " + lastGravity);
    }

    public void ChangeScene(string sceneName, string spawnpointName)
    {
        //Iniciar la corutina de cambio de escena.
        StartCoroutine(LoadSceneAndRespawn(sceneName, spawnpointName));
    }

    private IEnumerator LoadSceneAndRespawn(string sceneName, string spawnpointName)
    {
        //Cargamos la nueva escena de forma asíncrona.
        AsyncOperation newAsyncScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        //Esperamos hasta que la escena esté completamente cargada.
        while (!newAsyncScene.isDone)
        {
            yield return null;
        }

        //Guardamos los datos del jugador en el punto de spawn que puede ser entrada o salida.
        SavePlayerData(GameObject.Find(spawnpointName).transform);

        //Movemos al jugador a la posición de respawn en la nueva escena.
        Respawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string ActualScene = SceneManager.GetActiveScene().name;
        int ActualLevel = int.Parse(ActualScene.Substring(ActualScene.Length - Offset));


        if (collision.gameObject.CompareTag(DamageTag))
            Death();
        else if (collision.gameObject.CompareTag(CheckpointTag))
        {
            SaveCheckpoint(collision);
        }
        else if (collision.gameObject.name == Exit)
        {
            ChangeScene(SceneLevel + (ActualLevel + Offset), Entrance);
        }
        else if (collision.gameObject.name == Entrance)
        {
            ChangeScene(SceneLevel + (ActualLevel - Offset), Exit);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(DamageTag))
            Death();
    }
}
