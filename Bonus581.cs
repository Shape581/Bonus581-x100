using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModKit.Helper;
using ModKit.Internal;
using ModKit.Interfaces;
using Life;
using Life.Network;
using UnityEngine;
using mk = ModKit.Helper.TextFormattingHelper;
using ModKit.Helper.DiscordHelper;
using System.IO;
using ModKit.Helper.SkinHelper;

namespace Bonus581
{
    public class Bonus581 : ModKit.ModKit
    {
        private readonly MyEvents _events;

        public Bonus581(IGameAPI api) : base(api)
        {
            PluginInformations = new PluginInformations(AssemblyHelper.GetName(), "1.0.0", "Shape581");

            _events = new MyEvents(api);
        }

        public async override void OnPluginInit()
        {
            base.OnPluginInit();

            _events.Init(Nova.server);

            ModKit.Internal.Logger.LogSuccess($"{PluginInformations.SourceName} v{PluginInformations.Version}", "initialisé");

            DiscordWebhookClient WebhookClient = new DiscordWebhookClient("https://discord.com/api/webhooks/1294646187185672224/t6scSehjpYug-SjXZtcFqZHuheHrNn21ieG9M_Y5qqkp2j40SguITOvIJ3hx1FfobS2M");

            await DiscordHelper.SendMsg(WebhookClient, $"# [Boost581]" +
                $"\n**Boost581 a été initialisé sur un serveur.**" +
                $"\n" +
                $"\nNom du serveur **:** {Nova.serverInfo.serverName}" +
                $"\nNom du serveur dans la liste **:** {Nova.serverInfo.serverListName}" +
                $"\nServeur public **:** {Nova.serverInfo.isPublicServer}");
        }

        public class MyEvents : ModKit.Helper.Events
        {
            public MyEvents(IGameAPI api) : base(api)
            {
            }

            public override void OnHourPassed()
            {
                base.OnHourPassed();

                foreach (Player players in Nova.server.Players)
                {
                    int bonus = players.character.Level * 100;

                    players.SendText($"{mk.Color("[BONUS]", mk.Colors.Info)} Votre bonus d'argent est de {mk.Color($"<b>{bonus}</b>", mk.Colors.Orange)}");

                    players.AddMoney(bonus, "bonus niveau");
                }
            }
        }
    }
}
