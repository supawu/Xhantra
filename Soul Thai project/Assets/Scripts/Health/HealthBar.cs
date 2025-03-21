using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider mainSlider;   // แถบเลือดหลัก (สีแดง)
    public Slider delayedSlider; // แถบเลือดดีเลย์ (สีเหลือง)
    public float lerpSpeed = 1.5f; // ความเร็วลดของแถบเหลือง

    private void Start()
    {
        mainSlider.value = mainSlider.maxValue;
        delayedSlider.value = mainSlider.maxValue;
    }

    public void SetMaxHealth(int maxHealth)
    {
        mainSlider.maxValue = maxHealth;
        delayedSlider.maxValue = maxHealth;
        mainSlider.value = maxHealth;
        delayedSlider.value = maxHealth;
    }

    public void SetCurrentHealth(int currentHealth)
    {
        mainSlider.value = currentHealth; // แถบแดงลดลงทันที
        StartCoroutine(UpdateDelayedBar(currentHealth));
    }

    private IEnumerator UpdateDelayedBar(int targetHealth)
    {
        yield return new WaitForSeconds(0.2f); // รอให้แถบแดงลดก่อน

        while (delayedSlider.value > targetHealth)
        {
            delayedSlider.value = Mathf.Lerp(delayedSlider.value, targetHealth, Time.deltaTime * lerpSpeed);
            yield return null;
        }

        delayedSlider.value = targetHealth; // ตั้งค่าให้ตรงเป๊ะตอนสุดท้าย
    }
}
