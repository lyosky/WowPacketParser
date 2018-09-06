using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.SMSG_BATTLENET_UPDATE_SESSION_KEY)]
        public static void HandleBattlenetUpdateSessionKey(Packet packet)
        {
            var size = (byte)packet.ReadBits(7);

            packet.ReadBytesTable("UnkDigest_1", 32);
            packet.ReadBytesTable("UnkDigest_2", size);
        }
    }
}
