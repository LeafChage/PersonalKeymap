namespace PersonalKeymap.Editor
{
    public class KeymapInformationWithLabel
    {
        public string Label { get; set; }
        public Keymap Keymap { get; set; }
        public KeymapInformationWithLabel(string label)
        {
            Label = label;
            Keymap = new Keymap(Chain.Mono, Key.Null());
        }
        public KeymapInformationWithLabel(string label, Keymap keymap)
        {
            Label = label;
            Keymap = keymap;
        }
    }
}