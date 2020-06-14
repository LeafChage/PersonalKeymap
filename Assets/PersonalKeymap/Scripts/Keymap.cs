using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;

namespace PersonalKeymap
{
    [Serializable]
    public class Keymap
    {
        [SerializeField, JsonProperty("chain"), JsonConverter(typeof(StringEnumConverter))] private Chain chain;
        [SerializeField, JsonProperty("key")] private Key key;
        [SerializeField, JsonProperty("keymaps")] private Keymap[] keymaps;
        [JsonIgnore] public Chain Chain { get { return chain; } }
        [JsonIgnore] public Key Key { get { return key; } }
        [JsonIgnore] public Keymap[] Keymaps { get { return keymaps; } }

        public Keymap(Chain c, Keymap[] km)
        {
            chain = c;
            keymaps = km;
            key = Key.Null();
        }

        public Keymap(Chain c, Key k)
        {
            chain = c;
            keymaps = new Keymap[0];
            key = k;
        }

        [JsonConstructor]
        public Keymap(Chain c, Key k, Keymap[] kms)
        {
            chain = c;
            key = k;
            keymaps = kms;
        }

        public override string ToString()
        {
            return $"chain: {chain}, key: {key?.ToString()}, keymap: {keymaps?.Select(km => km.ToString()).Aggregate("", (a, b) => $"{a}\n{b}")}";
        }


        public bool On()
        {
            switch (chain)
            {
                case Chain.Mono:
                    return key.On();
                case Chain.And:
                    foreach (var keymapChain in keymaps)
                    {
                        if (!keymapChain.On())
                        {
                            return false;
                        }
                    }

                    return true;
                case Chain.Or:
                    foreach (var keymapChain in keymaps)
                    {
                        if (keymapChain.On())
                        {
                            return true;
                        }
                    }

                    return false;
                default:
                    return false;
            }
        }
    }
}