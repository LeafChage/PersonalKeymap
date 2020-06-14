using UnityEngine;
using Newtonsoft.Json;

namespace PersonalKeymap
{
    public class KeymapSetting<T>
    {
        private IPersonalKeymapJar PersonalKeymapJar { get; }
        public T Mapping { get; }

        public KeymapSetting(IPersonalKeymapJar keyJar)
        {
            PersonalKeymapJar = keyJar;
            Mapping = FetchKeymap();
        }

        private T FetchKeymap()
        {
            var json = PersonalKeymapJar.Get();
            return JsonConvert.DeserializeObject<T>(json);
        }

        private void SaveKeymap()
        {
            var json = JsonConvert.SerializeObject(Mapping);
            PersonalKeymapJar.Set(json);
        }
    }
}