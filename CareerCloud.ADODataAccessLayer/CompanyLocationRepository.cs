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
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);

            using (conn)
            {
                foreach (CompanyLocationPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Company_Locations ([Id],[Company],[Country_Code],[State_Province_Code],[Street_Address],[City_Town],[Zip_Postal_Code])

                                                VALUES(@Id,@Company,@Country_Code,@State_Province_Code,@Street_Address,@City_Town,@Zip_Postal_Code)", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection
             (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand(@"SELECT [Id], [Company], 
                                                [Country_Code],[State_Province_Code],
                                                [Street_Address],[City_Town],
                                                [Zip_Postal_Code],[Time_Stamp]
                                                FROM Company_Locations", conn);

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyLocationPoco[] companyLocPocos = new CompanyLocationPoco[2000];
                while (rdr.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Company = rdr.GetGuid(1);
                    poco.CountryCode = rdr.GetString(2);
                    poco.Province = rdr.GetString(3);
                    poco.Street = rdr.GetString(4);
                    poco.City = rdr.IsDBNull(5)?null:rdr.GetString(5);
                    poco.PostalCode = rdr.IsDBNull(6)?null:rdr.GetString(6);
                    poco.TimeStamp = (byte[])rdr[7];

                    companyLocPocos[x] = poco;
                    x++;

                }
                conn.Close();
                return companyLocPocos.Where(a => a != null).ToList();

            }
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            SqlConnection conn = new SqlConnection
                (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            using (conn)
            {

                foreach (CompanyLocationPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Company_Locations WHERE Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            SqlConnection conn = new SqlConnection
                (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            using (conn)
            {
                foreach (CompanyLocationPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE Company_Locations 
                                                    SET[Company] =@Company ,[Country_Code]=@Country_Code,
                                                    [State_Province_Code]= @State_Province_Code,[Street_Address]=@Street_Address,
                                                    [City_Town]= @City_Town,[Zip_Postal_Code]=@Zip_Postal_Code
                                                    WHERE [Id]= @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
