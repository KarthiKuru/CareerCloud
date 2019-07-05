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
    public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);

            foreach (ApplicantWorkHistoryPoco poco in items)
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Applicant_Work_History ([Id], [Applicant],[Company_Name], [Country_Code], [Location],[Job_Title],[Job_Description], [Start_Month],[Start_Year],[End_Month],[End_Year])
                                                VALUES (@Id,@Applicant,@Company_Name, @Country_Code, @Location,@Job_Title,@Job_Description, @Start_Month,@Start_Year,@End_Month,@End_Year )", conn);

                cmd.Parameters.AddWithValue("@Id", poco.Id);
                cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                cmd.Parameters.AddWithValue("@Location", poco.Location);
                cmd.Parameters.AddWithValue("@Job_Title", poco.JobTitle);
                cmd.Parameters.AddWithValue("@Job_Description", poco.JobDescription);
                cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection
              (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand(@"SELECT [Id], [Applicant], 
                                                [Company_Name],[Country_Code],
                                                [Location],[Job_Title],
                                                [Job_Description],[Start_Month],
                                                [Start_Year],[End_Month],
                                                [End_Year],[Time_Stamp]
                                                FROM Applicant_Work_History", conn);

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantWorkHistoryPoco[] appWorkPocos = new ApplicantWorkHistoryPoco[1000];
                while (rdr.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Applicant = rdr.GetGuid(1);
                    poco.CompanyName = rdr.GetString(2);
                    poco.CountryCode = rdr.GetString(3);
                    poco.Location = rdr.GetString(4);
                    poco.JobTitle = rdr.GetString(5);
                    poco.JobDescription = rdr.GetString(6);
                    poco.StartMonth = (short)rdr.GetSqlInt16(7);
                    poco.StartYear =(int)rdr.GetSqlInt32(8);
                    poco.EndMonth = (short)rdr.GetSqlInt16(9);
                    poco.EndYear = (int)rdr.GetSqlInt32(10);
                    poco.TimeStamp = (byte[])rdr[11];

                    appWorkPocos[x] = poco;
                    x++;

                }
                conn.Close();
                return appWorkPocos.Where(a => a != null).ToList();

            }
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection conn = new SqlConnection
                (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            using (conn)
            {

                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Applicant_Work_History WHERE Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection conn = new SqlConnection
               (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            using (conn)
            {
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE Applicant_Work_History 
                                                    SET[Applicant] =@Applicant ,[Company_Name]=@Company_Name,
                                                    [Country_Code]= @Country_Code,[Location]= @Location,
                                                    [Job_Title]= @Job_Title,[Job_Description]= @Job_Description,
                                                    [Start_Month]=@Start_Month,[Start_Year]=@Start_Year,
                                                    [End_Month]=@End_Month,[End_Year]=@End_Year                                              
                                                    WHERE [Id]= @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", poco.Location);
                    cmd.Parameters.AddWithValue("@Job_Title", poco.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", poco.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);
                    

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
