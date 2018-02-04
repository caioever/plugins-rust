using Oxide.Core;
using System;
using System.Collections.Generic;
using Rust;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Plugins
{
    [Info("StartWithStuff", "caio.barcelos", 1.0.1)]
    [Description("These plugin allow your player start with itens")]
    class StartWithStuff : RustPlugin
    {
        // Init
        void Init()
        {
            Puts("Plugin start succefuly");
        }

        void OnPlayerRespawned(BasePlayer player)
        {
            GivePlayerGift(player, "smg.mp5");
        }

        object OnPlayerSpawn(BasePlayer player)
        {
            GivePlayerGift(player, "smg.mp5");
            return player;
        }

        void GivePlayerGift(BasePlayer player, string gift)
        {
            Item item = ItemManager.CreateByItemID(ItemManager.FindItemDefinition(gift).itemid);
            player.GiveItem(item);
        }
    }
}
