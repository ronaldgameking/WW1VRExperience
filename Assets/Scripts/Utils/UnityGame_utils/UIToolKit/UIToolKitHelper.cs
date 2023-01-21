using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityUtils.UIToolkit
{
    //Summary:
    //  Methods to simplify 
    public static class UIToolKitHelper
    {
        public static void ChangeActiveUIDocument(UIDocument prev, UIDocument next)
        {
            next.gameObject.SetActive(true);
            prev.gameObject.SetActive(false);
        }

        // public static void FormatUIDoucement(UIDocument doc, string[] format)
        // {
        //     throw new NotImplementedException();
        //     VisualElement root = doc.rootVisualElement;
        //     IEnumerable<VisualElement> childen = root.Children();
        // }

        public static void OpenOnTop(UIDocument under, UIDocument top)
        {
            under.gameObject.SetActive(true);
            top.sortingOrder = under.sortingOrder + 1;
        }
    }
}