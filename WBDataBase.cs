using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeManagement
{
    class WBDataBase
    {
        private DataSet codedataset = new DataSet("CODE");
        //public DataSet CodeDataSet { get { return codedataset; } }
        private DataTable Codetable { get; set; }
        
        DataSet ds = new DataSet("CodeArchive");

        private string constr;
        private SqlConnection scon;

        public WBDataBase()
        {
            constr = @"Data Source=DESKTOP-64A98NM;Initial Catalog=WBSample;User ID=SIK;Password=qwerty";
            scon = new SqlConnection(constr);
            Codetable = new DataTable("CODE");
        }

        public void Open()
        {
            try
            {
                scon.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Dispose()
        {
            scon.Close();
            MessageBox.Show("DB Close()");
        }

        #region Product(객체 생성을 통한 명령객체)

        //insert into Product values ('ADO.NET', 12000, '데이터베이스 프로그래밍 교재')
        //ExecuteNonQuery : insert, update, delete
        public void InsertCode(string keyword ,string url)
        {
            string sql =
                string.Format("insert into CODE values('{0}', '{1}')",  keyword, url);

            using (SqlCommand command = new SqlCommand(sql, scon))
            {
                if (command.ExecuteNonQuery() != 1)
                    throw new Exception("추가 실패");
            }
        }

        //delete from Product where pname = '??';
        public void DeleteCode(string url)
        {
            string sql = string.Format("delete from CODE where URL = {0}", url);

            using (SqlCommand command = new SqlCommand(sql, scon))
            {
                if (command.ExecuteNonQuery() == 0)
                    throw new Exception("삭제 실패");
            }
        }

        //update from product set price = ??, description = '??' where pname = '??';
        public void UpdateCode(int uid, string keyword, string url)
        {
            string sql =
                string.Format("update CODE set UID = {0}, KEYWORD = '{1}' where URL = '{2}'",
                uid, keyword, url);

            using (SqlCommand command = new SqlCommand(sql, scon))
            {
                if (command.ExecuteNonQuery() == 0)
                    throw new Exception("수정 실패");
            }
        }

        public List<CODE> KeyWordToURL(string keyword)
        {
            string sql = 
            string.Format("select URL from CODE where KEYWORD = '{0}'", keyword);

            using (SqlCommand command = new SqlCommand(sql, scon))
            {
                SqlDataReader reader = command.ExecuteReader();
                List<CODE> product = new List<CODE>();
                while (reader.Read())
                {
                    product.Add(new CODE(int.Parse(reader["UID"].ToString()),
                                            reader["KEYWORD"].ToString(),
                                            reader["URL"].ToString())) ;
                }
                reader.Close();
                return product;
            }
        }

        public int KeyWordToUID(string keyword)
        {
            string sql =
             string.Format("select UID from CODE where KEYWORD = '{0}'", keyword);

            using (SqlCommand command = new SqlCommand(sql, scon))
            {
                return (int)command.ExecuteScalar();
            }
        }

        #endregion

        /*#region Custom(파라미터 기반 명령객체 사용)

        //select * from book; //다중 rowdata, 다중 col
        public List<CODE> CodeList()
        {
            string comtext = "select * from CODE";
            SqlCommand command = new SqlCommand(comtext, scon);

            SqlDataReader reader = command.ExecuteReader();

            List<CODE> products = new List<CODE>();
            while (reader.Read())
            {
                products.Add(new CODE(reader["URL"].ToString(),
                                      reader["CODENAME"].ToString(),
                                      reader["FILEPATH"].ToString()));
            }
            reader.Close();

            return products;
        }

        //select * from book where bname = '홍길동'; 다중컬럼, 단일 로우데이터
        public CODE CodeList(string codename)
        {
            string comtext = "select * from CODE where codename = @CODENAME";
            SqlCommand command = new SqlCommand(comtext, scon);
            //- 파라미터 등록---
            SqlParameter param_codename = new SqlParameter();
            param_codename.ParameterName = "@CODENAME";
            param_codename.SqlDbType = System.Data.SqlDbType.VarChar;
            param_codename.Value = codename;
            command.Parameters.Add(param_codename);
            //-----------------
            SqlDataReader reader = command.ExecuteReader();

            reader.Read();  //<-----
            CODE code = new CODE(reader["URL"].ToString(),
                                      reader["CODENAME"].ToString(),
                                      reader["FILEPATH"].ToString());
            reader.Close(); //<---

            return code;
        }
        #endregion
        */
        #region FILL
        public void Fill()
        {
            string comtxt_code = "select * from CODE;";

            try
            {
               // ds = new DataSet("CodeArchive");

                //PK 획득 문제...........................
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(comtxt_code, con);
                    adapter.FillSchema(ds, SchemaType.Source, "CODE");
                    adapter.Fill(ds, "CODE");  //DB.OPEN(), 명령실행(ExcuteQuery), DB.CLOSE()
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
       
        #region CODES
        public void MakeTable_Codes()
        {
            Codetable = new DataTable("CODES");

            DataColumn dc_uid = new DataColumn("UID", typeof(int));
            dc_uid.AllowDBNull = false;
            Codetable.Columns.Add(dc_uid);

            DataColumn dc_keyword = new DataColumn();
            dc_keyword.ColumnName = "KEYWORD";
            dc_keyword.DataType = typeof(string);
            dc_keyword.AllowDBNull = false;
            Codetable.Columns.Add(dc_keyword);

            DataColumn dc_url = new DataColumn();
            dc_url.ColumnName = "URL";
            dc_url.DataType = typeof(string);
            dc_url.AllowDBNull = false;
            Codetable.Columns.Add(dc_url);
        }

        public void PrintTable_Codes(DataGridView dataGridView)
        {
            //Codetable = librarydataset.Tables[1];   //<======================
            string temp = string.Empty;
            //temp += "테이블명 : " + Booktable.TableName + "\r\n";
            //temp += "컬럼개수 : " + Booktable.Columns.Count + "\r\n";
            //temp += "-------------------------------------------------------------\r\n";
            //foreach (DataColumn dc in Booktable.Columns)
            //    temp += getColumnInfo(dc) + "\r\n";
            //temp += "-------------------------------------------------------------\r\n";
            //temp += "기본키 : " + Booktable.PrimaryKey[0].ColumnName;
            //textbox.Text = temp;
        }

        private string getColumnInfo(DataColumn c)
        {
            return string.Format("{0} : {1}, (널허용여부-{2}, 유니크-{3})",
                c.ColumnName, c.DataType, c.AllowDBNull, c.Unique);
        }

        public void DataBinding(DataGridView view)
        {
            view.DataSource = Codetable;
        }

        #endregion
    }
}
