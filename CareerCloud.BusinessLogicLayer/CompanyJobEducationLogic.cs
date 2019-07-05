using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobEducationLogic : BaseLogic<CompanyJobEducationPoco>
    {

        public CompanyJobEducationLogic(IDataRepository<CompanyJobEducationPoco> repository) : base(repository)
        {
        }


        public override void Add(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyJobEducationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {

                if (string.IsNullOrEmpty(Convert.ToString(poco.Major)))
                {
                    exceptions.Add(new ValidationException(200, $"Major for CompanyJobEducation {poco.Major} cannot be null"));
                }
                else if (poco.Major.Length < 2)
                {
                    exceptions.Add(new ValidationException(200, $"Major for CompanyJobEducation {poco.Id} cannot be greater than 2 characters"));
                }

                if (string.IsNullOrEmpty(Convert.ToString(poco.Importance)))
                {
                    exceptions.Add(new ValidationException(201, $"Importance for CompanyJobEducation {poco.Importance} cannot be null"));
                }
                else if (poco.Importance < 0)
                {
                    exceptions.Add(new ValidationException(201, $"Importance for CompanyJobEducation {poco.Id} cannot be less than 0 "));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
