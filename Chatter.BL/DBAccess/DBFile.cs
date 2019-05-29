using Chatter.BL.DBAccess.Interfaces;
using Chatter.BL.DTO.File;
using Chatter.Common.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Chatter.DAL.Factories;

namespace Chatter.BL.DBAccess
{
    public class DBFile : IDBFile
    {
        private readonly IDBContextFactory chatterDbContextFactory;

        public DBFile(IDBContextFactory chatterDbContextFactory)
        {
            this.chatterDbContextFactory = chatterDbContextFactory;
        }

        readonly Mapper mapper = new Mapper();

        public FileDetailModelDTO AddFile(FileDetailModelDTO file)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var newFile = mapper.MapDetailToEntity(file);

                connection.Files.Add(newFile);
                connection.SaveChanges();
                return mapper.MapEntityToDetailModel(newFile);
            }
        }

        public bool EditFile(FileDetailModelDTO file)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var oldFileId = GetFileByID(file.Id).Id;

                var editedFile = mapper.MapDetailToEntity(file);

                editedFile.Id = oldFileId;

                connection.Files.Update(editedFile);
                return connection.SaveChanges() == 1;
            }
        }

        public ICollection<FileListModelDTO> GetAllFiles()
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return new ObservableCollection<FileListModelDTO>(connection.Files.Select(mapper.MapEntityToListModel).ToList());
            }
        }

        public FileDetailModelDTO GetFileByID(Guid id)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return connection.Files.Where(x => x.Id == id).Select(mapper.MapEntityToDetailModel).FirstOrDefault();
            }
        }

        public ICollection<FileListModelDTO> GetFilesByComment(Guid commentId)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return new ObservableCollection<FileListModelDTO>(connection.Files.Where(x => x.Comment.Id == commentId).Select(mapper.MapEntityToListModel).ToList());
            }
        }

        public ICollection<FileListModelDTO> GetFilesByPost(Guid postId)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return new ObservableCollection<FileListModelDTO>(connection.Files.Where(x => x.Post.Id == postId).Select(mapper.MapEntityToListModel).ToList());
            }
        }

        public ICollection<FileListModelDTO> GetFilesByType(FileType fileType)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return new ObservableCollection<FileListModelDTO>(connection.Files.Where(x => x.Type == fileType).Select(mapper.MapEntityToListModel).ToList());
            }
        }

        public ICollection<FileListModelDTO> GetFilesByUser(Guid userId)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return new ObservableCollection<FileListModelDTO>(connection.Files.Where(x => x.UploadedBy.Id == userId).Select(mapper.MapEntityToListModel).ToList());
            }
        }

        public bool RemoveFile(FileDetailModelDTO file)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                connection.Files.Remove(mapper.MapDetailToEntity(file));
                return connection.SaveChanges() == 1;
            }
        }

        public bool RemoveFile(Guid id)
        {
            return RemoveFile(GetFileByID(id));
        }
    }
}
