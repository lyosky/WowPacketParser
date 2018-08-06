using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V8_0_1_27101.Enums;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class CombatLogHandler
    {
        public static void ReadPeriodicAuraLogEffectData(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("Effect", idx);
            packet.ReadUInt32("Amount", idx);
            packet.ReadUInt32("Unk801", idx);
            packet.ReadInt32("OverHealOrKill", idx); // normally this is uint32 in client, used int here for better readability
            packet.ReadUInt32("SchoolMaskOrPower", idx);
            packet.ReadUInt32("AbsorbedOrAmplitude", idx);
            packet.ReadUInt32("Resisted", idx);

            packet.ResetBitReader();

            packet.ReadBit("Crit", idx);
            var hasDebugData = packet.ReadBit("HasPeriodicAuraLogEffectDebugInfo", idx);
            var hasSandboxScaling = packet.ReadBit("HasSandboxScaling", idx);

            if (hasSandboxScaling)
                SpellHandler.ReadSandboxScalingData(packet, idx, "SandboxScalingData");

            if (hasDebugData)
            {
                packet.ReadSingle("CritRollMade", idx);
                packet.ReadSingle("CritRollNeeded", idx);
            }
        }

        [Parser(Opcode.SMSG_SPELL_PERIODIC_AURA_LOG)]
        public static void HandleSpellPeriodicAuraLog720(Packet packet)
        {
            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadPackedGuid128("CasterGUID");

            packet.ReadUInt32<SpellId>("SpellID");

            var periodicAuraLogEffectCount = packet.ReadInt32("PeriodicAuraLogEffectCount");

            packet.ResetBitReader();
            var hasLogData = packet.ReadBit("HasLogData");

            for (var i = 0; i < periodicAuraLogEffectCount; i++)
                ReadPeriodicAuraLogEffectData(packet, "PeriodicAuraLogEffectData");

            if (hasLogData)
                SpellHandler.ReadSpellCastLogData(packet, "SpellCastLogData");
        }
    }
}
