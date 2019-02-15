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
    }
}
