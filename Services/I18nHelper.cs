using Indego.Client;
using Toolbelt.Blazor.I18nText;

namespace Indego.Services;

public static class I18nHelper
{

    public static string AsString(this IndegoStates indegoStates, I18nText.Strings strings) =>
        indegoStates switch
        {
            IndegoStates.ReadingStatus => strings.mower_state_getting_status,
            IndegoStates.Charging => strings.mower_state_charging,
            IndegoStates.Docked => strings.mower_state_docked,
            IndegoStates.DockedSoftwareIpdate => strings.mower_state_sw_update,
            IndegoStates.Docked2 => strings.mower_state_docked,
            IndegoStates.Docked3 => strings.mower_state_docked,
            IndegoStates.DockedLoadingMap => strings.mower_state_docked_loading_map,
            IndegoStates.DockedSavingMap => strings.mower_state_docked_saving_map,
            IndegoStates.Mowing => strings.mower_state_mowing,
            IndegoStates.Relocalising => strings.mower_state_relocalising,
            IndegoStates.LoadingMap => strings.mower_state_loading_map,
            IndegoStates.LearningLawn => strings.mower_state_learning_lawn,
            IndegoStates.Paused => strings.mower_state_paused,
            IndegoStates.BorderCut => strings.mower_state_border_cut,
            IndegoStates.IdleInLawn => strings.mower_state_idle_in_lawn,
            IndegoStates.ReturningToDock => strings.mower_state_returning_to_dock,
            IndegoStates.ReturningToDock2 => strings.mower_state_returning_to_dock,
            IndegoStates.ReturningToDockBatteryLow => strings.mower_state_returning_to_dock_battery_low,
            IndegoStates.ReturningToDockCalendarTimeslotEnded => strings.mower_state_returning_to_dock_calendar,
            IndegoStates.ReturningToDockBatteryTempRange => strings.mower_state_returning_to_dock_battery_temp,
            IndegoStates.ReturningToDock3 => strings.mower_state_returning_to_dock,
            IndegoStates.ReturningToDockLawnComplete => strings.mower_state_returning_to_dock_lawn_complete,
            IndegoStates.ReturningToDockRelocalising => strings.mower_state_returning_to_dock_relocalising,
            IndegoStates.DiagnosticMode => strings.mower_state_diagnostic,
            IndegoStates.EndOfLive => strings.mower_state_eol,
            IndegoStates.SoftwareUpdate => strings.mower_state_sw_update,
        };

}