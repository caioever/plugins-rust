using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Libraries;
using Oxide.Core;

namespace Oxide.Plugins
{
    [Info("DiscordChatConector", "caio.barcelos", 1.0)]
    [Description("Plugin de ponte para chat do Rust com Discord")]

    class DiscordChatConector : CovalencePlugin
    {
        static readonly Regex Regex = new Regex(@"<avatarMedium><!\[CDATA\[(.*)\]\]></avatarMedium>");

        void OnUserChat(IPlayer player, string message)
        {
            webrequest.Enqueue($"http://steamcommunity.com/profiles/{player.Id}?xml=1", null, (code, response) =>
            {
              string avatarSteam = null;
              if (response != null && code == 200) avatarSteam = Regex.Match(response).Groups[1].ToString();

              var payload = new DiscordPayload
              {
                avatar_url = $"{avatarSteam}",
                username = $"{player.Name}",
                content = $"{message}"
              };

              PostDiscord(payload);
            }, this, RequestMethod.GET);
        }

        void PostDiscord(DiscordPayload payload)
        {
            string webhook = "WEBHOOCK-DO-DISCORD";
            if (payload == null || string.IsNullOrEmpty(webhook)) return;
            webrequest.Enqueue(webhook, JsonConvert.SerializeObject(payload), (code, response) => ServerDetailsCallback(code, response), this, RequestMethod.POST);
        }

        void ServerDetailsCallback(int codigo, string callback)
        {
            if (callback == null || codigo != 200 || codigo != 201 || codigo != 202 || codigo != 203 || codigo != 204)
            {
                Puts($"ERRO: {codigo} - NAO FOI POSSIVEL CONECTAR COM DISCORD | callback: {callback}");
                return;
            }
            Puts("Discord respondeu com codigo " + codigo + ": " + callback);
        }

        class DiscordPayload
        {
            public string avatar_url { get; set; }
            public string username { get; set; }
            public string content { get; set; }
        }
    }
}
