using System.Collections;
using UnityEngine;
using System.Collections.Generic; // Buraya eklendi

using UnityEngine.UI;
using DG.Tweening;  // DOTween k�t�phanesini ekleyin

public class SkillButtonManager : MonoBehaviour
{
    public Button skill1Button; // Skill 1 d��mesi
    public Button skill2Button; // Skill 2 d��mesi
    public Button skill3Button; // Skill 3 d��mesi
    public float cooldownTime = 5f; // Her d��me i�in cooldown s�resi
    private int score = 0; // Puan

    // Skill 2 i�in par�ac�k sistemi
    public ParticleSystem fireballEffect; // Ate� topu efekti
    public ParticleSystem shockwaveEffect; // �ok dalgas� efekti

    void Start()
    {
        // Ba�lang��ta par�ac�k efektlerini kapat
        fireballEffect.Stop();
        shockwaveEffect.Stop();

        // D��me t�klama i�lemlerini ba�la
        skill1Button.onClick.AddListener(() => OnSkillButtonPressed(skill1Button, "Skill 1 kullan�ld�!"));
        skill2Button.onClick.AddListener(() => OnSkillButtonPressed(skill2Button, "Skill 2 kullan�ld�!"));
        skill3Button.onClick.AddListener(() => OnSkillButtonPressed(skill3Button, "Skill 3 kullan�ld�!"));
    }

    void OnSkillButtonPressed(Button button, string message)
    {
        // E�er d��me devre d���ysa t�klanmas�na izin verme
        if (!button.interactable)
            return;

        // Beceriyi aktif et (�rne�in, konsola bir mesaj yazd�r)
        UnityEngine.Debug.Log(message);

        // 1. yetenek t�klan�nca "spawnedObject" etiketine sahip objeleri yok et ve puan ekle
        if (button == skill1Button)
        {
            ActivateFireballEffect();
            ActivateShockwaveEffect();
            DestroySpawnedObjectsAndAddScore();
        }

        // 2. yetenek: Ate� topu efekti ve animasyon ba�lat
        if (button == skill2Button)
        {
            StartCoroutine(ActivateFireballEffectAndShuffleObjects());
        }

        // 3. yetenek: �ok dalgas� efekti ve animasyon ba�lat
        if (button == skill3Button)
        {
            ActivateShockwaveEffect();
        }

        // D��meyi devre d��� b�rak
        button.interactable = false;

        // Cooldown ba�lat
        StartCoroutine(CooldownCoroutine(button));

        // Buton animasyonu ekleyelim (�l�ek de�i�imi)
        AnimateButton(button);
    }

    IEnumerator ActivateFireballEffectAndShuffleObjects()
    {
        // Ate� topu efektini ba�lat
        ActivateFireballEffect();

        // 0.5 saniye bekle
        yield return new WaitForSeconds(0.5f);

        // Objeleri kar��t�r ve tekrar spawn et
        ShuffleSpawnedObjects();
    }
    void ShuffleSpawnedObjects()
    {
        // "spawnedObject" etiketine sahip t�m objeleri bul
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("spawnedObject");

        // E�er obje yoksa i�lemi sonland�r
        if (spawnedObjects.Length == 0)
            return;

        // Rastgele pozisyonlar olu�tur
        List<Vector3> originalPositions = new List<Vector3>();
        foreach (GameObject obj in spawnedObjects)
        {
            originalPositions.Add(obj.transform.position);
        }

        // Pozisyonlar� kar��t�r
        System.Random random = new System.Random();
        for (int i = originalPositions.Count - 1; i > 0; i--)
        {
            int randomIndex = random.Next(0, i + 1);
            Vector3 temp = originalPositions[i];
            originalPositions[i] = originalPositions[randomIndex];
            originalPositions[randomIndex] = temp;
        }

        // Objeleri yeni pozisyonlara ta�� ve yukar�dan b�rak
        for (int i = 0; i < spawnedObjects.Length; i++)
        {
            Vector3 targetPosition = originalPositions[i];
            Vector3 startPosition = new Vector3(targetPosition.x, targetPosition.y + 3f, targetPosition.z); // Hedef pozisyonun �zerine ��k

            // Objeyi �nce yukar� ta��
            spawnedObjects[i].transform.position = startPosition;

            // Yumu�ak bir �ekilde hedef konuma d���r
            spawnedObjects[i].transform.DOMove(targetPosition, 1f)
                .SetEase(Ease.OutBounce); // D����e sekme efekti ekle
        }
    }

    void DestroySpawnedObjectsAndAddScore()
    {
        // "spawnedObject" etiketine sahip t�m objeleri bul
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("spawnedObject");

        // Obje say�s�n� al
        int objectCount = spawnedObjects.Length;

        // Obje say�s� / 2 * 10 puan ekle
        int pointsToAdd = (objectCount / 2) * 10;

        // Skoru g�ncelle
        score += pointsToAdd;
        GameManager.Instance.AddScore(score);
        score = 0;

        // Objeleri yok etmeden �nce animasyon ekleyelim
        foreach (GameObject spawnedObject in spawnedObjects)
        {
            // Obje �zerinde animasyon yapal�m (�l�eklendirme, d�nd�rme, �effafl�k)
            AnimateSpawnedObject(spawnedObject);
        }
    }

    void DestroyAllSpawnedObjects()
    {
        // "spawnedObject" etiketine sahip t�m objeleri bul
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("spawnedObject");

        // Objeleri yok etmeden �nce animasyon ekleyelim
        foreach (GameObject spawnedObject in spawnedObjects)
        {
            AnimateSpawnedObject(spawnedObject); // Objeyi animasyonla yok et
        }

        // Objeleri yok et
        foreach (GameObject spawnedObject in spawnedObjects)
        {
            Destroy(spawnedObject); // Objeyi yok et
        }
    }

    void AnimateSpawnedObject(GameObject spawnedObject)
    {
        // Null kontrol� ekleyelim (objenin h�l� var oldu�undan emin olal�m)
        if (spawnedObject == null)
            return;

        // Objeye patlama efekti ekleyelim (objenin �l�e�ini h�zl�ca art�r�p azaltma)
        spawnedObject.transform.DOScale(Vector3.one * 10f, 0.5f) // H�zla b�y�r
            .SetEase(Ease.InFlash) // Patlama gibi h�zl� bir etki
            .OnComplete(() =>
            {
                // Patlama efekti sonras� tekrar orijinal boyuta d�ner
                spawnedObject.transform.DOScale(Vector3.one, 7f)
                    .SetEase(Ease.OutElastic);
            });

        // Par�ac�k sistemi ba�latma (Null kontrol� ile)
        ParticleSystem particleSystem = spawnedObject.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Play(); // Par�ac�k efekti ba�lat
        }

        // Zamanl� yok etme i�lemi, animasyon bitiminde
        Destroy(spawnedObject, 2f); // 3 saniye sonra objeyi yok et
    }

    void ActivateFireballEffect()
    {
        // Ate� topu par�ac�klar�n� ba�lat
        fireballEffect.Play();

        // Ate� topunu d�nd�rme animasyonu
        fireballEffect.transform.DORotate(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart);  // Sonsuz bir �ekilde d�nd�rme

        // 1 saniye sonra d�nd�rmeyi durdur
        DOVirtual.DelayedCall(1f, () =>
        {
            fireballEffect.transform.DORotate(Vector3.zero, 1.5f); // 0.5 saniyede durdur
            fireballEffect.Stop();

        });
    }


  void ActivateShockwaveEffect()
{
    // "spawnedObject" etiketine sahip iki objeyi bul
    GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("spawnedObject");

    // �ki obje se� (ilk ikisini al�yoruz, e�lenik olduklar�n� varsay�yoruz)
    if (spawnedObjects.Length >= 2)
    {
        GameObject firstObject = spawnedObjects[0];
        GameObject secondObject = spawnedObjects[1];

        // Objeleri z�platma (bounce) hareketi yapt�r
        firstObject.transform.DOPunchScale(Vector3.one * 2.5f, 1.5f, 5, 0.4f) // Daha uzun ve yo�un z�plama
            .OnComplete(() =>
            {
                shockwaveEffect.Stop(); // Z�plama hareketi tamamland���nda efekt durdur
            });

        secondObject.transform.DOPunchScale(Vector3.one * 2.5f, 1.5f, 5, 0.4f) // Daha uzun ve yo�un z�plama
            .OnComplete(() =>
            {
                shockwaveEffect.Stop(); // Z�plama hareketi tamamland���nda efekt durdur
            });
    }

    // �ok dalgas� par�ac�klar�n� ba�lat
    shockwaveEffect.Play();
}



    void AnimateButton(Button button)
    {
        // Butonun �effafl���n� de�i�tirerek fade animasyonu ekleyelim
        button.GetComponent<Image>().DOFade(0.5f, 0.2f)  // �effafl��� %50 yapal�m, 0.2 saniye s�rs�n
            .OnComplete(() => button.GetComponent<Image>().DOFade(1f, 0.2f)); // Tekrar tam �effafl�k
    }

    IEnumerator CooldownCoroutine(Button button)
    {
        // Cooldown s�resi kadar bekle
        yield return new WaitForSeconds(cooldownTime);

        // D��meyi tekrar aktif hale getir
        button.interactable = true;
    }
}
