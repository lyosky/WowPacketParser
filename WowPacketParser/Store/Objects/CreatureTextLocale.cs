using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;
using WowPacketParser.Loading;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_text_locale")]
    public sealed class CreatureTextLocale : IDataModel
    {
        [DBFieldName("CreatureID", true)]
        public uint? Entry;

        [DBFieldName("GroupID", true, true)]
        public string GroupId;

        [DBFieldName("ID", true, true)]
        public string ID;

        [DBFieldName("locale", true)]
        public string Locale = BinaryPacketReader.GetClientLocale();

        [DBFieldName("Text")]
        public string Text;
    }
}

