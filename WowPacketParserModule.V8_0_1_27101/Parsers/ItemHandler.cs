using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.CMSG_AZERITE_EMPOWERED_ITEM_SELECT_POWER)]
        public static void HandleItemAzerithEmpoweredItemSelectPower(Packet packet)
        {
            packet.ReadInt32("Unk1");
            packet.ReadInt32("Unk2");
            packet.ReadByte("Unk3");
            packet.ReadByte("Unk4");
        }

        [Parser(Opcode.CMSG_AZERITE_EMPOWERED_ITEM_VIEWED)]
        public static void HandleItemAzerithEmpoweredItemViewed(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }
    }
}
