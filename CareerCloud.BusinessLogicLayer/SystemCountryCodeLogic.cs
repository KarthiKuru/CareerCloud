using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic : SystemCountryCodePoco
    {
        protected IDataRepository<SystemCountryCodePoco> _repository;
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository) 
        {
            _repository = repository;
        }

        public virtual SystemCountryCodePoco Get(String code)
        {
            return _repository.GetSingle(c => c.Code == code);
        }

        public virtual List<SystemCountryCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }


        public void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Add(pocos);
        }

        public void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Update(pocos);
        }

        protected void Verify(SystemCountryCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();


            foreach (var poco in pocos)
            {

                if (string.IsNullOrEmpty(poco.Code))
                {
                    exceptions.Add(new ValidationException(900, $"Code for SystemCountryCode {poco.Code} is required"));
                }

                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(901, $"Name for SystemCountryCode {poco.Code} is required"));
                }

            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
        public void Delete(SystemCountryCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }
    }
}
