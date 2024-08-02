using System.Collections;
using UnityEngine;

public class PlayerWaterDetection : MonoBehaviour
{
    public Transform[] characterList;
    private Animator characterAnim;
    private PlayerTouchMovement playerTouchMovementScript;
    private GridDetection gridDetection;
    public LayerMask waterLayer; // Su katmanýný belirleyin
    public float detectionDistance = 1f; // Su tespit mesafesi
    public float sinkingSpeed = 0.5f; // Karakterin suya düþme hýzý
    public float sinkingDepth = 8f; // Karakterin düþeceði mesafe
    public AudioSource waterSound; // Suya düþme ses kaynaðý
    public float soundDelay = 0.5f; // Sese gecikme süresi (saniye)

    private bool isSinking = false;
    private float initialYPosition;

    private void Start()
    {
        playerTouchMovementScript = GetComponent<PlayerTouchMovement>();
        gridDetection = GetComponent<GridDetection>();
        CharacterSelection characterSelectionScript = transform.Find("Characters").GetComponent<CharacterSelection>();
        int index = characterSelectionScript.index;

        if (index < 0 || index >= characterList.Length)
        {
            Debug.LogError("Geçersiz karakter index'i!");
            return;
        }

        characterAnim = characterList[index].GetComponent<Animator>();

        // Null kontrolü
        if (characterAnim == null)
        {
            Debug.LogError("Animator bileþeni bulunamadý!");
        }
        else
        {
            Debug.Log("Animator bileþeni baþarýyla alýndý.");
        }
    }


    void Update()
    {
        if (!isSinking)
        {
            DetectWater();
        }
    }

    void DetectWater()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit[] hits = Physics.RaycastAll(ray, detectionDistance);

        bool isWaterDetected = false;
        bool isNonWaterDetected = false;

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Door"))
            {
                continue; // "Door" tag'li nesneleri es geç
            }

            if (hit.collider.CompareTag("Water"))
            {
                isWaterDetected = true;
            }
            else
            {
                isNonWaterDetected = true;
            }
        }

        if (isWaterDetected && !isNonWaterDetected)
        {
            Debug.Log("Suya düþtü!");
            StartSinking();
        }
    }

    void StartSinking()
    {
        playerTouchMovementScript.enabled = false;
        gridDetection.enabled = false;
        transform.SetParent(null);
        isSinking = true;
        initialYPosition = transform.position.y;

        // Suya batma animasyonunu tetikle
        if (characterAnim != null)
        {
            Debug.Log("Animasyon parametresi 'Water Death' ayarlandý.");
            characterAnim.SetTrigger("Water Death");
        }
        else
        {
            Debug.LogWarning("Animator bileþeni ayarlanamadý!");
        }

            if (waterSound != null)
            {
                StartCoroutine(PlaySoundWithDelay(soundDelay));
            }

            StartCoroutine(Sink());
        }

    IEnumerator Sink()
    {
        while (transform.position.y > initialYPosition - sinkingDepth)
        {
            transform.position -= new Vector3(0, sinkingSpeed * Time.deltaTime, 0);
            yield return null;
        }

        isSinking = false;

        // Animasyon bitiminde yapýlmasý gerekenler varsa buraya eklenebilir.
        // Örneðin animasyon bitince baþka bir state'e geçiþ yapýlabilir.
        Debug.Log("Sinking tamamlandý.");
    }

    IEnumerator PlaySoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (waterSound != null)
        {
            waterSound.Play();
        }
    }
    }



