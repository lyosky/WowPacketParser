using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V8_0_1_27101.Enums;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var hasData = packet.ReadByte("HasData");

            packet.ReadPackedGuid128("Player Guid");

            if (hasData == 0)
            {
                packet.ReadBit("IsDeleted");
                var bits15 = (int)packet.ReadBits(6);

                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.ReadBits(7);

                for (var i = 0; i < 5; ++i)
                    packet.ReadWoWString("Name Declined", count[i], i);

                packet.ReadPackedGuid128("AccountID");
                packet.ReadPackedGuid128("BnetAccountID");
                packet.ReadPackedGuid128("Player Guid");

                packet.ReadUInt32("VirtualRealmAddress");

                packet.ReadUInt64("UnkUInt64");

                packet.ReadByteE<Race>("Race");
                packet.ReadByteE<Gender>("Gender");
                packet.ReadByteE<Class>("Class");
                packet.ReadByte("Level");

                packet.ReadWoWString("Name", bits15);
            }
        }
        
        public static void ReadCharactersData(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("Guid", idx);

            packet.ReadUInt64("Unk801", idx);

            packet.ReadByte("ListPosition", idx);
            var race = packet.ReadByteE<Race>("RaceID", idx);
            var klass = packet.ReadByteE<Class>("ClassID", idx);
            packet.ReadByteE<Gender>("SexID", idx);
            packet.ReadByte("SkinID", idx);
            packet.ReadByte("FaceID", idx);
            packet.ReadByte("HairStyle", idx);
            packet.ReadByte("HairColor", idx);
            packet.ReadByte("FacialHairStyle", idx);

            for (uint j = 0; j < 3; ++j)
                packet.ReadByte("CustomDisplay", idx, j);

            packet.ReadByte("ExperienceLevel", idx);
            var zone = packet.ReadUInt32<ZoneId>("ZoneID", idx);
            var mapId = packet.ReadUInt32<MapId>("MapID", idx);

            var pos = packet.ReadVector3("PreloadPos", idx);

            packet.ReadPackedGuid128("GuildGUID", idx);

            packet.ReadUInt32("Flags", idx);
            packet.ReadUInt32("Flags2", idx);
            packet.ReadUInt32("Flags3", idx);
            packet.ReadUInt32("PetCreatureDisplayID", idx);
            packet.ReadUInt32("PetExperienceLevel", idx);
            packet.ReadUInt32("PetCreatureFamilyID", idx);

            for (uint j = 0; j < 2; ++j)
                packet.ReadUInt32("ProfessionIDs", idx, j);

            for (uint j = 0; j < 23; ++j)
            {
                packet.ReadUInt32("InventoryItem DisplayID", idx, j);
                packet.ReadUInt32("InventoryItem DisplayEnchantID", idx, j);
                packet.ReadByteE<InventoryType>("InventoryItem InvType", idx, j);
            }

            packet.ReadTime("LastPlayedTime", idx);

            packet.ReadUInt16("SpecID", idx);
            packet.ReadUInt32("Unknown703", idx);
            packet.ReadUInt32("LastLoginBuild", idx);
            packet.ReadUInt32("Flags4", idx);

            packet.ResetBitReader();

            var nameLength = packet.ReadBits("Character Name Length", 6, idx);
            var firstLogin = packet.ReadBit("FirstLogin", idx);
            packet.ReadBit("BoostInProgress", idx);
            packet.ReadBits("UnkWod61x", 5, idx);

            packet.ReadWoWString("Character Name", nameLength, idx);

            if (firstLogin)
            {
                PlayerCreateInfo startPos = new PlayerCreateInfo { Race = race, Class = klass, Map = mapId, Zone = zone, Position = pos, Orientation = 0 };
                Storage.StartPositions.Add(startPos, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT)]
        public static void HandleEnumCharactersResult(Packet packet)
        {
            packet.ReadBit("Success");
            packet.ReadBit("IsDeletedCharacters");
            packet.ReadBit("IsDemonHunterCreationAllowed");
            packet.ReadBit("HasDemonHunterOnRealm");
            packet.ReadBit("Unknown7x");

            var hasDisabledClassesMask = packet.ReadBit("HasDisabledClassesMask");
            packet.ReadBit("IsAlliedRacesCreationAllowed");

            var charsCount = packet.ReadUInt32("CharactersCount");
            packet.ReadInt32("MaxCharacterLevel");
            var raceUnlockCount = packet.ReadUInt32("RaceUnlockCount");

            if (hasDisabledClassesMask)
                packet.ReadUInt32("DisabledClassesMask");

            for (uint i = 0; i < charsCount; ++i)
                ReadCharactersData(packet, i, "CharactersData");

            for (var i = 0; i < raceUnlockCount; ++i)
                V7_0_3_22248.Parsers.CharacterHandler.ReadRaceUnlockData(packet, i, "RaceUnlockData");
        }
    }
}
