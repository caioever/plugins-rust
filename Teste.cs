using Oxide.Core;
using System;
using System.Collections.Generic;
using Rust;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Plugins
{
    [Info("Test", "caio.barcelos", 0.1)]
    [Description("Primeiro plugin de teste")]
    class Test : RustPlugin
    {
        // Init
        void Init()
        {
            Puts("Plugin de teste iniciado");
        }
        void OnPlayerLanded(BasePlayer player, float num)
        {
            Puts("debug mask OnPlayerLanded");

            GivePlayerGift(player, "smg.mp5");
        }

        void GivePlayerGift(BasePlayer player, string gift)
        {
            Item item = ItemManager.CreateByItemID(ItemManager.FindItemDefinition(gift).itemid);
            player.GiveItem(item);
        }
    }
}
