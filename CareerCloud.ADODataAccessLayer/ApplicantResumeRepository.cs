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
    public class ApplicantResumeRepository : IDataRepository<ApplicantResumePoco>
    {
        public void Add(params ApplicantResumePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            using (conn)
            {
                foreach (ApplicantResumePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Applicant_Resumes ([Id], [Applicant], [Resume], [Last_Updated])
                                                    VALUES (@Id, @Applicant, @Resume, @Last_Updated)", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);

                    conn.Open();
                    int rowExecuted = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection
               (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand(@"SELECT [Id], [Applicant], 
                                                    [Resume], [Last_Updated]
                                                FROM Applicant_Resumes", conn);

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantResumePoco[] appResumePocos = new ApplicantResumePoco[1000];
                while (rdr.Read())
                {
                    ApplicantResumePoco poco = new ApplicantResumePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Applicant = rdr.GetGuid(1);
                    poco.Resume = rdr.GetString(2);
                    poco.LastUpdated = rdr.IsDBNull(3) ? (DateTime?)null : rdr.GetDateTime(3);

                    appResumePocos[x] = poco;
                    x++;

                }
                conn.Close();
                return appResumePocos.Where(a => a != null).ToList();

            }
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            SqlConnection conn = new SqlConnection
               (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);


            using (conn)
            {

                foreach (ApplicantResumePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Applicant_Resumes WHERE Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            SqlConnection conn = new SqlConnection
               (ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            using (conn)
            {
                foreach (ApplicantResumePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE Applicant_Resumes 
                                                    SET[Applicant] =@Applicant ,[Resume]=@Resume,
                                                    [Last_Updated]= @Last_Updated
                                                    WHERE [Id]= @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

        }
    }
}
