using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic : SystemLanguageCodePoco
    {
        protected IDataRepository<SystemLanguageCodePoco> _repository;
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
        {
            _repository = repository;
        }

        public virtual SystemLanguageCodePoco Get(String langID)
        {
            return _repository.GetSingle(c => c.LanguageID == langID);
        }

        public virtual List<SystemLanguageCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }


        public void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Add(pocos);
        }

        public void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Update(pocos);
        }

        protected void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();


            foreach (var poco in pocos)
            {

                if (string.IsNullOrEmpty(poco.LanguageID))
                {
                    exceptions.Add(new ValidationException(1000, $"LanguageID for SystemLanguageCode {poco.LanguageID} is required"));
                }

                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(1001, $"Name for SystemLanguageCode {poco.Name} is required"));
                }

                if (string.IsNullOrEmpty(poco.NativeName))
                {
                    exceptions.Add(new ValidationException(1002, $"NativeName for SystemLanguageCode {poco.NativeName} is required"));
                }

            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public void Delete(SystemLanguageCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }
    }
}
