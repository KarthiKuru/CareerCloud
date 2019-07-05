using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyDescriptionRepository : IDataRepository<CompanyDescriptionPoco>
    {
        public void Add(params CompanyDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);

            using (conn)
            {
                foreach (CompanyDescriptionPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Company_Descriptions ([Id],[Company],[LanguageID],[Company_Name],[Company_Description])                                                VALUES(@Id,@Company,@LanguageID,@Company_Name,@Company_Description)", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);
                    
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection
             (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand(@"SELECT [Id], [Company], 
                                                [LanguageID],[Company_Name],
                                                [Company_Description],[Time_Stamp]
                                                FROM Company_Descriptions", conn);

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyDescriptionPoco[] companyDescPocos = new CompanyDescriptionPoco[1000];
                while (rdr.Read())
                {
                    CompanyDescriptionPoco poco = new CompanyDescriptionPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Company = rdr.GetGuid(1);
                    poco.LanguageId = rdr.GetString(2);
                    poco.CompanyName = rdr.GetString(3);
                    poco.CompanyDescription = rdr.GetString(4);
                    poco.TimeStamp = (byte[])rdr[5];


                    companyDescPocos[x] = poco;
                    x++;

                }
                conn.Close();
                return companyDescPocos.Where(a => a != null).ToList();

            }
        }

            public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection
                (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            using (conn)
            {

                foreach (CompanyDescriptionPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Company_Descriptions WHERE Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection
               (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            using (conn)
            {
                foreach (CompanyDescriptionPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE Company_Descriptions 
                                                    SET[Company] =@Company ,[LanguageID]=@LanguageID,
                                                    [Company_Name]= @Company_Name,[Company_Description]= @Company_Description                                             
                                                    WHERE [Id]= @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);
                    

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
