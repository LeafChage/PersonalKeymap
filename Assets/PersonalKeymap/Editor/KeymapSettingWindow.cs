using System.Collections.Generic;
using System.Linq;
using System;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;

namespace PersonalKeymap.Editor
{
    public class KeymapSettingWindow : EditorWindow
    {
        private const float ButtonWidth = 75;
        private const float ValueWidth = 300;

        private List<KeymapInformationWithLabel> keymapInformations = new List<KeymapInformationWithLabel>();
        protected IPersonalKeymapJar PersonalKeymapJar { get; } = new StandardJar();

        [MenuItem("Keymaps/Setting")]
        private static void ShowWindow()
        {
            GetWindow<KeymapSettingWindow>();
        }


        private void OnGUI()
        {
            EditorGUILayout.LabelField("Keymaps");
            if (!HasSaved() && GUILayout.Button("Demo", GUILayout.Width(ButtonWidth)))
            {
                SaveInitialSetting();
                Load();
            }
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Add", GUILayout.Width(ButtonWidth)))
            {
                keymapInformations.Add(new KeymapInformationWithLabel("New"));
            }

            if (GUILayout.Button("Clear", GUILayout.Width(ButtonWidth)))
            {
                keymapInformations.Clear();
            }
            EditorGUILayout.EndHorizontal();


            foreach (var keymapInfo in keymapInformations)
            {
                KeymapForm(keymapInfo);
            }


            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Load Json", GUILayout.Width(ButtonWidth)))
            {
                Load();
            }

            if (GUILayout.Button("Save", GUILayout.Width(ButtonWidth)))
            {
                Save();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void KeymapForm(KeymapInformationWithLabel keymapInfo)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            keymapInfo.Label = EditorGUILayout.TextField("Label", keymapInfo.Label, GUILayout.Width(ValueWidth));
            keymapInfo.Keymap = KeymapForm(keymapInfo.Keymap);
            EditorGUILayout.EndVertical();
        }


        private Keymap KeymapForm(Keymap keymap)
        {
            EditorGUILayout.BeginHorizontal();
            var chain = (Chain)EditorGUILayout.EnumPopup("Chain", keymap.Chain, GUILayout.Width(ValueWidth));
            var keyEvent = keymap.IsMono()
                ? (KeyEvent)EditorGUILayout.EnumPopup("Event", keymap.Key.KeyEvent, GUILayout.Width(ValueWidth))
                : KeyEvent.None;
            var keyCode = keymap.IsMono()
                ? (KeyCode)EditorGUILayout.EnumPopup("Code", keymap.Key.KeyCode, GUILayout.Width(ValueWidth))
                : KeyCode.None;
            EditorGUILayout.EndHorizontal();
            var newKeymap = keymap.IsMono()
                ? new Keymap(chain, new Key(keyEvent, keyCode))
                : new Keymap(chain, KeymapsForm(keymap.Keymaps));
            return newKeymap;
        }

        private Keymap[] KeymapsForm(Keymap[] keymaps)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add", GUILayout.Width(ButtonWidth)))
            {
                var newKeymaps = new Keymap[keymaps.Length + 1];
                Array.Copy(keymaps, 0, newKeymaps, 0, keymaps.Length);
                newKeymaps[keymaps.Length] = new Keymap(Chain.Mono, Key.Null());
                keymaps = newKeymaps;
            }

            if (GUILayout.Button("Clear", GUILayout.Width(ButtonWidth)))
            {
                keymaps = new Keymap[0];
            }
            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel += 2;
            for (int i = 0; i < keymaps.Length; i++)
            {
                keymaps[i] = KeymapForm(keymaps[i]);
            }
            EditorGUI.indentLevel -= 2;
            return keymaps;
        }

        private void Save()
        {
            var dict = keymapInformations.ToDictionary(
                info => info.Label,
                info => info.Keymap);
            var json = JsonConvert.SerializeObject(dict);
            PersonalKeymapJar.Set(json);
        }

        private void Load()
        {
            var json = PersonalKeymapJar.Get();
            var dict = JsonConvert.DeserializeObject<Dictionary<string, Keymap>>(json);
            keymapInformations = new List<KeymapInformationWithLabel>();

            foreach (var pair in dict)
            {
                Debug.Log(pair);
                keymapInformations.Add(new KeymapInformationWithLabel(pair.Key, pair.Value));
            }
        }

        private bool HasSaved()
        {
            return PersonalKeymapJar.Get() != "";
        }

        private void SaveInitialSetting()
        {
            if (!HasSaved())
            {
                var demoSettingJson = "{\"Jump\":{\"chain\":\"Mono\",\"key\":{\"keyEvent\":\"KeyDown\",\"keyCode\":\"Space\",\"KeyEvent\":1,\"KeyCode\":32},\"keymaps\":[]},\"Front\":{\"chain\":\"Mono\",\"key\":{\"keyEvent\":\"Key\",\"keyCode\":\"UpArrow\",\"KeyEvent\":2,\"KeyCode\":273},\"keymaps\":[]},\"Attack\":{\"chain\":\"Mono\",\"key\":{\"keyEvent\":\"KeyDown\",\"keyCode\":\"Return\",\"KeyEvent\":1,\"KeyCode\":13},\"keymaps\":[]},\"SpecialAttack\":{\"chain\":\"And\",\"key\":{\"keyEvent\":\"None\",\"keyCode\":\"None\",\"KeyEvent\":0,\"KeyCode\":0},\"keymaps\":[{\"chain\":\"Mono\",\"key\":{\"keyEvent\":\"Key\",\"keyCode\":\"F\",\"KeyEvent\":2,\"KeyCode\":102},\"keymaps\":[]},{\"chain\":\"Mono\",\"key\":{\"keyEvent\":\"KeyDown\",\"keyCode\":\"J\",\"KeyEvent\":1,\"KeyCode\":106},\"keymaps\":[]}]}}";
                PersonalKeymapJar.Set(demoSettingJson);
            }
        }
    }
}