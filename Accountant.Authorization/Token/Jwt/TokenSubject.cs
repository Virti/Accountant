using System;

namespace Accountant.Authorization.Token.Jwt {

    public class TokenSubject {
        private Guid _tenantId;
        private Guid _userId;

        public string Value { 
            get {
                return $"{_tenantId}{Separator}{_userId}";
            }

            private set {
                var subjectParts = value.Split(Separator);
                
                _tenantId = new Guid(subjectParts[0]);
                _userId = new Guid(subjectParts[1]);
            }
        }

        private const char Separator = '.';

        public TokenSubject(string subject)
        {
            Value = subject;
        }

        public TokenSubject(Guid tenantId, Guid userId)
        {
            _tenantId = tenantId;
            _userId = userId;
        }

        public override string ToString() => Value;

        public override bool Equals(object obj)
        {
            var item = obj as TokenSubject;
            return (item != null && item.Value == Value);
        }

        public static bool operator ==(TokenSubject a, TokenSubject b) => a.Equals(b);
        public static bool operator !=(TokenSubject a, TokenSubject b) => !a.Equals(b);

        public override int GetHashCode() => Value.GetHashCode();

        public static explicit operator TokenSubject(string value) => new TokenSubject(value);
    }
}