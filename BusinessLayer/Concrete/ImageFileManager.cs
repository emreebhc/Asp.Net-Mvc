using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ImageFileManager : IImageFileService
    {
        IImageFileDal _ImageFile;

        public ImageFileManager(IImageFileDal ımageFile)
        {
            _ImageFile = ımageFile;
        }

        public List<ImageFile> GetList()
        {
          return  _ImageFile.List();
        }
    }
}
