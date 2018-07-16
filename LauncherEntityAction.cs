using System.Linq;
using Plukit.Base;
using Staxel;
using Staxel.Entities;
using Staxel.EntityActions;
using Staxel.Items;
using Staxel.Logic;
using Staxel.Notifications;

namespace NimbusFox.LauncherAPI {
    public class LauncherEntityAction : EntityActionDriver {
        public static string KindCode() {
            return "nimbusfox.entityAction.Launch";
        }

        public override string Kind() {
            return KindCode();
        }

        public override void Start(Entity entity, EntityUniverseFacade facade) {
            if (entity.Inventory.ActiveItem().Item is LauncherTool launcher) {
                var release = launcher.GetAmount();
                if (launcher.UseInventory()) {
                    if (entity.Inventory.CountItemsWithCode(launcher.GetItemToThrow()) < 1) {
                        if (entity.PlayerEntityLogic != null) {

                            var config =
                                GameContext.NotificationDatabase.GetNotificationConfig(
                                    "nimbusfox.launcherapi.notifications.NoAmmo");

                            config.LangCode = launcher.GetNoAmmoCode();

                            var notifcation = GameContext.NotificationDatabase.CreateNotificationFromCode(
                                "nimbusfox.launcherapi.notifications.NoAmmo", facade.Step, NotificationParams.EmptyParams, true);

                            var blankBlob = BlobAllocator.Blob(true);

                            blankBlob.FetchBlob("textParams");

                            notifcation.Restore(config, blankBlob);

                            entity.PlayerEntityLogic.ShowNotification(notifcation);
                        }
                        goto end;
                    }

                    if (entity.Inventory.CountItemsWithCode(launcher.GetItemToThrow()) < release) {
                        release = (byte) entity.Inventory.CountItemsWithCode(launcher.GetItemToThrow());
                    }
                    entity.Inventory.RemoveItemWithCode(launcher.GetItemToThrow(), release, "");
                }

                Item item = null;

                var configs = GameContext.ItemDatabase.GetConfigsByKind(launcher.GetKind()).ToList();

                if (configs.Any(x => x.Value.Code == launcher.GetItemToThrow())) {

                    item = GameContext.ItemDatabase.InstanceFromItemConfiguration(configs
                        .First(x => x.Value.Code == launcher.GetItemToThrow()).Value);
                }

                if (item == null) {
                    goto end;
                }

                for (var i = 0; i < release; i++) {
                    ItemEntityBuilder.SpawnDroppedItem(entity, facade, new ItemStack(item, 1), entity.Physics.TopPosition(), entity.Physics.Velocity, entity.Logic.Heading().ToVector(), SpawnDroppedFlags.None, -1, launcher.GetVelocity());
                } 
            }
            end:
            entity.Logic.ActionFacade.NoNextAction();
        }
    }
}
