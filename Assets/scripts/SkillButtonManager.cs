using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  // DOTween kütüphanesini ekleyin

public class SkillButtonManager : MonoBehaviour
{
    public Button skill1Button; // Skill 1 düðmesi
    public Button skill2Button; // Skill 2 düðmesi
    public Button skill3Button; // Skill 3 düðmesi
    public float cooldownTime = 5f; // Her düðme için cooldown süresi
    private int score = 0; // Puan

    // Skill 2 için parçacýk sistemi
    public ParticleSystem fireballEffect; // Ateþ topu efekti
    public ParticleSystem shockwaveEffect; // Þok dalgasý efekti

    void Start()
    {
        // Baþlangýçta parçacýk efektlerini kapat
        fireballEffect.Stop();
        shockwaveEffect.Stop();

        // Düðme týklama iþlemlerini baðla
        skill1Button.onClick.AddListener(() => OnSkillButtonPressed(skill1Button, "Skill 1 kullanýldý!"));
        skill2Button.onClick.AddListener(() => OnSkillButtonPressed(skill2Button, "Skill 2 kullanýldý!"));
        skill3Button.onClick.AddListener(() => OnSkillButtonPressed(skill3Button, "Skill 3 kullanýldý!"));
    }

    void OnSkillButtonPressed(Button button, string message)
    {
        // Eðer düðme devre dýþýysa týklanmasýna izin verme
        if (!button.interactable)
            return;

        // Beceriyi aktif et (örneðin, konsola bir mesaj yazdýr)
        UnityEngine.Debug.Log(message);

        // 1. yetenek týklanýnca "spawnedObject" etiketine sahip objeleri yok et ve puan ekle
        if (button == skill1Button)
        {
            ActivateFireballEffect();
            ActivateShockwaveEffect();
            DestroySpawnedObjectsAndAddScore();
        }

        // 2. yetenek: Ateþ topu efekti ve animasyon baþlat
        if (button == skill2Button)
        {
            StartCoroutine(ActivateFireballEffectAndDestroyObjects());
        }

        // 3. yetenek: Þok dalgasý efekti ve animasyon baþlat
        if (button == skill3Button)
        {
            ActivateShockwaveEffect();
        }

        // Düðmeyi devre dýþý býrak
        button.interactable = false;

        // Cooldown baþlat
        StartCoroutine(CooldownCoroutine(button));

        // Buton animasyonu ekleyelim (ölçek deðiþimi)
        AnimateButton(button);
    }
    IEnumerator ActivateFireballEffectAndDestroyObjects()
    {
        // Ateþ topu efektini baþlat
        ActivateFireballEffect();

        // 0.5 saniye bekle
        yield return new WaitForSeconds(1f);

        // Sonra tüm objeleri yok et
        DestroyAllSpawnedObjects();
    }

    void DestroySpawnedObjectsAndAddScore()
    {
        // "spawnedObject" etiketine sahip tüm objeleri bul
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("spawnedObject");

        // Obje sayýsýný al
        int objectCount = spawnedObjects.Length;

        // Obje sayýsý / 2 * 10 puan ekle
        int pointsToAdd = (objectCount / 2) * 10;

        // Skoru güncelle
        score += pointsToAdd;
        GameManager.Instance.AddScore(score);
        score = 0;

        // Objeleri yok etmeden önce animasyon ekleyelim
        foreach (GameObject spawnedObject in spawnedObjects)
        {
            // Obje üzerinde animasyon yapalým (ölçeklendirme, döndürme, þeffaflýk)
            AnimateSpawnedObject(spawnedObject);
        }
    }

    void DestroyAllSpawnedObjects()
    {
        // "spawnedObject" etiketine sahip tüm objeleri bul
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("spawnedObject");

        // Objeleri yok etmeden önce animasyon ekleyelim
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
        // Null kontrolü ekleyelim (objenin hâlâ var olduðundan emin olalým)
        if (spawnedObject == null)
            return;

        // Objeye patlama efekti ekleyelim (objenin ölçeðini hýzlýca artýrýp azaltma)
        spawnedObject.transform.DOScale(Vector3.one * 10f, 0.5f) // Hýzla büyür
            .SetEase(Ease.InFlash) // Patlama gibi hýzlý bir etki
            .OnComplete(() =>
            {
                // Patlama efekti sonrasý tekrar orijinal boyuta döner
                spawnedObject.transform.DOScale(Vector3.one, 7f)
                    .SetEase(Ease.OutElastic);
            });

        // Parçacýk sistemi baþlatma (Null kontrolü ile)
        ParticleSystem particleSystem = spawnedObject.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Play(); // Parçacýk efekti baþlat
        }

        // Zamanlý yok etme iþlemi, animasyon bitiminde
        Destroy(spawnedObject, 2f); // 3 saniye sonra objeyi yok et
    }

    void ActivateFireballEffect()
    {
        // Ateþ topu parçacýklarýný baþlat
        fireballEffect.Play();

        // Ateþ topunu döndürme animasyonu
        fireballEffect.transform.DORotate(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart);  // Sonsuz bir þekilde döndürme

        // 1 saniye sonra döndürmeyi durdur
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

        // Ýki obje seç (ilk ikisini alýyoruz, eþlenik olduklarýný varsayýyoruz)
        if (spawnedObjects.Length >= 2)
        {
            GameObject firstObject = spawnedObjects[0];
            GameObject secondObject = spawnedObjects[1];

            // Objeleri büyütüp küçültürken animasyonlarý düzgün yapalým
            firstObject.transform.DOScale(Vector3.one * 7f, 1f) // Büyütme
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    // Küçültme iþlemi bittiðinde parçacýk efektini kapat
                    shockwaveEffect.Stop();
                });

            secondObject.transform.DOScale(Vector3.one * 7f, 1f) // Büyütme
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    // Küçültme iþlemi bittiðinde parçacýk efektini kapat
                    shockwaveEffect.Stop();
                });
        }

        // Þok dalgasý parçacýklarýný baþlat
        shockwaveEffect.Play();
    }

    void AnimateButton(Button button)
    {
        // Butonun þeffaflýðýný deðiþtirerek fade animasyonu ekleyelim
        button.GetComponent<Image>().DOFade(0.5f, 0.2f)  // Þeffaflýðý %50 yapalým, 0.2 saniye sürsün
            .OnComplete(() => button.GetComponent<Image>().DOFade(1f, 0.2f)); // Tekrar tam þeffaflýk
    }

    IEnumerator CooldownCoroutine(Button button)
    {
        // Cooldown süresi kadar bekle
        yield return new WaitForSeconds(cooldownTime);

        // Düðmeyi tekrar aktif hale getir
        button.interactable = true;
    }
}
