#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace MusicStore
{
    //ViewModel class for ListViewComponent
    public class ListViewModel
    {
        #region Fields

        private ObservableCollection<ListViewContactsInfo> contactsInfo;

        #endregion

        #region Constructor

        public ListViewModel()
        {
            GenerateSource(100);
        }

        #endregion

        #region Properties

        public ObservableCollection<ListViewContactsInfo> Contacts
        {
            get { return contactsInfo; }
            set { this.contactsInfo = value; }
        }

        #endregion

        #region ItemSource

        public void GenerateSource(int count)
        {
            ListViewContactsInfoRepository contactRepository = new();
            contactsInfo = contactRepository.GetContactDetails(count);
        }

        #endregion
    }
}
