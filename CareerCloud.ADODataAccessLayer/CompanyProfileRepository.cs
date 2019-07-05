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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);

            using (conn)
            {
                foreach (CompanyProfilePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Company_Profiles ([Id],[Registration_Date],[Company_Website],[Contact_Phone],[Contact_Name],[Company_Logo])

                                                VALUES(@Id,@Registration_Date,@Company_Website,@Contact_Phone,@Contact_Name,@Company_Logo)", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);
                 

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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection
             (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand(@"SELECT [Id], [Registration_Date], 
                                                [Company_Website],[Contact_Phone],
                                                [Contact_Name],[Company_Logo],[Time_Stamp]
                                                FROM Company_Profiles", conn);

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyProfilePoco[] companyJobPocos = new CompanyProfilePoco[1000];
                while (rdr.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.RegistrationDate = rdr.GetDateTime(1);
                    poco.CompanyWebsite = rdr.IsDBNull(2)?null:rdr.GetString(2);
                    poco.ContactPhone = rdr.GetString(3);
                    poco.ContactName = rdr.IsDBNull(4) ? null : rdr.GetString(4);
                    poco.CompanyLogo = rdr.IsDBNull(5) ? null : (byte[])rdr.GetSqlBinary(5);
                    poco.TimeStamp = (byte[])rdr[6];

                    companyJobPocos[x] = poco;
                    x++;

                }
                conn.Close();
                return companyJobPocos.Where(a => a != null).ToList();

            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection
                (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            using (conn)
            {

                foreach (CompanyProfilePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Company_Profiles WHERE Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection
                 (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            using (conn)
            {
                foreach (CompanyProfilePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE Company_Profiles 
                                                    SET[Registration_Date] =@Registration_Date ,[Company_Website]=@Company_Website,
                                                    [Contact_Phone]= @Contact_Phone,[Contact_Name]=@Contact_Name,[Company_Logo]=@Company_Logo                                     
                                                    WHERE [Id]= @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
