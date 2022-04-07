using System;
using UnityEngine;
using UnityModManagerNet;
using UnityEngine.UI;
using HarmonyLib;

namespace RtWPChanges
{
    static class Main
    {
        public static Settings Settings;
        public static bool Enabled;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            Settings = Settings.Load<Settings>(modEntry);
            modEntry.Logger.ToString();
            modEntry.OnGUI = OnGUI;
            modEntry.OnSaveGUI = OnSaveGUI;
            modEntry.OnToggle = OnToggle;
            var harmony = new Harmony(modEntry.Info.Id);
            harmony.PatchAll();
            Main.Log("DEBUG LOGGING ENABLED", modEntry);
            return true;
        }

        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            Enabled = value;
            return true;
        }

        static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("MyFloatOption", GUILayout.ExpandWidth(false));
            GUILayout.Space(10);
            Settings.MyFloatOption = GUILayout.HorizontalSlider(Settings.MyFloatOption, 1f, 10f, GUILayout.Width(300f));
            GUILayout.Label($" {Settings.MyFloatOption:p0}", GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("MyBoolOption", GUILayout.ExpandWidth(false));
            GUILayout.Space(10);
            Settings.MyBoolOption = GUILayout.Toggle(Settings.MyBoolOption, $" {Settings.MyBoolOption}", GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("MyTextOption", GUILayout.ExpandWidth(false));
            GUILayout.Space(10);
            Settings.MyTextOption = GUILayout.TextField(Settings.MyTextOption, GUILayout.Width(300f));
            GUILayout.EndHorizontal();
        }

        static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            Settings.Save(modEntry);
        }

        public static void Log(string msg, UnityModManager.ModEntry modEntry)
        {
            modEntry.Logger.Log(msg);
        }

    }
}
