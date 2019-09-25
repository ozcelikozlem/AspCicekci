using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace AspCicekci
{
    public partial class Cicekler1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
       
            string yol = "data source=DESKTOP-H0I06TG; initial catalog=CICEKCIM; integrated security=SSPI";
            SqlConnection con = new SqlConnection(yol);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from OnayliCicek where OnayliKategori='Çiçekler'", con);

            DataTable dt = new DataTable();
            da.Fill(dt);//veri tabnını doldur
            PagedDataSource pds = new PagedDataSource();//sayfalama için veri kaynağı oluştur.
            pds.DataSource = dt.DefaultView;//Veritabnındaki leri sayfalama kaynağının içine at
            pds.AllowPaging = true;//Sayfala
            pds.PageSize = 3;//Kaç veri olacak sayfada
            int sayfa;
            if (Request.QueryString["sayfa"] != null)//sayfalama için querystring aynı sayfada veri aktarımı için
            {
                sayfa = Convert.ToInt32(Request.QueryString["sayfa"]);

            }
            else
            {

                sayfa = 1;// eğer veri yoksa sayfa 1

            }

            pds.CurrentPageIndex = sayfa - 1;
            for (int i = 1; i <= pds.PageCount; i++)//PageCount olması Sayfalama değere göre olsun boyuta göre değil
            {
                HyperLink hyper = new HyperLink();
                hyper.Text = i.ToString();
                hyper.NavigateUrl = "Cicekler1.aspx?sayfa=" + i.ToString();//Sayfa linki aynı sayfa içim 
                Panel1.Controls.Add(hyper);


            }

            Repeater1.DataSource = pds;
            Repeater1.DataBind();
            con.Close();

        }
    }
}