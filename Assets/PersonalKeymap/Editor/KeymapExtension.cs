namespace PersonalKeymap.Editor
{
    public static class KeymapExtension
    {
        public static bool IsMono(this Keymap keymap)
        {
            return keymap.Chain == Chain.Mono;
        }
    }
}

