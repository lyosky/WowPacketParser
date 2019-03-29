using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class ChallengeModeHandler
    {
        public static void ReadClientChallengeModeMap(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("MapId", indexes);
            packet.ReadInt32("KeyId", indexes);
            packet.ReadInt32("BestCompletionMilliseconds", indexes);
            packet.ReadInt32("LastCompletionMilliseconds", indexes);
            packet.ReadInt32("BestMedal", indexes);
            packet.ReadPackedTime("BestMedalDate", indexes);

            var bestSpecCount = packet.ReadInt32("BestSpecIDCount", indexes);
            packet.ReadInt32("Unk1", indexes);
            packet.ReadInt32("Unk2", indexes);
            packet.ReadInt32("Unk3", indexes);
            for (int i = 0; i < bestSpecCount; i++)
                packet.ReadInt16("BestSpecID", indexes, i);//ReadInt16
   
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_ALL_MAP_STATS)]
        public static void HandleChallengeModeAllMapStats(Packet packet)
        {
            var challengeModeMapCount = packet.ReadInt32("ChallengeModeMapCount");
            for (int i = 0; i < challengeModeMapCount; i++)
                ReadClientChallengeModeMap(packet, i);
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_REWARDS)]
        public static void HandleChallengeModeRewards(Packet packet)
        {
            packet.ReadBit("IsWeeklyRewardAvailable");
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_REQUEST_LEADERS_RESULT)]
        public static void HandleChallengeModeRequestLeadersResult(Packet packet)
        {
            packet.ReadInt32("MapID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_0_23706))
                packet.ReadInt32("Unk");
            packet.ReadTime("LastGuildUpdate");
            packet.ReadTime("LastRealmUpdate");

            var int4 = packet.ReadInt32("GuildLeadersCount");
            var int9 = packet.ReadInt32("RealmLeadersCount");

            for (int i = 0; i < int4; i++)
                V6_0_2_19033.Parsers.ChallengeModeHandler.ReadChallengeModeAttempt(packet, i, "GuildLeaders");

            for (int i = 0; i < int9; i++)
                V6_0_2_19033.Parsers.ChallengeModeHandler.ReadChallengeModeAttempt(packet, i, "RealmLeaders");
        }
    }
}
