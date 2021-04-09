using System;
using System.Collections.Generic;
using System.Text;
using Unbroken.LaunchBox.Plugins.Data;

namespace Common_PMG.Container.Game
{
    interface I_FullGame : IGame
    {
        bool MissingBox3dImage { get; set; }
        bool MissingCartImage { get; set; }
        bool MissingCart3dImage { get; set; }
        bool MissingManual { get; set; }
        bool MissingBannerImage { get; set; }
        bool MissingMusic { get; set; }

        // ---
        string GogAppId { get; set; }
        string OriginAppId { get; set; }

        string OriginInstallPath { get; set; }

        // --- Pauses
        bool UsePauseScreen { get; set; }
        bool PauseAutoHotkeyScript { get; set; }
        bool ResumeAutoHotkeyScript { get; set; }

        bool SuspendProcessOnPause { get; set; }
        bool ForcefulPauseScreenActivation { get; set; }

        bool OverrideDefaultPauseScreenSettings { get; set; }

        bool LoadStateAutoHotkeyScript { get; set; }
        bool SaveStateAutoHotkeyScript { get; set; }
        bool ResetAutoHotkeyScript { get; set; }
        bool SwapDiscsAutoHotkeyScript { get; set; }
    }
}
