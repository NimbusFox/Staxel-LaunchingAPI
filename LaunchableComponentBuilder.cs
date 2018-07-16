using Plukit.Base;
using Staxel.Items;

namespace NimbusFox.LauncherAPI {
    public class LaunchableComponentBuilder : IItemComponentBuilder {
        public string Kind() {
            return "launcher";
        }

        public object Instance(BaseItemConfiguration item, Blob config) {
            return new LaunchableComponent(config);
        }
    }
}
