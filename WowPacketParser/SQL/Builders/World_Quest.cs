using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class World_Quest
    {
        [BuilderMethod]
        public static string WorldQuest()
        {
            if (Storage.WorldQuests.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.worldquest))
                return string.Empty;

                var templatesDb = SQLDatabase.Get(Storage.WorldQuests);

               return SQLUtil.Compare(Storage.WorldQuests, templatesDb, StoreNameType.WorldQuest);
        }
    }
}
