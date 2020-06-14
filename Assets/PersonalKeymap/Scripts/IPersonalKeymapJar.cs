namespace PersonalKeymap
{
    public interface IPersonalKeymapJar
    {
        // save custom key file
        void Set(string json);

        // get custom key file
        string Get();
    }
}