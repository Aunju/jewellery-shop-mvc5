using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using Project.Models;

namespace Project.Controllers
{
    public class ReportsController : Controller
    {
        JewelleryDbContext db = new JewelleryDbContext();
        // GET: Report
        public ActionResult Index()
        {
            try
            {
                string str = "Data Source=DESKTOP-KE6N3B4;Initial Catalog=R49_JewelleryDB;Integrated Security=True";
                SqlConnection con = new SqlConnection(str);
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand command = con.CreateCommand();
                command.CommandText = "Select* from [Categories] ca inner join Items i on ca.CategoryId=i.CategoryId";
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds, "Categories");
                ReportDocument rd = new ReportDocument();
                string rptPath = System.Web.HttpContext.Current.Server.MapPath("~/Report/Jewellery.rpt");
                rd.Load(rptPath);
                rd.SetDataSource(ds);
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Flush();
                rd.Close();
                rd.Dispose();
                return File(stream, System.Net.Mime.MediaTypeNames.Application.Pdf);
            }
            catch (Exception ex)
            {
                return Content("<h2>Error:" + ex.Message + "</h2>", "text/html");
            }

        }
        

    }
}