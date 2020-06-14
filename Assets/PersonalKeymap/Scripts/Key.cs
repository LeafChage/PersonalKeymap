using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PersonalKeymap
{
    [Serializable]
    public class Key
    {
        [SerializeField, JsonProperty("keyEvent"), JsonConverter(typeof(StringEnumConverter))] private KeyEvent keyEvent;
        [SerializeField, JsonProperty("keyCode"), JsonConverter(typeof(StringEnumConverter))] private KeyCode keyCode;
        public KeyEvent KeyEvent { get { return keyEvent; } }
        public KeyCode KeyCode { get { return keyCode; } }


        public Key(KeyEvent e, KeyCode c)
        {
            keyEvent = e;
            keyCode = c;
        }

        public bool On()
        {
            switch (keyEvent)
            {
                case KeyEvent.Key:
                    return Input.GetKey(keyCode);
                case KeyEvent.KeyDown:
                    return Input.GetKeyDown(keyCode);
                case KeyEvent.KeyUp:
                    return Input.GetKeyUp(keyCode);
                default:
                    return false;
            }
        }

        public static Key Null()
        {
            return new Key(KeyEvent.None, KeyCode.None);
        }

        public override string ToString()
        {
            return $"keyEvent: {KeyEvent}, keyCode: {KeyCode}";
        }
    }
}