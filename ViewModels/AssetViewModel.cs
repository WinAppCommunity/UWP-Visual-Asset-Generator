﻿using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_Visual_Asset_Generator.ViewModels
{
    public class AssetViewModel : ValleyBaseViewModel
    {
        private string _title;

        public AssetViewModel(string title)
        {
            Title = title;
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }
    }
}
