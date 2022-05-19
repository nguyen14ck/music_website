using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MusicWS.Models.MyAnnotation
{
    public class CustomAnnotation
    {
    }

    public class MaxWordsAttribute:RequiredAttribute
    {
        private readonly int _maxWords;

        public MaxWordsAttribute(int maxWords)
        {
            _maxWords = maxWords;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var giaTri = value.ToString();
                string[] soTu = giaTri.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (soTu.Length > _maxWords)
                {
                    return new ValidationResult(String.Format("Số từ tối đa là {0}!", _maxWords));
                }
            }
            return ValidationResult.Success;
        }
    }
}