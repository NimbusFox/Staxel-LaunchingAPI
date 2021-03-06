﻿using System;
using Plukit.Base;
using Staxel.Client;
using Staxel.Collections;
using Staxel.Items;
using Staxel.Logic;
using Staxel.Tiles;

namespace NimbusFox.LauncherAPI {
    public class LauncherTool : Item {

        private readonly LauncherToolBuilder _builder;

        private string _itemToThrow;
        private bool _useInventory;
        private double _velocity;
        private string _kind;
        private byte _amount;
        private string _noAmmoCode;

        public LauncherTool(LauncherToolBuilder builder, ItemConfiguration config) : base(builder.Kind()) {
            _builder = builder;
            Configuration = config;
        }
        public override void Update(Entity entity, Timestep step, EntityUniverseFacade entityUniverseFacade) { }

        public override void Control(Entity entity, EntityUniverseFacade facade, ControlState main, ControlState alt) {
            if (!main.DownClick) {
                return;
            }

            entity.Logic.ActionFacade.NextAction(LauncherEntityAction.KindCode());
        }

        protected override void AssignFrom(Item item) {
            if (item.Kind != Kind) {
                throw new Exception("Attempting to match tools " + item.Kind + ", " + Kind + " in assignFrom");
            }

            Configuration = item.Configuration;

            var component = Configuration.Components.Get<LaunchableComponent>();

            _itemToThrow = component.ItemToThrow;
            _useInventory = component.UseInventory;
            _velocity = component.Velocity;
            _kind = component.Kind;
            _amount = component.Amount;
            _noAmmoCode = component.NoAmmoCode;
        }

        public override ItemRenderer FetchRenderer() {
            return _builder.Renderer;
        }

        public override bool Same(Item item) {
            return item.GetItemCode() == GetItemCode();
        }

        public override bool PlacementTilePreview(AvatarController avatar, Entity entity, Universe universe, Vector3IMap<Tile> previews) {
            return false;
        }

        public override bool HasAssociatedToolComponent(Components components) {
            return components.Contains<LaunchableComponent>();
        }

        public string GetItemToThrow() {
            return _itemToThrow;
        }

        public bool UseInventory() {
            return _useInventory;
        }

        public double GetVelocity() {
            return _velocity;
        }

        public string GetKind() {
            return _kind;
        }

        public byte GetAmount() {
            return _amount;
        }

        public string GetNoAmmoCode() {
            return _noAmmoCode;
        }

        public override bool TryResolveMainInteractVerb(Entity entity, EntityUniverseFacade facade, Entity lookedAtEntity, out string verb) {
            verb = InteractVerb();
            return true;
        }

        public override bool TryResolveMainInteractVerb(Entity entity, EntityUniverseFacade facade, Vector3I location,
            TileConfiguration lookedAtTile, out string verb) {
            verb = InteractVerb();
            return true;
        }
    }
}
