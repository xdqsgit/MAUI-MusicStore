﻿#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using System;
using System.ComponentModel;


namespace MusicStore
{
    public class ListViewContactsInfo : INotifyPropertyChanged
    {
        #region Fields

        private string contactName;      
        private string image;
   
        #endregion

        #region Constructor

        public ListViewContactsInfo()
        {

        }

        #endregion

        #region Public Properties

        public string ContactName
        {
            get { return this.contactName; }
            set
            {
                this.contactName = value;
                RaisePropertyChanged("ContactName");
            }
        }

        public string ContactImage
        {
            get { return this.image; }
            set
            {
                if (value != null)
                {
                    this.image = value;
                    this.RaisePropertyChanged("ContactImage");
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(String name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
