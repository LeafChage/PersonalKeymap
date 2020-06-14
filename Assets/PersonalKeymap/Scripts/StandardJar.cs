using UnityEngine;

namespace PersonalKeymap
{
    public class StandardJar : IPersonalKeymapJar
    {
        #region ICustomKeyJar

        private const string Key = "customeKey.keymap";

        public void Set(string json)
        {
            Debug.Log("set json" + json);
            PlayerPrefs.SetString(Key, json);
            PlayerPrefs.Save();
        }

        public string Get()
        {
            var json = PlayerPrefs.GetString(Key, "");
            Debug.Log("get json" + json);
            return json;
        }

        #endregion
    }
}