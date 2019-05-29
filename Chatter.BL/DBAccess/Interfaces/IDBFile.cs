using Chatter.BL.DTO.File;
using Chatter.Common.Enum;
using System;
using System.Collections.Generic;

namespace Chatter.BL.DBAccess.Interfaces
{
    public interface IDBFile
    {
        FileDetailModelDTO AddFile(FileDetailModelDTO file);

        FileDetailModelDTO GetFileByID(Guid id);

        bool RemoveFile(Guid id);

        bool RemoveFile(FileDetailModelDTO file);

        bool EditFile(FileDetailModelDTO file);

        ICollection<FileListModelDTO> GetAllFiles();

        ICollection<FileListModelDTO> GetFilesByType(FileType fileType);

        ICollection<FileListModelDTO> GetFilesByUser(Guid userId);

        ICollection<FileListModelDTO> GetFilesByPost(Guid postId);

        ICollection<FileListModelDTO> GetFilesByComment(Guid commentId);
    }
}