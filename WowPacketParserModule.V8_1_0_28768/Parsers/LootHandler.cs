using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_1_0_28768.Parsers
{
    public static class LootHandler
    {
        [Parser(Opcode.SMSG_LOOT_LEGACY_RULES_IN_EFFECT)]
        public static void HandleLootLegacyRulesInEffect(Packet packet)
        {
            packet.ReadBit("LegacyRulesActive");
        }
    }
}
