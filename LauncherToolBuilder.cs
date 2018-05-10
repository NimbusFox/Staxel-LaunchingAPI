using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plukit.Base;
using Staxel.Items;

namespace NimbusFox.LauncherAPI {
    public class LauncherToolBuilder : IItemBuilder, IDisposable {
        public ItemRenderer Renderer { get; private set; }

        public void Dispose() { }

        public void Load() {
            Renderer = new ItemRenderer();
        }

        public Item Build(Blob blob, ItemConfiguration config, Item spare) {
            if (spare is LauncherTool) {
                if (spare.Configuration != null) {
                    spare.Restore(config, blob);
                    return spare;
                }
            }

            var launcherTool = new LauncherTool(this, config);
            launcherTool.Restore(config, blob);
            return launcherTool;
        }

        public string Kind() {
            return "nimbusfox.item.Launcher";
        }
    }
}
