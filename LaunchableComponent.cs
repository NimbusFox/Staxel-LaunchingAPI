using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plukit.Base;
using Staxel.Items;
using Staxel.Items.ItemComponents;

namespace NimbusFox.LauncherAPI {
    public class LaunchableComponent {
        public string ItemToThrow { get; private set; }
        public bool UseInventory { get; private set; }
        public double Velocity { get; private set; }
        public string Kind { get; private set; }
        public byte Amount { get; private set; }

        public LaunchableComponent(Blob config) {
            ItemToThrow = config.GetString("itemToThrow", "staxel.crop.Watermelon");
            UseInventory = config.GetBool("useInventory", false);
            Velocity = config.GetDouble("velocity", 50.0);
            Kind = config.GetString("kind", "staxel.item.CraftItem");
            var amount = config.GetLong("amount", 1);

            if (byte.TryParse(amount.ToString(), out var permitted)) {
                Amount = permitted;
            } else {
                Amount = 1;
            }
        }
    }
}
