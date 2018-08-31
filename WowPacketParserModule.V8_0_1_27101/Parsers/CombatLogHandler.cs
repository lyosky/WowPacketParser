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

        public static void ReadSandboxScalingData(Packet packet, params object[] idx)
        {
            packet.ReadByte("Type", idx);
            packet.ReadByte("TargetLevel", idx);
            packet.ReadByte("Expansion", idx);
            packet.ReadByte("TargetMinScalingLevel", idx);
            packet.ReadByte("TargetMaxScalingLevel", idx);
            packet.ReadInt16("PlayerLevelDelta", idx);
            packet.ReadSByte("TargetScalingLevelDelta", idx);
            packet.ReadUInt16("PlayerItemLevel", idx);
            packet.ReadUInt16("Flags", idx);

            packet.ResetBitReader();
            packet.ReadBit("UnkBit", idx);
        }

        public static void ReadAttackRoundInfo(Packet packet, params object[] indexes)
        {
            var hitInfo = packet.ReadInt32E<SpellHitInfo>("HitInfo", indexes);

            packet.ReadPackedGuid128("AttackerGUID", indexes);
            packet.ReadPackedGuid128("TargetGUID", indexes);

            packet.ReadInt32("Damage", indexes);
            packet.ReadInt32("OriginalDamage", indexes);
            packet.ReadInt32("OverDamage", indexes);

            var subDmgCount = packet.ReadBool("HasSubDmg", indexes);
            if (subDmgCount)
            {
                packet.ReadInt32("SchoolMask", indexes);
                packet.ReadSingle("FloatDamage", indexes);
                packet.ReadInt32("IntDamage", indexes);

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                    packet.ReadInt32("DamageAbsorbed", indexes);

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                    packet.ReadInt32("DamageResisted", indexes);
            }

            packet.ReadByteE<VictimStates>("VictimState", indexes);
            packet.ReadInt32("AttackerState", indexes);

            packet.ReadInt32<SpellId>("MeleeSpellID", indexes);

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
                packet.ReadInt32("BlockAmount", indexes);

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_RAGE_GAIN))
                packet.ReadInt32("RageGained", indexes);

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_UNK0))
            {
                packet.ReadInt32("Unk Attacker State 3 1", indexes);
                packet.ReadSingle("Unk Attacker State 3 2", indexes);
                packet.ReadSingle("Unk Attacker State 3 3", indexes);
                packet.ReadSingle("Unk Attacker State 3 4", indexes);
                packet.ReadSingle("Unk Attacker State 3 5", indexes);
                packet.ReadSingle("Unk Attacker State 3 6", indexes);
                packet.ReadSingle("Unk Attacker State 3 7", indexes);
                packet.ReadSingle("Unk Attacker State 3 8", indexes);
                packet.ReadSingle("Unk Attacker State 3 9", indexes);
                packet.ReadSingle("Unk Attacker State 3 10", indexes);
                packet.ReadSingle("Unk Attacker State 3 11", indexes);
                packet.ReadInt32("Unk Attacker State 3 12", indexes);
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK | SpellHitInfo.HITINFO_UNK12))
                packet.ReadSingle("Unk Float", indexes);

            ReadSandboxScalingData(packet, indexes, "SandboxScalingData");
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

        public static void ReadSpellNonMeleeDebugData(Packet packet, params object[] idx)
        {
            packet.ReadSingle("CritRoll", idx);
            packet.ReadSingle("CritNeeded", idx);
            packet.ReadSingle("HitRoll", idx);
            packet.ReadSingle("HitNeeded", idx);
            packet.ReadSingle("MissChance", idx);
            packet.ReadSingle("DodgeChance", idx);
            packet.ReadSingle("ParryChance", idx);
            packet.ReadSingle("BlockChance", idx);
            packet.ReadSingle("GlanceChance", idx);
            packet.ReadSingle("CrushChance", idx);
        }

        [Parser(Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            packet.ReadPackedGuid128("Me");
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadPackedGuid128("CastID");

            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadUInt32("SpellXSpellVisualID");
            packet.ReadUInt32("Damage");
            packet.ReadUInt32("OriginalDamage");
            packet.ReadInt32("OverKill"); // normally this is uint32 in client, used int here for better readability

            packet.ReadByte("SchoolMask");

            packet.ReadUInt32("Absorbed");
            packet.ReadUInt32("Resisted");
            packet.ReadUInt32("ShieldBlock");

            packet.ResetBitReader();

            packet.ReadBit("Periodic");

            packet.ReadBitsE<AttackerStateFlags>("Flags", 7);

            var hasDebugData = packet.ReadBit("HasDebugData");
            var hasLogData = packet.ReadBit("HasLogData");
            var hasSandboxScaling = packet.ReadBit("HasSandboxScaling");

            if (hasLogData)
                SpellHandler.ReadSpellCastLogData(packet, "SpellCastLogData");

            if (hasDebugData)
                ReadSpellNonMeleeDebugData(packet, "DebugData");

            if (hasSandboxScaling)
                SpellHandler.ReadSandboxScalingData(packet, "SandboxScalingData");
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_LOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadPackedGuid128("CasterGUID");

            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadUInt32("Health");
            packet.ReadUInt32("OverHeal");
            packet.ReadUInt32("Absorbed");
            packet.ReadUInt32("Unk801");

            packet.ResetBitReader();

            packet.ReadBit("Crit");
            var hasCritRollMade = packet.ReadBit("HasCritRollMade");
            var hasCritRollNeeded = packet.ReadBit("HasCritRollNeeded");
            var hasLogData = packet.ReadBit("HasLogData");
            var hasSandboxScaling = packet.ReadBit("HasSandboxScaling");

            if (hasLogData)
                SpellHandler.ReadSpellCastLogData(packet);

            if (hasCritRollMade)
                packet.ReadSingle("CritRollMade");

            if (hasCritRollNeeded)
                packet.ReadSingle("CritRollNeeded");

            if (hasSandboxScaling)
                SpellHandler.ReadSandboxScalingData(packet, "SandboxScalingData");
        }

        [Parser(Opcode.SMSG_SPELL_ABSORB_LOG)]
        public static void HandleSpellAbsorbLog(Packet packet)
        {
            packet.ReadPackedGuid128("Victim");
            packet.ReadPackedGuid128("Caster");

            packet.ReadInt32("InterruptedSpellID");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadPackedGuid128("ShieldTargetGUID?");
            packet.ReadInt32("Absorbed");
            packet.ReadInt32("OriginalDamage"); // OriginalDamage (before HitResult -> BeforeCrit and Armor etc)

            packet.ResetBitReader();

            var bit100 = packet.ReadBit("HasLogData");
            if (bit100)
                SpellHandler.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var hasLogData = packet.ReadBit("HasLogData");

            if (hasLogData)
                SpellHandler.ReadSpellCastLogData(packet);

            packet.ReadInt32("Size");

            ReadAttackRoundInfo(packet, "AttackRoundInfo");
        }

        [Parser(Opcode.SMSG_SPELL_DAMAGE_SHIELD)]
        public static void ReadSpellDamageShield(Packet packet)
        {
            packet.ReadPackedGuid128("Attacker");
            packet.ReadPackedGuid128("Defender");
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadUInt32("TotalDamage");
            packet.ReadUInt32("OverKill");
            packet.ReadUInt32("SchoolMask");
            packet.ReadUInt32("LogAbsorbed");
            packet.ReadUInt32("UnkUInt32");

            packet.ResetBitReader();

            var bit76 = packet.ReadBit("HasLogData");
            if (bit76)
                SpellHandler.ReadSpellCastLogData(packet);
        }
    }
}
