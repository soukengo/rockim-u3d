using RockIM.Sdk;
using RockIM.Sdk.Api.V1;

namespace RockIM.Demo.Scripts.Managers
{
    public class ImSdkManager
    {
        public static readonly ImSdkManager Instance = new ImSdkManager();

        private ImSdkManager()
        {
        }

        public void Init(Config config)
        {
            ImSdk.Async(() => ImSdk.V1.Init(config),
                (ret) =>
                {
                    if (ret.IsSuccess())
                    {
                    }
                });
        }
    }
}