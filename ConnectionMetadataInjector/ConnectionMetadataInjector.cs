using Modding;

namespace ConnectionMetadataInjector
{
    public class ConnectionMetadataInjector : Mod
    {
        internal static ConnectionMetadataInjector? Instance;

        public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();

        public ConnectionMetadataInjector() : base()
        {
            Instance = this;
        }

        public override void Initialize()
        {
            Log("Initializing");

            Log("Initialized");
        }
    }
}