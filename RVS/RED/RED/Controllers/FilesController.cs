﻿using RED.Models.ControllerBases;
using RED.Models.FileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RED.Controllers
{
    public class FilesController : ControllerBase<FilesRepository>
    {
        public FileResult GetRequestFile(Guid diaryId)
        {
            string fileName = "";
            var reportData = Rep.GetRequestListReport(diaryId, out fileName);
            return File(reportData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
	}
}