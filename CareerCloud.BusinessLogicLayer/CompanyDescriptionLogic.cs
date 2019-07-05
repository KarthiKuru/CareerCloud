using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>
    {

        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository) : base(repository)
        {
        }


        public override void Add(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {

                if (string.IsNullOrEmpty(poco.CompanyDescription))
                {
                    exceptions.Add(new ValidationException(107, $"CompanyDescription for CompanyDescription {poco.Id} cannot be null"));
                }
                else if (poco.CompanyDescription.Length >= 2)
                {
                    exceptions.Add(new ValidationException(107, $"CompanyDescription for CompanyDescription {poco.Id} should be greater than 2"));
                }

                if (string.IsNullOrEmpty(poco.CompanyName))
                {
                    exceptions.Add(new ValidationException(106, $"CompanyName for CompanyDescription {poco.Id} cannot be null"));
                }
                else if (poco.CompanyName.Length >= 2)
                {
                    exceptions.Add(new ValidationException(106, $"CompanyName for CompanyDescription {poco.Id} should be greater than 2"));
                }

            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
