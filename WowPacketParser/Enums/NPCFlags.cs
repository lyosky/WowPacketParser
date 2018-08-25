using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum NPCFlags : ulong
    {
        None                    = 0x00000000000,
        Gossip                  = 0x00000000001,
        QuestGiver              = 0x00000000002,
        Unknown1                = 0x00000000004,
        Unknown2                = 0x00000000008,
        Trainer                 = 0x00000000010,
        ClassTrainer            = 0x00000000020,
        ProfessionTrainer       = 0x00000000040,
        Vendor                  = 0x00000000080,
        AmmoVendor              = 0x00000000100,
        FoodVendor              = 0x00000000200,
        PoisonVendor            = 0x00000000400,
        ReagentVendor           = 0x00000000800,
        Repair                  = 0x00000001000,
        FlightMaster            = 0x00000002000,
        SpiritHealer            = 0x00000004000,
        SpiritGuide             = 0x00000008000,
        InnKeeper               = 0x00000010000,
        Banker                  = 0x00000020000,
        Petitioner              = 0x00000040000,
        TabardDesigner          = 0x00000080000,
        BattleMaster            = 0x00000100000,
        Auctioneer              = 0x00000200000,
        StableMaster            = 0x00000400000,
        GuildBanker             = 0x00000800000,
        SpellClick              = 0x00001000000,
        PlayerVehicle           = 0x00002000000,
        MailObject              = 0x00004000000,
        ForgeMaster             = 0x00008000000,
        Transmogrifier          = 0x00010000000,
        Vaultkeeper             = 0x00020000000,
        BlackMarket             = 0x00080000000,
        ItemUpgradeMaster       = 0x00100000000,
        GarrisonArchitect       = 0x00200000000,
        Steering                = 0x00400000000,
        ShipmentCrafter         = 0x01000000000,
        GarrisonMissionNpc      = 0x02000000000,
        TradeskillNpc           = 0x04000000000,
        BlackMarketView         = 0x08000000000,
        ContributionCollector   = 0x40000000000,
    }
}
