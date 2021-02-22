using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Assets.SimpleLocalization
{
    /// <summary>
    /// Localize text mesh pro component.
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizedTextMesh : MonoBehaviour
    {
        public string LocalizationKey;

        public void Start()
        {
            Localize();
            LocalizationManager.LocalizationChanged += Localize;
        }

        public void OnDestroy()
        {
            LocalizationManager.LocalizationChanged -= Localize;
        }

        private void Localize()
        {
            GetComponent<TMP_Text>().text = LocalizationManager.Localize(LocalizationKey);
        }
    }
}