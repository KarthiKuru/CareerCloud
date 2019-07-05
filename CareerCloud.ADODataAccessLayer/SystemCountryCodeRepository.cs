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
   public class SystemCountryCodeRepository : IDataRepository<SystemCountryCodePoco>
    {
        public void Add(params SystemCountryCodePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);

            using (conn)
            {
                foreach (SystemCountryCodePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO System_Country_Codes ([Code],[Name])

                                                VALUES(@Code,@Name)", conn);
                    cmd.Parameters.AddWithValue("@Code", poco.Code);
                    cmd.Parameters.AddWithValue("@Name", poco.Name);
         
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

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection
            (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand(@"SELECT [Code], [Name]
                                                FROM System_Country_Codes", conn);

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                SystemCountryCodePoco[] CountryPocos = new SystemCountryCodePoco[1000];
                while (rdr.Read())
                {
                    SystemCountryCodePoco poco = new SystemCountryCodePoco();
                    poco.Code = rdr.GetString(0);
                    poco.Name = rdr.GetString(1);

                    CountryPocos[x] = poco;
                    x++;

                }
                conn.Close();
                return CountryPocos.Where(a => a != null).ToList();

            }
        }

        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            SqlConnection conn = new SqlConnection
                 (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            using (conn)
            {

                foreach (SystemCountryCodePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM System_Country_Codes WHERE Code=@Code", conn);
                    cmd.Parameters.AddWithValue("@Code", poco.Code);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            SqlConnection conn = new SqlConnection
                (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            using (conn)
            {
                foreach (SystemCountryCodePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE System_Country_Codes 
                                                    SET[Name] =@Name 
                                                    WHERE [Code]= @Code", conn);
                    cmd.Parameters.AddWithValue("@Code", poco.Code);
                    cmd.Parameters.AddWithValue("@Name", poco.Name);
        

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
