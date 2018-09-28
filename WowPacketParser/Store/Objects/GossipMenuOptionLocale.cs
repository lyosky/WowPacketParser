using WowPacketParser.Loading;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gossip_menu_option_locale")]
    public class GossipMenuOptionLocale : IDataModel
    {
        [DBFieldName("MenuId", true)]
        public uint? MenuId;

        [DBFieldName("OptionIndex", true)]
        public uint? OptionIndex;

        [DBFieldName("locale", true)]
        public string Locale = BinaryPacketReader.GetClientLocale();

        [DBFieldName("OptionText")]
        public string OptionText;

        [DBFieldName("BoxText")]
        public string BoxText;

    }
}
