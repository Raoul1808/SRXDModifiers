using HarmonyLib;
using TMPro;
using UnityEngine;

namespace SRXDModifiers
{
    public class ModifiersCustomUI
    {
        private static GameObject modifiersContextMenu;
        private static GameObject modifiersButton;
        private static RectTransform modifiersButtonRect;

        [HarmonyPatch(typeof(XDLevelSelectMenuBase), nameof(XDLevelSelectMenuBase.OpenMenu))]
        [HarmonyPostfix]
        private static void XDLevelSelectMenuBaseOpenMenuPostfix(XDLevelSelectMenuBase __instance)
        {
            Transform sortParent = __instance.sortButton.transform.parent.parent.parent.parent;
            if (modifiersButton == null)
            {
                modifiersButton = Object.Instantiate(__instance.sortButton.transform.parent.gameObject, sortParent);
                modifiersButton.name = "ModifiersButton";
                modifiersButton.gameObject.GetComponentInChildren<TMP_Text>().name = "ModifiersButton";
                modifiersButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText("Modifiers");
            }
            modifiersButton.gameObject.transform.SetParent(sortParent);
            modifiersButton.gameObject.GetComponentInChildren<RectTransform>().SetParent(sortParent);

            modifiersButtonRect = modifiersButton.gameObject.GetComponentInChildren<RectTransform>();
            modifiersButtonRect.anchorMin = new Vector2(0.3399999f, -0.01999933f);
            modifiersButtonRect.anchorMax = new Vector2(0.3399999f, -0.01999933f);

            // If customs: 0.33 -0.4899992
            // If base game: 0.3399999 -0.01999933
        }

        [HarmonyPatch(typeof(XDCustomLevelSelectMenu), nameof(XDCustomLevelSelectMenu.OpenMenu))]
        [HarmonyPostfix]
        private static void XDCustomLevelSelectMenuOpenMenuPostfix()
        {
            modifiersButtonRect.anchorMin = new Vector2(0.33f, -0.4899992f);
            modifiersButtonRect.anchorMax = new Vector2(0.33f, -0.4899992f);
        }

        [HarmonyPatch(typeof(XDLevelSelectMenuBase), nameof(XDLevelSelectMenuBase.Update))]
        [HarmonyPostfix]
        private static void MoveButton()
        {
            RectTransform rect = modifiersButton.gameObject.GetComponentInChildren<RectTransform>();
            float x = 0f;
            float y = 0f;
            if (Input.GetKey(KeyCode.I)) y += 0.01f;
            if (Input.GetKey(KeyCode.J)) x -= 0.01f;
            if (Input.GetKey(KeyCode.K)) y -= 0.01f;
            if (Input.GetKey(KeyCode.L)) x += 0.01f;
            rect.anchorMin = new Vector2(rect.anchorMin.x + x, rect.anchorMin.y + y);
            rect.anchorMax = new Vector2(rect.anchorMax.x + x, rect.anchorMax.y + y);
            if (Input.GetKeyDown(KeyCode.N)) Mod.Logger.LogMessage(rect.anchorMin.x.ToString() + " " + rect.anchorMin.y.ToString());
        }

        [HarmonyPatch(typeof(TMP_Text), "set_text")]
        [HarmonyPrefix]
        private static void TMPTextSetTextPrefix(ref string value, TMP_Text __instance)
        {
            if (__instance.name == "ModifiersButton")
            {
                value = "Modifiers";
            }
        }
    }
}
