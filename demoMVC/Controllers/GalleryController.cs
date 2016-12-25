using System.Linq;
using System.Web.Mvc;
using demoMVC.Domain;
using demoMVC.Models;

namespace demoMVC.Controllers
{
    public class GalleryController : Controller
    {
        public ActionResult Index()
        {
            var model = new GalleryModel(){Images = FIleStoreInsert.ImageList(FIleStoreInsert.Gallery)};
            return View(model);
        }

        public FileContentResult GetImg(string fileName)
        {
            var tempImgResult = FIleStoreInsert.StreamToSingleFile(fileName);
            return tempImgResult.Keys.First() != null
                ? new FileContentResult(tempImgResult.Keys.First(), tempImgResult.Values.First())
                : null;
        }
    }
}