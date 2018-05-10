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
        public string ItemToThrow { get; }
        public bool UseInventory { get; }
        public double Velocity { get; }
        public string Kind { get; }
        public byte Amount { get; }
        public string NoAmmoCode { get; set; }

        public LaunchableComponent(Blob config) {
            ItemToThrow = config.GetString("itemToThrow", "staxel.crop.Watermelon");
            UseInventory = config.GetBool("useInventory", false);
            Velocity = config.GetDouble("velocity", 50.0);
            Kind = config.GetString("kind", "staxel.item.CraftItem");
            NoAmmoCode = config.GetString("noAmmoCode", "nimbusfox.launcherapi.noammo");
            var amount = config.GetLong("amount", 1);

            if (byte.TryParse(amount.ToString(), out var permitted)) {
                Amount = permitted;
            } else {
                Amount = 1;
            }
        }
    }
}
