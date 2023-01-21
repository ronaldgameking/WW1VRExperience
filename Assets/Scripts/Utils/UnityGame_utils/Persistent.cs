using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityUtils
{
    public enum HideMethod
    {
        Automatic = 0,
        Manual,
        MLock
    }

    public enum FinishAction
    {
        Disable = 0,
        Keep,
        Delete
    }

    public enum DuplicateAction
    {
        KeepOld = 0,
        ReplaceOld
    }

    [Guid("96706864-81BF-472C-A4FD-72E1349E8A9F")]
    public class Persistent : MonoBehaviour
    {

        [FormerlySerializedAs("preventDuplicates")] [Tooltip("Prevent duplicate objects of this type")]
        public bool PreventDuplicates = true;
        [FormerlySerializedAs("duplicateAction")] [DrawIf("preventDuplicates", true, ComparisonType.Equals, DisablingType.ReadOnly)]
        public DuplicateAction DuplicateAction = DuplicateAction.KeepOld;
        [FormerlySerializedAs("hideMethod")] [Tooltip("The method of hiding the object. \n automatic = after fixed seconds. \n manual = by function or trigger")]
        public HideMethod HideMethod = HideMethod.Automatic;
        [FormerlySerializedAs("hideDelay")]
        [Range(1.0f, 100f)]
        [Tooltip("The delay before the object becomes hidden (ignored if manual/lock method)")]
        public float HideDelay = 3.0f;
        [FormerlySerializedAs("doFinish")] [Tooltip("The action that is done after the delay (ignored if manual/lock method) \n keep = keep the object as is. \n disable = keep obj but invisible (setActive) \n del = destroy the object")]
        public FinishAction DoFinish = FinishAction.Disable;
        public bool DebugLogging = false;

        //static instance of this class, used to prevent more than 1 of the same object
        [HideInInspector]
        public static Persistent Instance;
        [HideInInspector]
        public bool IsLocked = false;

        private static bool PrevDupe
        {
            get
            {
                return Instance.PreventDuplicates;
            }
        }
        void Awake()
        {
            if (PreventDuplicates)
            {
                switch (Instance)
                {
                    case null:
                        if (DebugLogging)
                            Debug.Log("No instance found, creating 1");
                        Instance = this;
                        //objLink 
                        //currentScene = SceneManager.GetActiveScene();
                        break;
                    default:
                        if (DebugLogging)
                            Debug.Log("Instance found");
                        switch (DuplicateAction)
                        {
                            case DuplicateAction.KeepOld:
                                Destroy(gameObject);
                                break;
                            case DuplicateAction.ReplaceOld:
                                Destroy(Instance.gameObject);
                                Instance = this;
                                break;
                            default:
                                break;
                        }
                        break;
                }
            }
            DontDestroyOnLoad(this);
            if (DebugLogging) Debug.LogWarning("Object made persistent");

            switch (HideMethod)
            {
                case HideMethod.Automatic:
                    Invoke(nameof(AutoObj), HideDelay);
                    break;
                case HideMethod.Manual:
                    break;
                case HideMethod.MLock:
                    IsLocked = true;
                    break;
            }
        }
        void AutoObj()
        {
            switch (DoFinish)
            {
                case FinishAction.Keep:
                    break;
                case FinishAction.Disable:
                    gameObject.SetActive(false);
                    break;
                case FinishAction.Delete:
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
