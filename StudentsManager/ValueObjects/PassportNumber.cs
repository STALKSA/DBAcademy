using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace StudentsManager.ValueObjects
{
    
    public class PassportNumber
    {
        public string Value { get; private set; }
        protected PassportNumber() { }
        public PassportNumber(string value)
        { 
            if(value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if(!IsPassportNumber(value))
            {
                throw new InvalidOperationException("Некорректный номер");
            }
            Value = value;
        }

        public override string ToString() => Value;

        private  static bool IsPassportNumber(string value)
        {
            var ruPassportNumberRegex = new Regex(@"(\d{4})\s*-?\s*(\d{6})");
            return ruPassportNumberRegex.IsMatch(value);
        }

        protected bool Equals(PassportNumber other)
        { return Value == other.Value; }

        public override bool Equals(object? obj)
        {
            //if (ReferenceEquals(null, obj)) return false;
            //if (ReferenceEquals(this, obj)) return true;
            //if (obj.GetType() != this.GetType()) return false;

            //return Equals((PassportNumber)obj);

            return obj is PassportNumber value && Value == value.Value; 
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(PassportNumber? left, PassportNumber? right)
        {
            return EqualityComparer<PassportNumber>.Default.Equals(left, right);
        }

        public static bool operator !=(PassportNumber? left, PassportNumber? right)
        {
            return !(left == right);
        }

    }
}
