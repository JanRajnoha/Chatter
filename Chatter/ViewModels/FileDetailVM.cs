using Chatter.BL;
using Chatter.BL.DBAccess;
using Chatter.BL.DTO.File;
using Chatter.BL.DTO.Group;
using Chatter.BL.Messages.FileMessages;
using Chatter.Commands;
using Chatter.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chatter.ViewModels
{
    public class FileDetailVM : VMBase
    {
        private readonly DBFile fileDBAccess;

        private FileDetailModelDTO fileDetail;

        public FileDetailModelDTO FileDetail
        {
            get
            {
                return fileDetail;
            }

            set
            {
                fileDetail = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteCommand { get; set; }

        public FileDetailVM(DBFile dbFile, IMessenger messenger)
        {
            fileDBAccess = dbFile;
            this.messenger = messenger;

            DeleteCommand = new RelayCommand(DeleteFile);

            messenger.Register<FileChangedMessage>(FileChanged);
        }

        private void DeleteFile(object obj)
        {
            if (obj is FileDetailModelDTO fileDetail)
                fileDBAccess.RemoveFile(fileDetail.Id);
        }

        private void FileChanged(FileChangedMessage obj)
        {
            FileDetail = fileDBAccess.GetFileByID(obj.Id);
        }
    }
}
