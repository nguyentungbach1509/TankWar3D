using System.Collections;
using ICharacter;
using UnityEngine;
using UnityEngine.UI;

namespace HealthBar
{
    public class HealthBarController : MonoBehaviour
    {
        [SerializeField] Stats stats;
        [SerializeField] Image healthImage;
        [SerializeField] float updateSpeedSecond;
        [SerializeField] Transform target;
        private Transform mainCameraTransform;

        void Start()
        {
            stats.onHealthChange += HealthChange;
            mainCameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            if (target != null)
            {
                transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
            }

            transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward,
                mainCameraTransform.rotation * Vector3.up);
        }

        private void HealthChange(float percent)
        {
            StartCoroutine(HealthUpdate(percent));
        }

        private IEnumerator HealthUpdate(float percent)
        {
            updateSpeedSecond = 0.1f;
            float start = healthImage.fillAmount;
            float currentTime = 0;
            while (currentTime < updateSpeedSecond)
            {
                currentTime += Time.deltaTime;
                healthImage.fillAmount = Mathf.Lerp(start, percent, currentTime / updateSpeedSecond);
                yield return null;
            }

            healthImage.fillAmount = percent;
            if (percent == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}