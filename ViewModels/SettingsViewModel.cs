﻿using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace UWP_Visual_Asset_Generator.ViewModels
{
    public class SettingsViewModel : ValleyBaseViewModel
    {
        private bool _showWelcomePage = false;
        private ApplicationDataContainer roamingSettings;


        public SettingsViewModel()
        {
            Load();
        }

        public bool ShowWelcomePage
        {
            get
            {
                return _showWelcomePage;
            }
            set
            {
                _showWelcomePage = value;
                NotifyPropertyChanged("ShowWelcomePage");
            }
        }

        public void LoadRoamingSettings()
        {
            roamingSettings = null;
            roamingSettings = ApplicationData.Current.RoamingSettings;
        }

        public async Task Load()
        {
            if (!Thinking)
            {
                Thinking = true;

                LoadRoamingSettings();

                NotifyPropertyChanged(string.Empty);

                ShowWelcomePage = false;

                if (string.IsNullOrEmpty(LastUsedVersion))
                {
                    //First Load
                    ShowWelcomePage = true;
                }
                else
                {
                    if (LastUsedVersion.Equals(AppVersion))
                    {
                        //Do nothing, no changes since last load.
                    }
                    else
                    {
                        //Has been used before, but an update has occurred.
                        ShowWelcomePage = true;
                    }
                }

                LastUsedVersion = AppVersion;

                Thinking = false;
            }
        }

        public string LastUsedVersion
        {
            get
            {
                string result = "";

                if (roamingSettings == null)
                {
                    return result;
                }

                try
                {
                    result = (string)roamingSettings.Values["LastUsedVersion"];
                }
                catch (Exception ex)
                {

                }

                return result;
            }
            set
            {
                roamingSettings.Values["LastUsedVersion"] = value;

                NotifyPropertyChanged("LastUsedVersion");
            }
        }

        public string LastOutputDirectoryToken
        {
            get
            {
                string result = string.Empty;

                if (roamingSettings == null)
                {
                    return result;
                }

                try
                {
                    result = (string)roamingSettings.Values["LastOutputDirectoryToken"];
                }
                catch (Exception ex)
                {
                    roamingSettings.Values["LastOutputDirectoryToken"] = result;
                }

                return result;
            }
            set
            {
                roamingSettings.Values["LastOutputDirectoryToken"] = value;

                NotifyPropertyChanged("LastOutputDirectoryToken");
            }
        }

        public string AppName
        {
            get
            {
                Package package = Package.Current;
                return package.DisplayName;
            }
        }

        public string AppVersion
        {
            get
            {
                var v = Package.Current.Id.Version;
                var version = string.Concat(v.Major, ".", v.Minor, ".", v.Build, ".", v.Revision);
#if DEBUG
                version = string.Concat(version, " (D)");
#endif
                return string.Concat("Version ", version);
            }
        }
    }
}
