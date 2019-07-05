using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {

        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
        {
        }


        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {

                if (poco.StartMonth >12)
                {
                    exceptions.Add(new ValidationException(101, $"StartMonth for ApplicantSkill {poco.Id} should be less than 12"));
                }

                if (poco.EndMonth > 12)
                {
                    exceptions.Add(new ValidationException(102, $"EndMonth for ApplicantSkill {poco.Id} should be less than 12"));
                }

                if (poco.StartYear <1900 )
                {
                    exceptions.Add(new ValidationException(103, $"StartYear for ApplicantSkill {poco.Id} Cannot be less then 1900 "));
                }

                if (poco.EndYear < poco.StartYear)
                {
                    exceptions.Add(new ValidationException(104, $"EndYear for ApplicantSkill {poco.Id} Cannot be less then start year "));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
