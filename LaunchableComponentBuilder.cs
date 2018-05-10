﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plukit.Base;
using Staxel.Core;
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