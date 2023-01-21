using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityUtils.UIToolkit
{
    /// <summary>
    /// Provides a base for UI toolkit related code
    /// </summary>
    public class UIBehaviour : MonoBehaviour
    {
        [ReadOnly]
        public UIDocument Document;
        public VisualElement Root;

        protected virtual void Awake()
        {
            SetupUICallbacks();
        }

        protected virtual void OnEnable()
        {
            SetupUICallbacks();
        }

        /// <summary>
        /// Put queries and declare button events here
        /// </summary>
        protected virtual void SetupUICallbacks()
        {
            Document = GetComponent<UIDocument>();
            Root = Document.rootVisualElement;
        }
    }
}